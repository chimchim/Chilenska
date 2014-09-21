using System.Collections.Generic;
using System.Collections;

using Sol.Game.Entities;
using Sol.Game;
using Sol.Game.Systems;
using Sol.Game.States;
using Sol.Game.Rules;

namespace Sol.Game
{
	public class GameManager
	{
		private List<Entity> entities = new List<Entity>();
		private List<ISystem> systems = new List<ISystem>();
		private InputListener inputs = new InputListener();

		public GameRules Rules {get;private set;}

		static private GameManager instance = new GameManager();
		static public GameManager Instance {get{return instance;}}

		public List<Entity> Entities 
		{
			get 
			{
				return entities;
			}
		}

		public InputListener Inputs
		{
			get{return inputs;}
		}
		
		private GameManager()
		{
			InputManager.Instance.RegisterMouseListener(inputs.Mouse);
			Rules = new GameRules();
		}

		public void AddEntity(Entity entity)
		{
			entities.Add(entity);
		}

		public void InitateSystems()
		{
			systems = Systems.Factory.MakeSystems(entities);
		}

		public void AddSystem(ISystem system)
		{
			systems.Add(system);
		}

		public void Update(float delta)
		{
			foreach (Entity entity in entities)
			{
				entity.Update(instance, delta);
			}
			foreach (ISystem system in systems)
			{
				system.UpdateAll(instance, delta);
			}
			inputs.Mouse.Update();
			Rules.CurrentState.Ponder(delta);
		}

		public List<Entity> GetEntitiesWith<T>() where T: class, Components.IComponent
		{
			List<Entity> ret = new List<Entity>();
			foreach (Entity entity in entities)
			{
				if (entity.GetComponent<T>() != null)
				{
					ret.Add(entity);
				}
			}
			return ret;
		}

		public List<Entity> GetEntitiesWith<T, U>() 
			where T: class, Components.IComponent
			where U: class, Components.IComponent
		{
			List<Entity> ret = new List<Entity>();
			foreach (Entity entity in entities)
			{
				if (entity.GetComponent<T>() != null
				    && entity.GetComponent<U>() != null)
				{
					ret.Add(entity);
				}
			}
			return ret;
		}

		public List<Entity> GetEntitiesWith<T, U, V>()
			where T: class, Components.IComponent
			where U: class, Components.IComponent
			where V: class, Components.IComponent
		{
			List<Entity> ret = new List<Entity>();
			foreach (Entity entity in entities)
			{
				if (entity.GetComponent<T>() != null
				    && entity.GetComponent<U>() != null
					&& entity.GetComponent<V>() != null)
				{
					ret.Add(entity);
				}
			}
			return ret;
		}

		public List<Entity> GetEntitiesWith<T1, T2, T3, T4>()
			where T1: class, Components.IComponent
			where T2: class, Components.IComponent
			where T3: class, Components.IComponent
			where T4: class, Components.IComponent
		{
			List<Entity> ret = new List<Entity>();
			foreach (Entity entity in entities)
			{
				if (entity.GetComponent<T1>() != null
				    && entity.GetComponent<T2>() != null
				    && entity.GetComponent<T3>() != null
				    && entity.GetComponent<T4>() != null)
				{
					ret.Add(entity);
				}
			}
			return ret;
		}

		public List<Entity> GetEntitiesWith<T1, T2, T3, T4, T5>()
			where T1: class, Components.IComponent
			where T2: class, Components.IComponent
			where T3: class, Components.IComponent
			where T4: class, Components.IComponent
			where T5: class, Components.IComponent
		{
			List<Entity> ret = new List<Entity>();
			foreach (Entity entity in entities)
			{
				if (entity.GetComponent<T1>() != null
				    && entity.GetComponent<T2>() != null
				    && entity.GetComponent<T3>() != null
				    && entity.GetComponent<T4>() != null
				    && entity.GetComponent<T5>() != null)
				{
					ret.Add(entity);
				}
			}
			return ret;
		}
	}
}