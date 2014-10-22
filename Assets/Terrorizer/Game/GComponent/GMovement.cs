using System.Collections.Generic;
using UnityEngine;

namespace Game.Component
{
    public class GMovement : GComponent
    {
        public float _targetSpeed;
        public float _currentSpeed;
        public float _gravity;
        public bool _grounded;
        public Vector3 _moveVector;

        public GMovement()
        {
            
        }
        public static GMovement Make(int entityID)
        {
            GMovement comp = new GMovement();
            comp.EntityID = entityID;
            comp.FamilyID = 5;
            return comp;
        }
    }
}
