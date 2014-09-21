using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Sol.Game.Entities;
using Sol.Game.Components;

namespace Sol.Unity
{
	public class EntityRenderer
	{
		private Dictionary<Entity, GameObject> objects = new Dictionary<Entity, GameObject>();

		public void Render(List<Entity> entities)
		{
			foreach(Entity entity in entities)
			{
				if (!objects.ContainsKey(entity))
				{
					objects[entity] = CreateGameObject(entity);;
				}

				if (objects[entity] != null)
				{
					SetPosition(objects[entity], entity);
				}
			}
		}

		private void SetPosition(GameObject obj, Entity entity)
		{
			Position position = entity.GetComponent<Position>();
			IsGUIElement gui = entity.GetComponent<IsGUIElement>();
			if (position != null)
			{
				Vector3 offset = Vector3.zero;
				if (gui != null)
				{
					Vector3 camera = GameObject.Find("Main Camera").transform.position;
					offset = camera;
					offset.y = 0f;
				}

				obj.transform.localPosition = 
					new Vector3(position.position.x, position.depth, position.position.y)
						+ offset;
			}

		}

		private GameObject CreateGameObject(Entity entity)
		{
			if (entity.GetComponent<CardType>() != null)
			{
				return Factory.Cards.MakeCard("", entity.GetComponent<Position>().position);
			}
			else if (entity.GetComponent<SystemType>() != null)
			{
				return Factory.Systems.MakeSystem
					(entity.GetComponent<SystemType>().info.tile.X, entity.GetComponent<SystemType>().info.tile.Y);
			}
			else
			{
				return null;
			}
		}
	}


}