using System;
using Sol.Game;

namespace Sol.Game.Components
{
	public class Position : IComponent
	{
		public Vector2f position = new Vector2f(0,0);
		public float depth = 0.0f;

		public Position(float x, float y, float depth)
		{
			position.x = x;
			position.y = y;
			this.depth = depth;
		}
	}
}

