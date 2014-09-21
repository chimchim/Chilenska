using System;
using Sol.Game.Entities;

namespace Sol.Game.Systems
{
	[
	 RegisterSystem(
		new Type[]
		{		})
	 ]

	public class StartGame : ISystem
	{
		public StartGame ()
		{
		}

		public void UpdateRequired(Entity entity, GameManager game, float delta)
		{

		}

		public void UpdateAll(GameManager game, float delta)
		{
			game.Rules.StartGame();	
		}
	}
}

