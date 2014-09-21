using UnityEngine;
using System.Collections;

public class Phy : MonoBehaviour {

	private RaycastHit2D gravityRay;
    public BoxCollider2D asd;

    private Vector2 size;
    private Vector2 center;
	// Use this for initialization
	void Start () 
    {
        size = asd.size;
        center = asd.center;
        transform.position = new Vector3(-asd.bounds.extents.x + asd.transform.position.x, asd.bounds.extents.y + asd.transform.position.y, 0);
        Debug.Log(asd.bounds.extents);
	}
	
	// Update is called once per frame
	public void dpdate () 
	{
        size = asd.size;
        center = asd.center;
        transform.position = new Vector3(-asd.bounds.extents.x + asd.transform.position.x, asd.bounds.extents.y + asd.transform.position.y, 0);
        Debug.Log(asd.bounds.extents);
		//gravityRay = Physics2D.Raycast(new Vector3(0,7,0), -Vector2.up);
		//Debug.Log("Hitlayer " + (gravityRay.point));
		//Debug.Log("Hitlayer " + " "+ boxCollider2D.bounds.extents + (boxCollider2D.size.x+ transform.position.x) +" "+ (boxCollider2D.size.y+ transform.position.y));
	}
}
