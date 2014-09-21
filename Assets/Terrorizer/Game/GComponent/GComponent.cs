using System.Collections;

namespace Game.Component
{
	// lägg till entiterna i de representerade system när komponenterna skapas
	/* smygar spel där man försöker lista ut varandras position i mörker ( inte så snabb kanske) lysen där man kan vakta. low precision när man träffar från
	 * lång håll,
     * olika hastigheter när man har vissa vapen

	 Turnaround time på vapen?*/
	public abstract class GComponent
	{
		private int _entityid;
        private int _familiyid;
		protected GComponent()
		{
			
		}

		//public abstract void Recycle();

		public int EntityID 
		{

			get
			{
				return _entityid;
			}
			protected set
			{
				_entityid = value;
			}
		}

		public int FamilyID 
		{
			get
			{
				return _familiyid;
			}
			protected set
			{
				_familiyid = value;
			}
		}
	}
}
