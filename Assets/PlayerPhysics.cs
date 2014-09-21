using UnityEngine;
using System.Collections;


[RequireComponent (typeof(BoxCollider2D))]
public class PlayerPhysics : MonoBehaviour {
	
	public LayerMask collisionMask;

	private BoxCollider2D collider;
	private Vector3 s;
	private Vector3 c;
	
	private float skin = .005f;
	
	[HideInInspector]
	public bool grounded;
	
	Ray2D ray;
	RaycastHit2D hit;
	
	void Start() {
		collider = GetComponent<BoxCollider2D>();
		s = collider.size;
		c = collider.center;
	}

	public void Move(Vector2 moveAmount) {
		
		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		Vector2 p = transform.position;
		
		// Check collisions above and below
		grounded = false;
		
		for (int i = 0; i<3; i ++) {
			float dir = Mathf.Sign(deltaY);
			float x = (p.x + c.x - s.x/2) + s.x/2 * i; // Left, centre and then rightmost point of collider
			float y = p.y + c.y + s.y/2 * dir; // Bottom of collider

			ray = new Ray2D(new Vector2(x,y), new Vector2(0,dir));
			Debug.DrawRay(ray.origin,ray.direction);
			hit = Physics2D.Raycast(new Vector2(x,y), new Vector2(0,dir) , Mathf.Abs(deltaY),collisionMask);

            if (Physics2D.Raycast(new Vector2(x, y), new Vector2(0, dir), Mathf.Abs(deltaY), collisionMask))
            {
				// Get Distance between player and ground
				float dst = Vector3.Distance (ray.origin, hit.point);
				
				// Stop player's downwards movement after coming within skin width of a collider
				if (dst > skin) {
					deltaY = dst * dir + skin;
				}
				else {
					deltaY = 0;
				}
				
				grounded = true;
				
				break;
				
			}
		}
	
		Vector2 finalTransform = new Vector2(deltaX,deltaY);
		
		transform.Translate(finalTransform);
	}
	
}
/*
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.GEntity;
using Game.Component;

namespace Game.System
{
    public class GPhysicsSystem : GSystem
    {

        private float _skin = 0.5f;
        public override void Update(GameManager game, float delta)
        {
            float gravity = -1f;
            foreach (int entity in _entityList)
            {
               
                float deltaY = 0;
                GMovement movement = game.Entities.GetComponentOf<GMovement>(entity);
                
                GTransform transform = game.Entities.GetComponentOf<GTransform>(entity);
               
                Vector2 dir = new Vector2(0, Mathf.Sign(gravity + movement._gravity));
                RaycastHit2D right = Physics2D.Raycast(new Vector2(transform._bounds.x+transform._position.x, transform._position.y + (dir.y*transform._bounds.y)), dir, 500);
                RaycastHit2D left = Physics2D.Raycast(new Vector2(-transform._bounds.x + transform._position.x, transform._position.y + (dir.y * transform._bounds.y)), dir, 500);

                Debug.DrawLine(new Vector3(transform._bounds.x + transform._position.x, transform._position.y + (dir.y * transform._bounds.y), 0), right.point);
                Debug.DrawLine(new Vector3(-transform._bounds.x + transform._position.x, transform._position.y + (dir.y * transform._bounds.y), 0), left.point);

                float dst = Mathf.Min(500-right.distance, 500-left.distance);

                movement._gravity -= Time.deltaTime * 0.05f;
                deltaY = movement._gravity;
                if (dst > 0.1f)
                {
                    
                    movement._gravity -= Time.deltaTime * 0.05f;
                    deltaY = movement._gravity;
                    Debug.Log("IF SATS " + dst);
                }
                else
                {
                    Debug.Log("ELSE");
                    //deltaY = dst - 0.1f;
                    movement._gravity = 0;
                    deltaY = 0;
                }
               /* Debug.Log("Gravity1");
                if (Mathf.Abs(movement._gravity) > _skin)
                {
                    Debug.Log("Gravity2");
                    deltaY = movement._gravity;
                }
                else
                {
                    Debug.Log("Gravity3");
                    deltaY = 0;
                }
                foreach (Vector3 vec in movement._moves)
                {
                    transform._position += vec;
                }
                transform._position += new Vector3(0, deltaY, 0);
                movement._moves.Clear();
            }
        }

        public override void InitSystems(GameManager game)
        {

        }
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
        private float maxMove = 5.0f;
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
            float move = 1;
            int[] inter = { -1, 1, 1, -1, -1 };
            GMovement movement = game.Entities.GetComponentOf<GMovement>(owner);
            Vector3 P = game.Entities.GetComponentOf<GTransform>(owner)._position;
            Vector2 B = game.Entities.GetComponentOf<GBoxCollider2D>(owner)._bounds;

            if (_depth)
            {
            
                RaycastHit2D down = Physics2D.Raycast(new Vector2((_direction.x * B.x) + P.x, -B.y + P.y), _direction, 500);
                RaycastHit2D top = Physics2D.Raycast(new Vector2((_direction.x * B.x) + P.x, B.y + P.y), _direction, 500);
                float dst = (Mathf.Min(down.distance, top.distance));
                 if (dst > maxMove * Time.deltaTime)
                 {
                       move = maxMove * Time.deltaTime;
                 }
                  
                 else if (dst > (maxMove * Time.deltaTime) / maxMove * 0.1f)
                 {
                      Debug.Log("MoveElse");
                      move = dst - (maxMove * Time.deltaTime) / maxMove * 0.1f;
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
}
*/