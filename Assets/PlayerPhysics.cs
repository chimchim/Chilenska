using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BoxCollider2D))]
public class PlayerPhysics : MonoBehaviour
{

    public LayerMask collisionMask;

    private BoxCollider2D collider;
    private Vector3 s;
    private Vector3 c;

    private float skin = .005f;

    private PlayerController playerController;
    [HideInInspector]
    public bool grounded;

    Ray2D ray;
    RaycastHit2D hit;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        s = collider.size;
        c = collider.center;
        playerController = GetComponent<PlayerController>();
    }

    public void Move(Vector2 moveAmount)
    {

        float deltaY = moveAmount.y;
        float deltaX = moveAmount.x;
        float dirX = deltaX;

        Vector2 p = transform.position;
        Vector2 finalTransform1;
        Vector2 direction = Vector2.zero;
        // Check collisions above and below
        grounded = false;

        if (dirX <= 0)
        {
            for (int i = 2; i > -1; i--)
            {
                float dir = Mathf.Sign(deltaY);
                float x = (p.x + c.x - s.x / 2) + s.x / 2 * i; // Left, centre and then rightmost point of collider
                float y = p.y + c.y + s.y / 2 * dir; // Bottom of collider

                ray = new Ray2D(new Vector2(x, y), new Vector2(0, dir));
                //Debug.DrawRay(ray.origin, ray.direction);
                hit = Physics2D.Raycast(new Vector2(x, y), new Vector2(0, dir), Mathf.Abs(deltaY + (dir * skin)));

                if (Physics2D.Raycast(new Vector2(x, y), new Vector2(0, dir), Mathf.Abs(deltaY + (dir * skin))))
                {

                    float dst = Vector3.Distance(ray.origin, hit.point);
                    direction = Vector3.Cross(hit.normal, hit.collider.transform.forward);

                    //transform.position += new Vector3(0, skin - dst, 0);

                    finalTransform1 = new Vector2(direction.x, direction.y);
                    grounded = true;


                }
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                float dir = Mathf.Sign(deltaY);
                float x = (p.x + c.x - s.x / 2) + s.x / 2 * i; // Left, centre and then rightmost point of collider
                float y = p.y + c.y + s.y / 2 * dir; // Bottom of collider

                ray = new Ray2D(new Vector2(x, y), new Vector2(0, dir));
                //Debug.DrawRay(ray.origin, ray.direction);
                hit = Physics2D.Raycast(new Vector2(x, y), new Vector2(0, dir), Mathf.Abs(deltaY + (dir * skin)));

                if (Physics2D.Raycast(new Vector2(x, y), new Vector2(0, dir), Mathf.Abs(deltaY + (dir * skin))))
                {
                    
                    float dst = Vector3.Distance(ray.origin, hit.point);
                    direction = Vector3.Cross(hit.normal, hit.collider.transform.forward);
              
                    //transform.position += new Vector3(0, skin - dst, 0);
                    finalTransform1 = new Vector2(direction.x, direction.y);
                    grounded = true;

                }
            }
        }
        if (!grounded)
        {
 
            Vector2 finalTransform = new Vector2(0, deltaY);
            finalTransform1 = new Vector2(deltaX, deltaY);
        }
        else
        {
            finalTransform1 = new Vector2(direction.x * deltaX, direction.y * deltaX);
        }
        Vector2 newdir = Vector2.zero;
        bool hited = false;
        for (int i = 0; i < 3; i++)
        {
            float dir = Mathf.Sign(finalTransform1.x);
            float x = p.x + c.x + s.x / 2 * dir;
            float y = p.y + c.y - s.y / 2 + s.y / (3 - 1) * i;

            ray = new Ray2D(new Vector2(x, y), new Vector2(dir, 0));
            
            Debug.DrawRay(ray.origin, finalTransform1.normalized);
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(x, y), -Vector2.up, 10);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, y), finalTransform1, finalTransform1.magnitude);
            if (hit.collider != null)
            {

                if (i == 0 && 0.3f < hit.normal.y)
                {
                    Debug.Log("Walk up " + hit.normal);
                    direction = Vector3.Cross(hit.normal, hit.collider.transform.forward);
                    newdir = direction * deltaX;
                }
                else
                {

                    if (grounded && 0 >= hit.normal.y)
                    {
                        Debug.Log("Stop speed " + hit.normal.y);
                        playerController.currentSpeed = 0;
                    }
                    newdir = new Vector2(0, deltaY);
                    
                }
                hited = true;
            }

        }

        transform.Translate(newdir);
        if (!hited)
            transform.Translate(finalTransform1);
    }

}