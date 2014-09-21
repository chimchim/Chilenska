using System;

namespace Sol.Game
{
	public struct Vector2f
	{
		public float x;
		public float y;
		
		public Vector2f(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public static Vector2f Zero
		{
			get
			{
				return new Vector2f(0.0f, 0.0f);
			}
		}
		
		public static Vector2f operator +(Vector2f v1, Vector2f v2)
		{
			return new Vector2f(v1.x + v2.x, v1.y + v2.y);
		}
		
		public static Vector2f operator -(Vector2f v1, Vector2f v2)
		{
			return new Vector2f(v1.x - v2.x, v1.y - v2.y);
		}
		
		public static Vector2f operator *(Vector2f v1, float m)
		{
			return new Vector2f(v1.x * m, v1.y * m);
		}
		
		public static Vector2f operator /(Vector2f v1, float m)
		{
			return new Vector2f(v1.x / m, v1.y / m);
		}
		
		public static float Distance(Vector2f v1, Vector2f v2)
		{
			return (float)Math.Sqrt(Math.Pow(v1.x - v2.x, 2) + Math.Pow(v1.y - v2.y, 2));
		}
		
		public float Length()
		{
			return (float)Math.Sqrt(x * x + y * y);
		}

		public override string ToString()
		{
			return "[" + x + "," + y + "]";
		}
	}
}