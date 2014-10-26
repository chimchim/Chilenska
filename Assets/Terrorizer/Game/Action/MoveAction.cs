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
        private float _direction;
        private float maxMove = 2.0f;
        public MoveAction()
        {

        }

        public static MoveAction Make(float dir)
        {
            MoveAction move = _pool.GetNext();
            move._direction = dir;
            return move;
        }

        public override void Apply(GameManager game, int owner)
        {

            GMovement movement = game.Entities.GetComponentOf<GMovement>(owner);
            Vector3 P = game.Entities.GetComponentOf<GTransform>(owner)._position;
            Vector2 B = game.Entities.GetComponentOf<GTransform>(owner)._bounds;
            movement._targetSpeed = Mathf.Sign(_direction) * 4;

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