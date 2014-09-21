using UnityEngine;
using System.Collections;

namespace Game.Component
{
    public class GCollisionMask : GComponent
    {
        // kolla om den träffade collider finns med i listan med colliders?
        public int _layer;
        public static GCollisionMask Make(int entityID)
        {
            GCollisionMask comp = new GCollisionMask();
            comp._layer = 0;
            comp.EntityID = entityID;
            comp.FamilyID = 2;
            return comp;
        }
    }
}