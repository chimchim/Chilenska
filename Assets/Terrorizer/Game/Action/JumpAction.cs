using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Component;
namespace Game.Actions
{
    public class JumpAction : Action
    {
        private static ObjectPool<JumpAction> _pool = new ObjectPool<JumpAction>(100);

        public JumpAction()
        {

        }

        public static JumpAction Make()
        {
            JumpAction jumpAction = _pool.GetNext();

            return jumpAction;
        }

        public override void Apply(GameManager game, int owner)
        {

            game.Entities.GetComponentOf<GMovement>(owner)._gravity = 0.05f;      
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