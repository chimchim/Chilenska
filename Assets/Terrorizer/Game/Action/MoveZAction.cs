using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Component;
namespace Game.Actions
{
    public class MoveZAction : Action
    {
        private static ObjectPool<MoveZAction> _pool = new ObjectPool<MoveZAction>(100);
        private float _dir;
        public MoveZAction()
        {

        }

        public static MoveZAction Make(float z)
        {
            MoveZAction moveZAction = _pool.GetNext();
            moveZAction._dir = z;
            return moveZAction;
        }

        public override void Apply(GameManager game, int owner)
        {
            GMovement movement = game.Entities.GetComponentOf<GMovement>(owner);
            GTransform transform = game.Entities.GetComponentOf<GTransform>(owner);
            RaycastHit2D hit;
            float speed = 10 * Time.deltaTime * _dir;
            Vector3 p = transform._position;
            Vector2 s = transform._bounds;
            bool canMove = true;
            
            for (int i = 0; i < 2; i++)
            {
                float x = (p.x - s.x / 2) + s.x * i;
                float y = p.y - s.y / 2;

                Ray ray1 = new Ray(new Vector3(x, y, p.z), new Vector3(0, 0, 1));
                Debug.DrawRay(ray1.origin, ray1.direction);
                if (Physics.Raycast(new Vector3(x, y, p.z), new Vector3(0, 0, speed), Mathf.Abs(speed) + 0.05f))
                {
                    canMove = false;
                }
            }
            for (int i = 0; i < 2; i++)
            {
                float x = (p.x - s.x / 2) + s.x * i;
                float y = p.y + s.y / 2;

                Ray ray1 = new Ray(new Vector3(x, y, p.z), new Vector3(0, 0, 1));
                Debug.DrawRay(ray1.origin, ray1.direction);
                if (Physics.Raycast(new Vector3(x, y, p.z), new Vector3(0, 0, speed), Mathf.Abs(speed) + 0.05f))
                {
                    canMove = false;
                }
            }
            if (canMove)
                transform._position += (new Vector3(0, 0, speed));
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