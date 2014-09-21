using System.Collections;
using Game.Misc;
using UnityEngine;

namespace Game.Component
{
	public class GTransform : GComponent
	{
		public Vector3 _position;
        public Vector2 _bounds;

		public GTransform()
		{

		}
        public static GTransform Make(int entityID, Vector3 position, Vector2 bounds)
		{
			GTransform comp = new GTransform ();
			comp.EntityID = entityID;
            comp._bounds = bounds;
            comp.FamilyID = 0;
			comp._position = position;
			return comp;
		}
	}
}
