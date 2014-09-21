using System;
using Sol.Game;

namespace Sol.Game.Rules
{
	public class CardStats
	{
		public enum CardType 
		{
			None,
			Unit,
			Building,
			Planet
		}

		public struct Resources
		{
			public int Ore {get; private set;}
			public int Metal {get; private set;}
			public int Food {get; private set;}
			public int Energy {get; private set;}

			public Resources(int ore, int metal, int food, int energy)
			{
				Ore = ore;
				Metal = metal;
				Energy = energy;
				Food = food;
			}

			public bool Covers(Resources other)
			{
				return 
						Ore <= other.Ore &&
						Metal <= other.Metal &&
				 		Energy <= other.Energy &&
				 		Food <= other.Food;
			}

			public static Resources operator+(Resources lhs, Resources rhs)
			{
				return new Resources(
						lhs.Ore + rhs.Ore,
						lhs.Metal + rhs.Metal,
						lhs.Food + rhs.Food,
						lhs.Energy + rhs.Energy
					);
			}
		}

		public CardType type = CardType.None;

		int ap = 0;
		int attack = 0;
		int defence = 0;
		Resources resources;

		private CardStats()
		{

		}

		public static CardStats MakeFromName(string name)
		{
/*			JSONObject node = JSONFile.File["cards.json"];
			node = node[name];
			CardStats ret = new CardStats();
			ret.ap = int.Parse(node["ap"].str);
			ret.attack = int.Parse(node["attack"].str);
			ret.defence = int.Parse(node["defence"].str);
			
			//JSONObject resources = node["resources"];
			int food = int.Parse(resources["food"].str);
			int ore  = int.Parse(resources["ore"].str);
			int metal = int.Parse(resources["metal"].str);
			int energy = int.Parse(resources["energy"].str);
			
			ret.resources = new Resources(ore, metal, food, energy);
*/
			return null;
		}

		
	}
}

