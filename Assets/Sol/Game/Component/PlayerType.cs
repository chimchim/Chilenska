using Sol.Game.Components;
using Sol.Game.Rules;

public class PlayerType : IComponent 
{
	public PlayerInfo info;

	public PlayerType(PlayerInfo info)
	{
		this.info = info;
	}
}
