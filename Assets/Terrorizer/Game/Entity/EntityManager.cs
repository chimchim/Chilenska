using System.Collections;
using System.Collections.Generic;
using Game.Misc;
using Game.Component;

namespace Game.GEntity
{
	public class EntityManager 
	{
		private Dictionary<int, Entity> _entities = new Dictionary<int, Entity>();
        public List<Entity> _entityList = new List<Entity>();

		public void addEntity(Entity entity)
		{
			if(!_entities.ContainsKey(entity.ID))
			{
				_entities.Add(entity.ID, entity);
			}
			else
			{
				Debugger.Error("Could not add entity, ID Taken!");
			}
		}

        public T GetComponentOf<T>(int entity) where T : GComponent
        {

            return _entities[entity].GetComponent<T>();
        }

        public bool HasComponent(int entityid, int familiyid)
        { 
            return _entities[entityid].HasComponent(familiyid);
        }
	}
}
