using System.Collections.Generic;
using UnityEngine;

namespace Game.Component
{
    public class GMovement : GComponent
    {
        public float _moves;
        public float _gravity;
        public bool _grounded;
        public GMovement()
        {
            
        }
        public static GMovement Make(int entityID)
        {
            GMovement comp = new GMovement();
            comp.EntityID = entityID;
            comp.FamilyID = 5;
            comp._gravity = 0;
            return comp;
        }
    }
}
