using UnityEngine;
using System.Collections;

public class PhysicsTest : MonoBehaviour {

    float acceleration = 4f;
    float maxSpeed = 150f;
    float gravity = 6f;
    float maxfall = 200f;
    float jump = 200f;

    int layermask;

    Rect box;
    Vector2 velocity;

    bool grounded = false;
    bool falling = false;

    int horizontalRays = 6;
    int verticalRays = 4;
    int margin = 2;


	void Start () 
    {
        layermask = LayerMask.NameToLayer("groundFloor");
	}

    void FixedUpdate()
    {
        box = new Rect(collider.bounds.min.x,
                        collider.bounds.min.y,
                        collider.bounds.size.x,
                        collider.bounds.size.y);

        if (!grounded)
            velocity = new Vector2(velocity.x, Mathf.Max(velocity.y - gravity, -maxfall));
        //<> ||
        if (velocity.y < 0)
        {
            falling = true;
        }

        if (grounded || falling)
        {
            Vector3 startPoint = new Vector3(box.xMin + margin, box.center.y, transform.position.z);
            Vector3 endPoint = new Vector3(box.xMax - margin, box.center.y, transform.position.z);

            RaycastHit hitInfo;

            float distance = box.height / 2 + (grounded ? margin : Mathf.Abs(velocity.y * Time.deltaTime));

            bool connected = false;

            for (int i = 0; i < verticalRays; i++)
            {
                float lerpAmount = (float)i / (float)verticalRays - 1;
                Vector3 origin = Vector3.Lerp(startPoint, endPoint, lerpAmount);
                Ray ray = new Ray(origin, Vector3.down);

                connected = Physics.Raycast(ray, out hitInfo, distance, layermask);
                if (connected)
                {
                    grounded = true;
                    falling = false;
                    transform.Translate(Vector3.down * (hitInfo.distance - box.height / 2));
                    velocity = new Vector2(velocity.x, 0);
                    break;
                }
            }
            if (!connected)
            {
                grounded = false;
            }
        }

    }

	void LateUpdate () 
    {
        transform.Translate(velocity * Time.deltaTime);
	}
}
