using System;
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
        private float maxMove = 9.0f;
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
                    move =  multi;
                }

                else if (dst > 0.11f)
                {
                    move = dst - 0.1f;
                }
                movement._moves.Add(new Vector3(_direction.x, _direction.y, 0) * move);
                //game.Entities.GetComponentOf<GTransform>(owner)._position += new Vector3(_direction.x, _direction.y, 0) * move;

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