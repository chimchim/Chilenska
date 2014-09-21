using System;

using Sol.Game.Components;

namespace Sol.Game.Entities
{
	public class EntityFactory
	{
		public static Vector2f cardSize = new Vector2f(0.62f, 0.88f);
		public static Vector2f systemSize = new Vector2f(cardSize.x * 4, cardSize.y * 3);
		public static Vector2f systemSpacing = new Vector2f(cardSize.x, cardSize.y);

		public static Entity MakeCard(int cardID, string statsName)
		{
			Entity ret = new Entity();
			
			ret.AddComponent(new Position(0,0,1));
			ret.AddComponent(new BoundingRectangle(-cardSize.x / 2, -cardSize.y / 2, cardSize.x / 2, cardSize.y / 2));
			ret.AddComponent(new CardType(ret, cardID, statsName));

			return ret;
		}

		public static Entity MakeSystem(int tileX, int tileY)
		{
			Entity ret = new Entity();

			Vector2f offset = (systemSize + systemSpacing) * 0.5f;
			/* system at (0,0) should be place on (0,0) */
			float x = (systemSpacing.x + systemSize.x) * ((float)tileX + 0.5f);
			float y = (systemSpacing.y + systemSize.y) * ((float)tileY - 0.5f);
			
			Vector2f position = offset + new Vector2f(-x, y);

			ret.AddComponent(new BoundingRectangle(-systemSize.x / 2, -systemSize.y / 2, systemSize.x / 2, systemSize.y / 2));
			ret.AddComponent(new SystemType(ret ,tileX, tileY));

			ret.AddComponent(new Position(position.x,position.y,0));

			return ret;
		}

	}
}

