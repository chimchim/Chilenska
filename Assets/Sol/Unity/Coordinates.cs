using System;
using UnityEngine;
using Sol.Game;

namespace Sol.Unity
{
	public class Coordinates
	{
		public static Vector3 FromGame(Vector2f position)
		{
			return new Vector3(position.x, 0.0f, position.y);
		}

		public static Vector2f FromUnity(Vector3 position)
		{
			return new Vector2f(position.x, position.z);
		}
	}
}

