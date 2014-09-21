using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Component;
using Game.Misc;

namespace Game.GEntity
{
	public class Entity
	{
		public int ID{get {return _id;}}
		public GameObject gameObject{get {return _gameObject;}}

		private int _id;
		private GameObject _gameObject;
	
		private Dictionary<int,GComponent> _components = new Dictionary<int,GComponent>();
		
		public Entity()
		{
			_id = IDGiver.GetNewID();
		}
		
		public void AddComponent<T>(T component) where T:GComponent
		{
			if(!_components.ContainsKey(component.FamilyID))
			{
				_components.Add(component.FamilyID, component);
			}
		}

        public T GetComponent<T>() where T : GComponent
        {
            foreach (GComponent component in _components.Values)
            {
                if (component is T)
                {

                    return (T)component;
                }
            }
            return null;
        }
		//public GComponent GetComponent(int familiyid)
		//{
        //    if (!_components.ContainsKey(familiyid))
		//	{
		//		return null;
		//	}
        //    return _components[familiyid];
		//}

        public bool HasComponent(int familiyid)
        {
            return _components.ContainsKey(familiyid);
        }
	}
}
