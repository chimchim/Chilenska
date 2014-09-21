using UnityEngine;
using System.Collections;

public class scr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -Vector3.right * 9 * Time.deltaTime;
        }


        if (Input.GetKey(KeyCode.D))
        {

            transform.position += Vector3.right * 9 * Time.deltaTime;
        }
	}
}
