using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.GEntity;
using Game.Component;
using Game.Misc;
namespace Game.System
{
    public class Astar : GSystem
    {

        // -8 15 -4
        // -8 15 14
        // 32 15 -4
        // 32 15 14
        private Dictionary<Vector2, GameObject> gameObjs = new Dictionary<Vector2, GameObject>();
        private Dictionary<Vector3, bool> finalList = new Dictionary<Vector3, bool>();
        private PriorityQueue<Node> openList = new PriorityQueue<Node>();
        private List<Node> closedList = new List<Node>();

        bool finished;
        public override void Update(GameManager game, float delta)
        {
            foreach (int entity in _entityList)
            {
                GAstarComponent astarComponent = game.Entities.GetComponentOf<GAstarComponent>(entity);
                GTransform transform = game.Entities.GetComponentOf<GTransform>(entity);
                Node endNode = NodeDic[astarComponent._goal];
                if (astarComponent._active)
                {
                    openList.Enqueue(NodeDic[new Vector2((int)transform._position.x, (int)-transform._position.z)], 0);
                }
                while (astarComponent._active)
                {
                    Node current = openList.Dequeue();
                    for (int i = 0; i < 8; i++)
                    {
                        if (current.neighbours[i].value > 0)
                        {
                            if (NodeDic.ContainsKey(new Vector2(current.neighbours[i].position.x, current.neighbours[i].position.z)))
                            {
                                Node child = NodeDic[new Vector2(current.neighbours[i].position.x, current.neighbours[i].position.z)];
                                if (child == endNode)
                                {
                                    astarComponent._active = false;
                                    child.jump = current.neighbours[i].jump;
                                    child.parent = current;
                                    current = endNode;
                                    break;
                                }
                                if (!openList.Contains(child) && !closedList.Contains(child))
                                {
                                    gameObjs[new Vector2(current.position.x, current.position.z)].renderer.material.color = Color.red;
                                    float dst = Vector3.Distance(child.position, endNode.position);
                                    child.jump = current.neighbours[i].jump;
                                    child.parent = current;
                                    child.value = child.parent.value + current.neighbours[i].value;
                                    openList.Enqueue(child, dst + child.value);
                                }
                            }
                            else
                            {
                                Debug.Log("Did not exist " + new Vector2(current.neighbours[i].position.x, current.neighbours[i].position.z));
                            }
                        }
                    }
                    closedList.Add(current);
                    if (!astarComponent._active)
                    {
                        while (current.parent != null)
                        {
                            finalList.Add(current.position, current.jump);
                            gameObjs[new Vector2(current.position.x, current.position.z)].transform.localScale *= 4;
                            gameObjs[new Vector2(current.position.x, current.position.z)].renderer.material.color = Color.yellow;
                            current = current.parent;
                            //astarComponent._path.Add(current.position, current.jump);
                        }
                        //Sätt ihop dessa loopar
                        astarComponent._current = finalList.Last().Key;
                        for (int i = finalList.Count - 1; i >= 0; i--)
                        {
                            //finalList[finalList.ElementAt(i - 1).Key] = finalList.ElementAt(i).Value;
                            if (finalList.ElementAt(i).Value)
                            {
                                astarComponent._path.Add(finalList.ElementAt(i).Key, false);
                                astarComponent._path[finalList.ElementAt(i + 1).Key] = true;
                                Debug.Log("Fix Jump " + finalList.ElementAt(i + 1).Key);
                            }
                            else
                            {
                                astarComponent._path.Add(finalList.ElementAt(i).Key, finalList.ElementAt(i).Value);
                            }
                        }
                        astarComponent._index = 0;
                        finalList.Clear();
                        closedList.Clear();
                        openList.Clear();

                    }
                }
            }
        }


        public Dictionary<Vector2, Node> NodeDic = new Dictionary<Vector2, Node>();
        Vector2[] getN = {
                             new Vector2(-0.5f, 0.5f),
                             new Vector2(0, 0.5f),
                             new Vector2( 0.5f, 0.5f),
                             new Vector2(-0.5f, -0.5f),
                             new Vector2(0, -0.5f),
                             new Vector2(0.5f, -0.5f)
                         };
        Vector2[] getX = {
                             new Vector2(-0.5f, 0),
                             new Vector2(0.5f, 0)
                         };
        public override void InitSystems(GameManager game)
        {
            //Create Nodes
            Vector3 currentPosition = new Vector3(-4, 15, -2f);
            Vector3 endPosition = new Vector3(-8, 15, 14);
            RaycastHit hit;
            Node[] nodes = new Node[8];
            for (int i = 0; i < 360; i++)
            {

                for (int j = 0; j < 30; j++)
                {
                    if (Physics.Raycast(currentPosition, Vector3.down, out hit, 1000))
                    {
                        // Normal Block
                        if (hit.normal.y == 1f)
                        {

                            Node n = new Node(new Vector3(currentPosition.x, hit.point.y, currentPosition.z), Vector2.zero);
                            NodeDic.Add(new Vector2(currentPosition.x, currentPosition.z), n);
                        }
                        // Sloped Block
                        else if (0.3f < hit.normal.y)
                        {

                            float slopeY;
                            //calculate what kind of slope (left/right)
                            if (hit.normal.x < 0)
                            {
                                slopeY = hit.collider.transform.position.y + (-hit.collider.transform.right * hit.collider.transform.localScale.x / 2).y + (hit.collider.transform.up * hit.collider.transform.localScale.y / 2).y;
                            }
                            else
                            {
                                slopeY = hit.collider.transform.position.y + (-hit.collider.transform.up * hit.collider.transform.localScale.y / 2).y + (hit.collider.transform.right * hit.collider.transform.localScale.x / 2).y;
                            }

                            Node n = new Node(new Vector3(currentPosition.x,hit.point.y, currentPosition.z), new Vector2(Mathf.Sign(hit.normal.x), slopeY));
                            NodeDic.Add(new Vector2(currentPosition.x, currentPosition.z), n);
                            //Debug.Log("Slope " + NodeDic[new Vector2(currentPosition.x, currentPosition.z)]);

                        }
                        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        gameObjs.Add(new Vector2(currentPosition.x, currentPosition.z), go);
                        go.collider.enabled = false;
                        go.transform.position = hit.point;
                        go.transform.localScale *= 0.05f;
                    }
                    currentPosition.z += 0.5f;

                }

                currentPosition.z = -2f;
                currentPosition.x += 0.5f;

            }


            // Create Node relations OBS, Ändra til for, skriv över hela N
            foreach (Node n in NodeDic.Values)
            {
               
                //Debug.Log("n " + n.position);
                for (int i = 0; i < 2; i++)
                {
                    
                    Vector2 temp = new Vector2(n.position.x + getX[i].x, n.position.z + getX[i].y);
                    if (NodeDic.ContainsKey(temp))
                    {
                        CopyNode cn = new CopyNode(NodeDic[temp].position, NodeDic[temp].sloped);
                        n.neighbours[i] = cn;
                     

                      if (n.neighbours[i].position.y > n.position.y)
                      {
                          if (n.neighbours[i].sloped == Vector2.zero || n.neighbours[i].sloped.y > n.position.y)
                          {
                              n.neighbours[i].value = Vector3.Distance(n.position, n.neighbours[i].position) * 15;
                              n.neighbours[i].jump = true;
                          }
                          else
                          {
                              n.neighbours[i].value = Vector2.Distance(new Vector2(n.position.x, n.position.z), new Vector2(n.neighbours[i].position.x, n.neighbours[i].position.z)) * 10;
                          }
                      }
                      //Down Node
                      else
                      {
                          n.neighbours[i].value = Vector2.Distance(new Vector2(n.position.x, n.position.z), new Vector2(n.neighbours[i].position.x, n.neighbours[i].position.z)) * 10;
                      }
                      if (n.neighbours[i].position.y > n.position.y + 3)
                      {
                          n.neighbours[i].value = -1;
                      }
                    }
                    else
                    {
                        for (int j = 2; j < 12; j++)
                        {
                            if (NodeDic.ContainsKey(new Vector2(n.position.x + getX[i].x * j, n.position.z + getX[i].y * j)))
                            {
                                
                                CopyNode cn = new CopyNode(NodeDic[new Vector2(n.position.x + getX[i].x * j, n.position.z + getX[i].y * j)].position, NodeDic[new Vector2(n.position.x + getX[i].x * j, n.position.z + getX[i].y * j)].sloped);
                                n.neighbours[i] = cn;
                                float dst = Vector3.Distance(n.position, n.neighbours[i].position);
                                n.neighbours[i].jump = true;
                                if (dst > 5)
                                {
                                    n.neighbours[i].value = -1;
                                }
                                else
                                {
                                    n.neighbours[i].value = dst * 200;
                                }
                                break;
                            }
                        }
                    }
                }
                for (int i = 0; i < 6; i++)
                {
                    Vector2 temp = new Vector2(n.position.x + getN[i].x, n.position.z + getN[i].y);
                    if (NodeDic.ContainsKey(temp))
                    {
                        CopyNode cn = new CopyNode(NodeDic[temp].position, NodeDic[temp].sloped);
                        n.neighbours[i + 2] = cn;
                        if (n.neighbours[i + 2].sloped == n.sloped)
                        {
                            n.neighbours[i + 2].value = Vector3.Distance(n.position, n.neighbours[i + 2].position) * 10;
                        }
                        if (n.neighbours[i + 2].position.y > n.position.y)
                        {
                           if (n.neighbours[i + 2].sloped == n.sloped && n.sloped == Vector2.zero)
                           {
                               
                               n.neighbours[i + 2].value = Vector3.Distance(n.position, n.neighbours[i + 2].position) * 15;
                               n.neighbours[i + 2].jump = true;
                           }
                       }
                       else
                       {
                           n.neighbours[i + 2].value = Vector3.Distance(n.position, n.neighbours[i + 2].position) * 10;
                       }
                        if (n.neighbours[i + 2].position.y > n.position.y + 3)
                       {
                           n.neighbours[i + 2].value = -1;
                       }
                    }
                    else
                    {
                        for (int j = 2; j < 12; j++)
                        {
                            if (NodeDic.ContainsKey(new Vector2(n.position.x + (getN[i].x * j), n.position.z + (getN[i].y * j))))
                            {
                    
                                CopyNode cn = new CopyNode(NodeDic[new Vector2(n.position.x + getN[i].x * j, n.position.z + getN[i].y * j)].position, NodeDic[new Vector2(n.position.x + getN[i].x * j, n.position.z + getN[i].y * j)].sloped);
                                n.neighbours[i+2] = cn;
                                n.neighbours[i+2].jump = true;
                                float dst = Vector3.Distance(n.position, n.neighbours[i + 2].position);
                                n.neighbours[i + 2].jump = true;
                                if (dst > 5)
                                {
                                    n.neighbours[i + 2].value = -1;
                                }
                                else
                                {
                                    n.neighbours[i + 2].value = dst * 200;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            //21.5 4
            openList.Enqueue(NodeDic[new Vector2(-4,-2)], 0);
            Vector2 ite = new Vector2(7.5f, -2.0f);
           for (int i = 0; i < 8; i++)
           {
               Debug.Log("NEW");
               Debug.Log("Position " + i + " " + NodeDic[ite].neighbours[i].position);
               Debug.Log("jump " + i + " " + NodeDic[ite].neighbours[i].jump);
               Debug.Log("value " + i + " " + NodeDic[ite].neighbours[i].value);
               Debug.Log("sloped " + i + " " + NodeDic[ite].neighbours[i].sloped);
           }

        }
        // -value = kan inte gå på denna nod
        public class Node
        {
            public bool jump;
            public Vector3 position;
            public CopyNode[] neighbours = new CopyNode[8];
            public float value;
            public Node parent;
            public Vector2 sloped;
            public Node(Vector3 pos, Vector2 slope)
            {
                this.position = pos;
                this.jump = false;
                this.value = 0;
                this.sloped = slope;
                this.parent = null;
            }
        }

        public struct CopyNode
        {
            public bool jump;
            public Vector3 position;
            public float value;
            public Vector2 sloped;
            public CopyNode(Vector3 pos, Vector2 slope)
            {
                this.position = pos;
                this.jump = false;
                this.value = -1;
                this.sloped = slope;
            }
        }
        public class SearchNode
        {
            public bool jump;
            public Vector3 position;

            public float value;
            public Node parent;
            public SearchNode(Vector3 pos)
            {
                this.position = pos;
                this.jump = false;
                this.value = 0;
            }
        }
    }
    // varje nod håller information om vad du behöver göra för att gå til ldess grannar, gå/hoppa
    // är planet lutande? kolla hörn.
    //kommer till en nod där man måste hoppa? men har y värdet som behövs, faller... kötta x och z medan man faller ( löst?)
    //vector array
    //skriv i analys, nodgenerering clusterfuck
}