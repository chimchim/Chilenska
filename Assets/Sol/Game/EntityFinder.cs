using System.Collections.Generic;
using Sol.Game.Components;
using Sol.Game.Entities;

namespace Sol.Game
{
	public class EntityFinder 
	{		
		public static Entity GetEntityAt(List<Entity> entities, Vector2f point)
		{
			Entity found = null;
			float depth = 0.0f;
			foreach (Entity entity in entities)
			{
				if (IsEntityAt(entity, point))
				{
				    if (entity.GetComponent<Position>() != null)	
					{
						float newDepth = entity.GetComponent<Position>().depth;
						if (found == null)
						{
							depth = newDepth;
							found = entity;
						}
						else if (newDepth > depth)
						{
							depth = newDepth;
							found = entity;
						}
					}
				}
			}
			return found;
		}

		public static bool IsEntityAt(Entity entity, Vector2f point)
		{
			BoundingRectangle bound = entity.GetComponent<BoundingRectangle>();
			Position position = entity.GetComponent<Position>();
			
			if (position != null && bound != null)
			{

				Vector2f min = bound.min + position.position;
				Vector2f max = bound.max + position.position;
				
				if (TestCollision(point, min, max))
				{
					return true;
				}
			}

			return false;
		}

		public static bool TestCollision(Vector2f position, Vector2f min, Vector2f max)
		{
			return (position.x <= max.x && position.x >= min.x &&
			        position.y <= max.y && position.y >= min.y);
		}
	}
}