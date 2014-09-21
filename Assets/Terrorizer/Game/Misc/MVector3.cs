
using System.Collections;

namespace Game.Misc
{
	public class MVector3 
	{
		public float x;
		public float y;
		public float z;

		public MVector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		public MVector3()
		{
			this.x = 0;
			this.y = 0;
			this.z = 0;
		}
	}
}