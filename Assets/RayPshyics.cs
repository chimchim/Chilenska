using UnityEngine;
using System.Collections;

public class RayPshyics : MonoBehaviour {

	private RaycastHit2D gravityRay;
	private BoxCollider2D boxCollider2D;
	private Phy feaf;
	void Start () 
	{
		boxCollider2D = GetComponent<BoxCollider2D>();
		feaf = new Phy ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		feaf.dpdate ();
		//gravityRay = Physics2D.Raycast(transform.position, -Vector2.up);
		//Debug.Log("Hitlayer " + (gravityRay.distance-100000f));
		//Debug.Log("Hitlayer " + " "+ boxCollider2D.bounds.extents + (boxCollider2D.size.x+ transform.position.x) +" "+ (boxCollider2D.size.y+ transform.position.y));
	}
}
/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Component;
namespace Game.Actions
{
    public class MoveAction : Action
    {
        private static ObjectPool<MoveAction> _pool = new ObjectPool<MoveAction>(100);
        private Vector3 _direction;
        private bool _depth;
        private float maxMove = 3.0f;
        public MoveAction()
        {

        }

        public static MoveAction Make(Vector2 dir, bool depth)
        {
            MoveAction move = _pool.GetNext();
            move._direction = dir;
            move._depth = depth;
            return move;
        }

        public override void Apply(GameManager game, int owner)
        {
            float move = 0;
            int[] inter = { -1, 1, 1, -1, -1 };
            GMovement movement = game.Entities.GetComponentOf<GMovement>(owner);
            Vector3 P = game.Entities.GetComponentOf<GTransform>(owner)._position;
            Vector2 B = game.Entities.GetComponentOf<GTransform>(owner)._bounds;

            if (_depth)
            {

                RaycastHit2D down = Physics2D.Raycast(new Vector2((_direction.x * B.x) + P.x, -B.y + P.y), _direction, 500);
                RaycastHit2D top = Physics2D.Raycast(new Vector2((_direction.x * B.x) + P.x, B.y + P.y), _direction, 500);
                float dst = (Mathf.Min(down.distance, top.distance));
                float multi = maxMove * Time.deltaTime;
 
                if (dst - 0.1f > maxMove * multi)
                {
                    Debug.Log("Straight");
                    move = maxMove * multi;
                }

                else if (movement.angled)
                {
                    move = maxMove * multi;
                    _direction = down.transform.right;
                    Debug.Log("Angled ");
                }

                else if (dst > 0.11f)
                {
                    Debug.Log("Fronted");
                    if (down.normal.y > 0.4f)
                    {
                        movement.angled = true;
                    }
                    move = dst - 0.1f;
                }
                //Debug.Log("Velocity " + new Vector3(_direction.x, _direction.y, 0).magnitude * move * Time.deltaTime * 50);
                game.Entities.GetComponentOf<GTransform>(owner)._position += new Vector3(_direction.x, _direction.y, 0) * move;

            }
            // if (_depth)
            // {
            // 
            //     for (int i = 0; i < 2; i++)
            //     {
            //         RaycastHit2D hit = Physics2D.Raycast(
            //         new Vector2((_direction.x * B.x) + P.x, (inter[i] * B.y) + P.y), _direction, 20);
            //         if (hit.distance > 0.1f && hit)
            //         {
            //             
            //             Debug.DrawLine(new Vector2((_direction.x * B.x) + P.x, (inter[i] * B.y) + P.y), hit.point);
            //             Debug.Log("HitPoint " + hit.point  + " " + hit.transform.gameObject.layer + " Dot " + new Vector2(hit.normal.x*-1, hit.normal.y));
            // 
            //         }
            //     }
            // }
            // else
            // {
            //     for (int i = 0; i < 4; i++)
            //     {
            //         RaycastHit2D hit = Physics2D.Raycast(
            //         new Vector2((inter[i] * B.x) + P.x, (inter[i+1] * B.y) + P.y), _direction, 20, 10);
            //         
            //         if (hit != null)
            //         {
            //             Debug.Log("Normal " + hit.normal);
            //         }
            //     }
            // }

        }

        public override void Recycle()
        {
            _pool.Recycle(this);
        }

        public override string Type()
        {
            return "MoveAction";
        }
    }
}*/