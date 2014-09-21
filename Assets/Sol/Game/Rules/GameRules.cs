using UnityEngine;
using System.Collections;

using Sol.Game.States;

namespace Sol.Game.Rules
{
	public class GameRules 
	{
		StateMachine state;

		public StateMachine CurrentState {get {return state;}}

		public GameRules()
		{
			state = new StateMachine(State.MakeStates());
		}

		public void StartGame()
		{
			if (!state.IsAllowed("start game"))
			{
				return;
			}

			state.SetValue("turn", 0);
			
		}

		public bool CanPickUpCard(PlayerInfo player, CardInfo card)
		{
			int turn = state.GetValue<int>("turn");
			bool isPlayersTurn = player.Owner && player.PlayerID == turn;

			bool ret = isPlayersTurn &&
				(card.state == CardInfo.State.Hand ||
				 (card.state == CardInfo.State.Placed &&
				 card.stats.type == CardStats.CardType.Unit)) &&
				 state.IsAllowed("pickup card");
				
		 	return ret;
		}

		public void NextTurn()
		{
			if (!state.IsAllowed("next turn"))
			{
				return;
			}

			state.AddAction("next turn"); 
			state.SetValue("turn", (state.GetValue<int>("turn") + 1) % 2);
		}

		public void PlaceSelected(SystemInfo system)
		{
			if (!state.IsAllowed("place selected"))
			{
				return;
			}

			state.AddAction("place selected");
			CardInfo card = CardInfo.GetByInstanceID(state.GetValue<int>("selected card"));
			state.RemoveValue("selected card");

			card.state = CardInfo.State.Placed;
			card.tile = system.tile;
			system.cards.Add(card);
		}

		public void PickUpCard(CardInfo card, SystemInfo system)
		{
			if (!state.IsAllowed("pickup card"))
			{
				return;
			}

			state.AddAction("pickup card");
			card.state = CardInfo.State.Placed;
			card.tile = system.tile;
			system.cards.Add(card);
		}
	}
}