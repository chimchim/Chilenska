namespace Sol.Game.Rules
{
	public class SystemTile 
	{
		private int x = 0;
		private int y = 0;
		private bool nil = true;

		public bool IsNull {get {return nil;}}

		public int X 
		{
			get 
			{
				return x;
			}
			set
			{
				nil = false;
				x = value;
			}
		}

		public int Y
		{
			get 
			{
				return y;
			}
			set
			{
				nil = false;
				y = value;
			}
			
		}

		public SystemTile()
		{
			nil = true;
		}

		public SystemTile(int x, int y)
		{
			nil = false;
			this.x = x;
			this.y = y;
		}
	}
}