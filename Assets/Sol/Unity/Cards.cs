using System;
using UnityEngine;

using Sol.Game;

namespace Sol.Unity.Factory
{
	public class Cards
	{
		private static Vector2 size = new Vector2(0.62f, 0.88f);

		public static Vector2 Size {get {return size;}}

		public static GameObject MakeCard(string type, Vector2f position)
		{
			GameObject card = GameObject.Instantiate(new GameObject()) as GameObject;
			card.name = "Card";

			MakeFace(card, "Back", -0.01f, "2h", Color.clear);
			MakeFace(card, "Middle", 0.0f, null, Color.white);
			MakeFace(card, "Front", 0.01f, "2h", Color.clear);

			card.transform.position = new Vector3(position.x, 0.1f, position.y);
	
			return card;
		}

		private static void MakeFace(GameObject parent, string name, float z, string texture, Color color)
		{
			GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
			obj.name = name;

			obj.GetComponent<MeshCollider>().enabled = false;
			GameObject.Destroy(obj.GetComponent<MeshCollider>());

			obj.transform.parent = parent.transform;
			obj.transform.localScale = new Vector3(Size.x * 0.1f, 1f, Size.y * 0.1f);
			obj.transform.localPosition = Vector3.up * z;

			obj.GetComponent<MeshRenderer>().material.shader = Shader.Find("Transparent/Cutout/Soft Edge Unlit");
			obj.GetComponent<MeshRenderer>().material.SetFloat("_Cutoff", 0.0f);

			if (texture != null)
			{
				obj.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load(texture) as Texture;
			}

			if (color != Color.clear)
			{
				//obj.AddComponent<Colorize>();
				//obj.GetComponent<Colorize>().m_Color = color;
			}
		}
	}
}


