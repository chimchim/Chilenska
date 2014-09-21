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

                if (dst > Mathf.Abs(movement._gravity-Time.deltaTime)+0.4f)
                {
                    Debug.Log("IF " +dst);
                    movement._gravity -= Time.deltaTime*0.1f;
                    deltaY = movement._gravity;
                }
                    sd
                else
                {
                    Debug.Log("Else");
                    movement._gravity = 0;
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