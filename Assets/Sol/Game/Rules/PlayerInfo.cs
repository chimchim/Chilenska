
namespace Sol.Game.Rules
{
	public struct PlayerInfo {

		/*0-1, -1 means invalid*/
		public int PlayerID {get; private set;}
		public bool Owner {get; private set; }
		public int AP {get;set;}

		public PlayerInfo(int playerID, bool owner)
		{
			PlayerID = playerID;
			Owner = owner;
			AP = 0;
		}

		public override string ToString ()
		{
			return string.Format("[PlayerType: PlayerID={0}, Owner={1}]", PlayerID, Owner);
		}
	}
}