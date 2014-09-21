using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class MoveTest : MonoBehaviour {

	// Use this for initialization
	private int currentlayer = 9;

	public List<BoxCollider2D> secondFloors = new List<BoxCollider2D>();
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetKeyDown (KeyCode.W)) 
		{
			Debug.Log("Layer " + LayerMask.LayerToName(gameObject.layer));
			if(LayerMask.LayerToName(gameObject.layer) == "Characters")
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, 1);
				gameObject.layer = 12;
				Debug.Log("newLayer " + LayerMask.LayerToName(gameObject.layer));
			}
		}
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log ("Collider NAME " + LayerMask.LayerToName(col.gameObject.layer));

	}
}