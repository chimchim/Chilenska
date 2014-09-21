using UnityEngine;
using System.Collections;

namespace Game.Component
{
	public class GRawInput : GComponent
	{
		public bool _w;
		public bool _a;
		public bool _d;
		public bool _s;
		public bool _space;

		public bool _fire;

		public GRawInput()
		{
			
		}
		public static GRawInput Make(int entityID)
		{
			GRawInput comp = new GRawInput ();
			comp.EntityID = entityID;
			comp.FamilyID = 1;
			return comp;
		}
	}
}