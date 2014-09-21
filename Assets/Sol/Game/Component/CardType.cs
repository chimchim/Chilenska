using Sol.Game.Rules;
using Sol.Game.Entities;

namespace Sol.Game.Components
{
	public class CardType : IComponent
	{
		public CardInfo info;

		public CardType (Entity entity, int cardID, string statsName)
		{
			info = CardInfo.MakeWithID(cardID, statsName);
			info.entity = entity;
		}
	}
}

