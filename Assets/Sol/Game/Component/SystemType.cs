using Sol.Game.Rules;
using Sol.Game.Entities;

namespace Sol.Game.Components
{
	public class SystemType : IComponent
	{
		public SystemInfo info;
		
		public SystemType(Entity entity, int x, int y)
		{
			info = new SystemInfo(x, y);
			info.entity = entity;
		}
	}
}

