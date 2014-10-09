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
            float gravity = -0.01f;
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


                if (0.2f > dst)
                {

                    movement._grounded = true;
                    deltaY = 0.2f - dst;
                }
                
                else if (!movement._grounded)
                {
                    movement._grounded = false;
                    movement._gravity -= Time.deltaTime * 0.1f;
                    deltaY = movement._gravity;
                }
                else if (dst > 0.3f)
                {
                    movement._grounded = false;
                }

                if (right.distance >= left.distance && .25f > dst)
                {
                    if (left.normal.normalized == new Vector2(left.transform.right.x, left.transform.right.y).normalized)
                    {
                        Debug.Log("left " + left.transform.right + " " + left.normal + " " + left.normal.y);
                        Debug.Log("Drill");
                        transform._position += new Vector3(left.transform.right.x, -left.transform.right.y, 0) * movement._moves;
                    }
                    else
                    {
                        transform._position += left.transform.right * movement._moves;
                    }
                }
                else if (.25f > dst)
                {
                    transform._position += right.transform.right * movement._moves;
                }
                else
                {
                    transform._position += Vector3.right * movement._moves;
                }

                //transform._position += Vector3.right*movement._moves;
                
                movement._moves = 0;

                transform._position += new Vector3(0, deltaY, 0);

            }
        }

        public override void InitSystems(GameManager game)
        {

        }
    }
}