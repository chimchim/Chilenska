using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.GEntity;
using Game.Component;

namespace Game.System
{
    public class Astar : GSystem
    {

        // -8 15 -4
        // -8 15 14
        // 32 15 -4
        // 32 15 14
        public override void Update(GameManager game, float delta)
        {

  

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
            Node[] nodes = {new Node(),
                              new Node(),
                              new Node(),
                              new Node(),
                              new Node(),
                              new Node(),
                              new Node(),
                              new Node()
                          };
            for(int i = 0; i < 360; i++)
            {
        
                for (int j = 0; j < 30; j++)
                {
                    if (Physics.Raycast(currentPosition, Vector3.down, out hit, 1000))
                    {
                       
                        if (hit.normal.y == 1f)
                        {

                            Node n = new Node(hit.point, nodes);
                            n.position = hit.point;
                            NodeDic.Add(new Vector2(currentPosition.x, currentPosition.z), n);
                            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            go.transform.position = hit.point;
                            go.transform.localScale *= 0.05f;
                        }
                        else if (0.3f < hit.normal.y)
                        {
                            Node n = new Node(hit.point, nodes);
                            n.position = hit.point;

                            NodeDic.Add(new Vector2(currentPosition.x, currentPosition.z), n);
                            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            go.transform.position = hit.point;
                            go.transform.localScale *= 0.05f;

                            
                        }
                    }
                    currentPosition.z += 0.5f;
                    
                }
                
                currentPosition.z = -2f;
                currentPosition.x += 0.5f;

            }
            
            // Create Node relations
            foreach (Node n in NodeDic.Values)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 temp = new Vector2(n.position.x + getX[i].x, n.position.z + getX[i].y);
                    if (NodeDic.ContainsKey(temp))
                    {
                        n.neighbours[i] = NodeDic[temp];
                    }
                    else
                    {
                        for (int j = 2; j < 12; j++)
                        {
                            if (NodeDic.ContainsKey(new Vector2(n.position.x + getX[i].x * j, n.position.z + getX[i].y * j)))
                            {

                                n.neighbours[i] = NodeDic[new Vector2(n.position.x + getX[i].x * j, n.position.z + getX[i].y * j)];
                                break;
                            }
                        }
                    }
                }
                for(int i = 0; i < 6; i++)
                {
                    Vector2 temp = new Vector2(n.position.x + getN[i].x, n.position.z + getN[i].y);
                    if (NodeDic.ContainsKey(temp))
                    {
                        n.neighbours[i] = NodeDic[temp];
                    }
                    else
                    {
                        for (int j = 2; j < 12; j++)
                        {
                            if (NodeDic.ContainsKey(new Vector2(n.position.x + getN[i].x * j, n.position.z + getN[i].y * j)))
                            {

                                n.neighbours[i] = NodeDic[new Vector2(n.position.x + getN[i].x * j, n.position.z + getN[i].y * j)];
                                break;
                            }
                        }   
                    }   
                }
            }

            Debug.Log(NodeDic[new Vector2(26.5f, 3.5f)].neighbours[4].position);
            //NodeDic[new Vector2(26.5f, 3.5f)].neighbours[4].position = new Vector3(0, 0, 0);
            Debug.Log(NodeDic[new Vector2(30.5f, 3.5f)].position + " " + NodeDic[new Vector2(26.5f, 3.5f)].neighbours[4].position);
            //Debug.Log(
        }
       
        public struct Node
        {
            public bool jump;
            public Vector3 position;
            public Node[] neighbours;
            public float value;
            public Node(Vector3 pos, Node[] de): this()
            {
                position = pos;
                neighbours = de;
                jump = false;
                value = 0;
            }
        }
        
    }
    // varje nod håller information om vad du behöver göra för att gå til ldess grannar, gå/hoppa
    // är planet lutande? kolla hörn.
    //kommer till en nod där man måste hoppa? men har y värdet som behövs, faller... kötta x och z medan man faller ( löst?)
    
}