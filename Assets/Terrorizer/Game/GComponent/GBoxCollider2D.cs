using UnityEngine;
using System.Collections;

namespace Game.Component
{
    public class GBoxCollider2D : GComponent
    {

        public Vector2 _size;
        public Vector2 _center;
        public Vector2 _bounds;

        public GBoxCollider2D()
        {

        }
        public static GBoxCollider2D Make(int entityID, Vector2 size, Vector2 center, Vector2 bounds)
        {
            GBoxCollider2D comp = new GBoxCollider2D();
            comp._size = size;
            comp._bounds = bounds;
            comp._center = center;
            comp.EntityID = entityID;
            comp.FamilyID = 3;
            return comp;
        }
    }
}