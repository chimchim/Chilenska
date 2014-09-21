using System.Collections.Generic;
using Sol.Game.Entities;

namespace Sol.Game.Rules
{
	public class SystemInfo
	{
		public SystemTile tile = new SystemTile();	
		public List<CardInfo> cards = new List<CardInfo>();
		public Entity entity = null;

		public SystemInfo(int x, int y)
		{
			tile.X = x;
			tile.Y = y;
		}

	}
}