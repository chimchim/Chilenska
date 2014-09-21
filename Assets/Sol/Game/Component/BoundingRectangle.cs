using System.Collections;
using Sol.Game;

namespace Sol.Game.Components
{
	public class BoundingRectangle : IComponent
	{
		public Vector2f min = new Vector2f(0,0);
		public Vector2f max = new Vector2f(0,0);

		public BoundingRectangle(float x0, float y0, float x1, float y1)
		{
			min.x = x0;
			min.y = y0;
			max.x = x1;
			max.y = y1;
		}
	}
}