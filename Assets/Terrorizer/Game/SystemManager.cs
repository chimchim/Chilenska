using System.Collections;
using System.Collections.Generic;
using Game.System;

namespace Game
{
    public class SystemManager
    {
		private Dictionary<int,GSystem> _systems = new Dictionary<int, GSystem>();
		
		public void UpdateAll(GameManager game, float delta)
		{
			foreach(GSystem system in _systems.Values)
			{
				system.Update(game, delta);
			}
		}

        public void AddEntity(int system, int entity)
        {
            _systems[system].AddEntity(entity);
        }

        public void CreateSystems()
        {
            _systems.Add(0,new GInputSystem());
            _systems.Add(1, new GPhysicsSystem());
        }
        public void InitAll(GameManager game)
        {
            foreach (GSystem system in _systems.Values)
            {
                system.InitSystems(game);
            }
        }
        
    }
}