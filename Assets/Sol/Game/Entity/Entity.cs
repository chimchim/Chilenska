using System.Collections.Generic;
using Sol.Game.Components;
using Sol.Game.Systems;
using System.Collections;

namespace Sol.Game.Entities
{
	/* A little simplictic ECS goes a long way 
	 * Add components and systems will be added 
	 * based on the list of components based on 
	 * the components.
	 */
	public class Entity {

		public int ID{get {return id;}}
		private int id;

		private List<IComponent> components = new List<IComponent>();

		public Entity()
		{
			//id = IDGiver.GetNewID();
		}

		~Entity()
		{
			//IDGiver.FreeID(id);
		}

		public void AddComponent<T>(T component) where T: class, IComponent
		{
			if (GetComponent<T>() == null)
				components.Add(component);		
		}

		public T GetComponent<T>() where T: class, IComponent
		{
			foreach (IComponent component in components)
			{
				if (component is T)
				{

					return (T)component;
				}
			}
			return null;
		}

		public void Update(GameManager game, float delta)
		{
		}

	}
}