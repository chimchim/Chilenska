using System;
using System.Collections.Generic;
using Game.GEntity;

namespace Game
{
	public class GameManager
	{
        private EntityManager _entityManager = new EntityManager();
		private SystemManager _systemManager = new SystemManager();


        public EntityManager Entities { get { return _entityManager; } }
		public SystemManager Systems { get { return _systemManager; } }

		public GameManager()
		{
            
		}

        public void Update(float delta)
        { 
			_systemManager.UpdateAll(this, delta);
        }

		public void Initiate()
		{
            _systemManager.InitAll(this);
		}
	}
}
