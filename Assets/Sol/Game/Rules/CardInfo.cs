using System.Collections;
using Sol.Game.Entities;
using System.Collections.Generic;

namespace Sol.Game.Rules
{
	public class CardInfo 
	{
		private static Dictionary<int, CardInfo> instances = new Dictionary<int, CardInfo>();
		private static int nextID = 0;

		public enum State {None, Placed, Hand};

		public CardStats stats;

		public State state = State.None ; 
		public SystemTile tile = new SystemTile();

		public Entity entity = null;

		public int instanceID = -1;

		private CardInfo(int ID)
		{
			instances.Add(ID, this);
			instanceID = ID;
		}

		public static CardInfo GetByInstanceID(int ID)
		{
			return instances[ID];
		}

		public static CardInfo MakeWithID(int instanceID, string cardStatsName)
		{
			CardInfo ret = new CardInfo(instanceID);
			ret.stats = CardStats.MakeFromName(cardStatsName);
			return ret;
		}

		public static int NewID()
		{
			return nextID++;
		}
	}
}