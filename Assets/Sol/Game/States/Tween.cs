using System;

namespace Sol.Game.States
{
	public class Tween
	{
		float runningTime;
		float elapsedTime = 0f;

		public Tween (float runningTime)
		{
			this.runningTime = runningTime;
		}

		public void Reset()
		{
			elapsedTime = 0f;
		}

		/* returns a value between 0 and 1, this is the value
		 * of the tween. If this function returns 1 or above, 
		 * the tween has finished running.
		 */ 
		public float Update(float delta)
		{
			elapsedTime += delta;
			return elapsedTime / runningTime;
		}
	}
}

