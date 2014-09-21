using System;
using Sol.Game.Components;
using Sol.Game.Entities;
using Sol.Game.Rules;
using Sol.Game;

namespace Sol.Game.Systems
{
	[
	 RegisterSystem(
		new Type[]
		{		})
	]

	public class MouseCardMover : ISystem
	{
		private Vector2f offset = Vector2f.Zero;
		private Entity selected = null;

		public void UpdateRequired(Entity entity, GameManager game, float delta)
		{

		}

		public void UpdateAll(GameManager game, float delta)
		{
			if (selected != null)
			{
				selected.GetComponent<Position>().position = game.Inputs.Mouse.Position + offset;
				if (game.Inputs.Mouse.Left.Released)
				{
					Place (game, selected, game.Inputs.Mouse.Position);
					selected = null;
				}
			}
			else if (selected == null &&
			    game.Inputs.Mouse.Left.Pressed)
			{
				selected = EntityFinder.GetEntityAt(game.GetEntitiesWith<CardType>(), game.Inputs.Mouse.Position);
				PlayerInfo playerInfo = new PlayerInfo();
				foreach(Entity player in game.GetEntitiesWith<PlayerType>())
				{
					if (player.GetComponent<PlayerType>().info.Owner)
					{
						playerInfo = player.GetComponent<PlayerType>().info;
						break;
					}
				}

				if (selected != null)
				{
					if (!game.Rules.CanPickUpCard(playerInfo, selected.GetComponent<CardType>().info))
					{
						selected = null;
					}
					else
					{
						offset = selected.GetComponent<Position>().position - game.Inputs.Mouse.Position;
					}
				}
			}
		}

		private void Place(GameManager game, Entity selected, Vector2f position)
		{
			selected.GetComponent<Position>().position = position + offset;
			Entity system = EntityFinder.GetEntityAt(game.GetEntitiesWith<SystemType>(), position);
			if (system != null)
			{
				game.Rules.PlaceSelected(system.GetComponent<SystemType>().info);
			}
		}
	}
}

