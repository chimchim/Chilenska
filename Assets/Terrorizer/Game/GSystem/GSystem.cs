using System.Collections;
using System.Collections.Generic;

namespace Game.System
{
	public abstract class GSystem
	{
        protected List<int> _entityList = new List<int>();

		public abstract void Update(GameManager game, float delta);
        public abstract void InitSystems(GameManager game);
        public void AddEntity(int entity)
        {
            _entityList.Add(entity);
        }
	}
}