using System;
using System.Reflection;
using System.Collections.Generic;

using Sol.Game.Entities;

namespace Sol.Game.Systems
{
	public class RegisterSystem : Attribute
	{
		public Type[] Required {get {return required;}}
		private Type[] required = null;
		
		public RegisterSystem(Type[] requiredComponents)
		{
			required = requiredComponents;
		}
	}

	public class Factory
	{
		public static List<ISystem> MakeSystems(List<Entity> entities)
		{
			List<ISystem> systems = new List<ISystem>();

			foreach(Type system in GetSystems())
			{
				ISystem systemObject = (ISystem)Activator.CreateInstance(system);
				systems.Add(systemObject);

				foreach(Entity entity in entities)
				{
					foreach(RegisterSystem register in system.GetCustomAttributes(true))
					{
						bool allRequired = true;
						foreach(Type type in register.Required)
						{
							MethodInfo method = typeof(Entity).GetMethod("GetComponent");
							method = method.MakeGenericMethod(type);
							if (method.Invoke(entity, new Object[]{}) == null)
							{
								allRequired = false;
								break;
							}
						}
						
						if (allRequired)
						{
							MethodInfo method = typeof(Entity).GetMethod("AddSystem");
							method.Invoke(entity, new Object[]{systemObject});
						}
					}
				}
			}

			return systems;
		}

		private static IEnumerable<Type> GetSystems() 
		{
			foreach(Type type in Assembly.GetAssembly(typeof(Factory)).GetTypes()) 
			{
				if (type.GetCustomAttributes(typeof(RegisterSystem), true).Length > 0) {
					yield return type;
				}
			}
		}
	
	}
}

