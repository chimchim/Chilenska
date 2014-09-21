using System;
using System.Collections.Generic;
using Sol.Game.Entities;
using Sol.Game.Components;
using Sol.Game.Rules;

namespace Sol.Game.Systems
{
	[
	 RegisterSystem(
		new Type[]{
		typeof(SystemType)
	})]
	/* Places cards that are placed in a system in a sorted order in 
	 * some position along the system card
	 */
	public class SystemCardSorter : ISystem
	{
		public void UpdateRequired(Entity entity, GameManager game, float delta)
		{
			SystemInfo system = entity.GetComponent<SystemType>().info;
			int index = 0;
			foreach(CardInfo card in system.cards)
			{
				Vector2f offset = CalculateOffset(index, system.cards.Count);
				card.entity.GetComponent<Position>().position = system.entity.GetComponent<Position>().position + offset;
				++index;
			}
		}
		
		public void UpdateAll(GameManager game, float delta)
		{
		
		}

		private Vector2f CalculateOffset(int index, int total)
		{
			Vector2f ret = new Vector2f();
			ret.y = ((EntityFactory.systemSize.y / 2) - EntityFactory.cardSize.y / 2);
			float middleOffset = (total - 1) * (EntityFactory.cardSize.x / 2);
			ret.x = (middleOffset) - EntityFactory.cardSize.x * index;
			return ret;
		}
			
	}
}

