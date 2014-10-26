using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Game.Component
{
    public class GAstarComponent : GComponent
    {

        public Dictionary<Vector3, bool> _path;
        public bool _active;
        public Vector2 _goal;
        public Vector3 _current;
        public int _index;
        public GAstarComponent()
        {

        }
        public static GAstarComponent Make(int entityID)
        {
            GAstarComponent comp = new GAstarComponent();
            comp.EntityID = entityID;
            comp._goal = new Vector2(36f, 2.5f); 
            comp._active = true;
            comp.FamilyID = 6;
            comp._index = 0;
            comp._path = new Dictionary<Vector3, bool>();
            return comp;
        }
    }
}