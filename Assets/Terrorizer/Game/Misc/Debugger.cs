using UnityEngine;
using System.Collections;

namespace Game.Misc
{
	public static class Debugger 
	{
		public static void Log(string message)
		{
			Debug.Log(message);
		}
		public static void Warning(string message)
		{
			Debug.LogWarning(message);
		}
		public static void Error(string message)
		{
			Debug.LogError(message);
		}
	}
}