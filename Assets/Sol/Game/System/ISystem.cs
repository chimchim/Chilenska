using System.Collections.Generic;
using Sol.Game.Components;
using Sol.Game.Entities;

namespace Sol.Game.Systems
{
	/* In order to be registered in and called from the factory
	 * use RegisterSystem attribute, initialized with the list 
	 * of types for the required components for the system, if
	 * any.
	 * The system will be initialized by the factory and
	 * added to the entities complying with the required
	 * compeentes to be called on UpdateRequired. It will
	 * also be added to the GameManager to be called on 
	 * UpdateAll. The same instance will be called in both
	 * cases.
	 */  
	public interface ISystem  
	{
		void UpdateRequired(Entity entity, GameManager game, float delta);
		void UpdateAll(GameManager game, float delta);
	}
}