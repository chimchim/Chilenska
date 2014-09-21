using UnityEngine;
using System.Collections;
using Sol.Game.Entities;
using Sol.Game.Components;
using Sol.Game;
using Sol.Game.Rules;

namespace Sol.Unity
{
	public class Initator : MonoBehaviour 
	{
		GameManager game = GameManager.Instance;
		EntityRenderer render = new EntityRenderer();
		void Start () 
		{
			Entity e = new Entity();

			Entity player1 = new Entity();
			Entity player2 = new Entity();
			player1.AddComponent(new PlayerType(new PlayerInfo(0, true)));
			player2.AddComponent(new PlayerType(new PlayerInfo(1, false)));

			game.AddEntity(player1);
			game.AddEntity(player2);
			Entity card = EntityFactory.MakeCard(CardInfo.NewID(), "test");
			card.GetComponent<CardType>().info.state = CardInfo.State.Hand;
			game.AddEntity(card);

			card = EntityFactory.MakeCard(CardInfo.NewID(), "test");
			card.GetComponent<CardType>().info.state = CardInfo.State.Hand;
			game.AddEntity(card);

			card = EntityFactory.MakeCard(CardInfo.NewID(), "test");
			card.GetComponent<CardType>().info.state = CardInfo.State.Hand;
			game.AddEntity(card);

			game.AddEntity(EntityFactory.MakeSystem(0,0));
			game.AddEntity(EntityFactory.MakeSystem(0,1));

			game.InitateSystems();
		}

		void Update()
		{
			game.Update(Time.deltaTime);
			render.Render(game.Entities);
		}
	}
}