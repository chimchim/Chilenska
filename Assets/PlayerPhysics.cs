using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BoxCollider2D))]
public class PlayerPhysics : MonoBehaviour
{

    public LayerMask collisionMask;

    private BoxCollider2D collider;
    private Vector3 s;
    private Vector3 c;

    private float skin = .05f;
    public LayerMask layer;
    private PlayerController playerController;
    [HideInInspector]
    public bool grounded;

    Ray2D ray;
    

    void Start()
    {
        Debug.Log(layer.value);
        collider = GetComponent<BoxCollider2D>();
        s = collider.size;
        c = collider.center;
        playerController = GetComponent<PlayerController>();
    }

    public void Move(Vector2 moveAmount)
    {
        RaycastHit2D hit;
        float deltaY = moveAmount.y;
        float deltaX = moveAmount.x;

        Vector2 p = transform.position;
        Vector2 finalTransform1;
        Vector2 direction = Vector2.zero;
        // Check collisions above and below
        grounded = false;

        if (deltaX <= 0)
        {
            for (int i = 2; i > -1; i--)
            {
                float dir = Mathf.Sign(deltaY);
                float x = (p.x + c.x - s.x / 2) + s.x / 2 * i;
                float y = p.y + c.y + s.y / 2 * dir; 
                
                ray = new Ray2D(new Vector2(x, y), new Vector2(0, dir));
                Debug.DrawRay(ray.origin, ray.direction);
                hit = Physics2D.Raycast(new Vector2(x, y), new Vector2(0, dir), Mathf.Abs(deltaY + (dir * skin)), 1 << 11, transform.position.z - 2, transform.position.z + 2);

                if (hit.collider != null)
                {
                    
                    direction = Vector3.Cross(hit.normal, hit.collider.transform.forward);
                    
                    if (hit.normal.y < 0.3f && hit.normal.y > 0.0f)
                    {
                        
                        if (direction.y < 0)
                        {
                            deltaX = -(direction.x * deltaY + deltaX);
                            deltaY = (-direction.y * deltaY);
                        }
                        else
                        {
                            deltaX = direction.x * deltaY + deltaX;
                            deltaY = direction.y * deltaY;
                        }
                    }
                    else
                    {
                        transform.position = new Vector3(transform.position.x, hit.point.y + (-dir * (skin + 0.5f)), transform.position.z);
                        finalTransform1 = new Vector2(direction.x, direction.y);
                        grounded = true; 
                    }
                    break;
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
                Debug.DrawRay(ray.origin, ray.direction);
                hit = Physics2D.Raycast(new Vector2(x, y), new Vector2(0, dir), Mathf.Abs(deltaY + (dir * skin)), 1 << 11, transform.position.z - 2, transform.position.z + 2);

                if (hit.collider != null)
                {
                    direction = Vector3.Cross(hit.normal, hit.collider.transform.forward);
                    if (hit.normal.y < 0.3f && hit.normal.y > 0.0f)
                    {
                        deltaX = direction.x * deltaY - deltaX;
                        deltaY = direction.y * deltaY;
                    }
                    else
                    { 
                        transform.position = new Vector3(transform.position.x, hit.point.y + (-dir * (skin + 0.5f)), transform.position.z);
                        finalTransform1 = new Vector2(direction.x, direction.y);
                        grounded = true;
                    }
                    break;
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
            hit = Physics2D.Raycast(new Vector2(x, y), finalTransform1.normalized, finalTransform1.magnitude + skin, 1 << 11, transform.position.z - 2, transform.position.z + 2);
            if (hit.collider != null)
            {
               
                if (i == 0 && 0.3f < hit.normal.y)
                {
                    grounded = true;
                    direction = Vector3.Cross(hit.normal, hit.collider.transform.forward);
                    newdir = direction * deltaX;
                }
                else
                {
                    if (grounded && hit.normal.y < 0.3f)
                    {
                        playerController.currentSpeed = 0;
                        deltaY = 0;
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
    public void MoveZ(float z)
    {
        RaycastHit2D hit;
        Vector3 p = transform.position;
        bool canMove = true;

        for (int i = 0; i < 2; i++)
        {
            float x = (p.x + c.x - s.x / 2) + s.x  * i; 
            float y = p.y + c.y -s.y / 2;

            Ray ray1 = new Ray(new Vector3(x, y, p.z), new Vector3(0, 0, 1));
            Debug.DrawRay(ray1.origin, ray1.direction);
            if (Physics.Raycast(new Vector3(x, y, p.z), new Vector3(0, 0, 1), Mathf.Abs(z + skin)))
            {
                canMove = false;
            }
        }
        for (int i = 0; i < 2; i++)
        {
            float x = (p.x + c.x - s.x / 2) + s.x * i;
            float y = p.y + c.y + s.y / 2;

            Ray ray1 = new Ray(new Vector3(x, y, p.z), new Vector3(0, 0, 1));
            Debug.DrawRay(ray1.origin, ray1.direction);
            if (Physics.Raycast(new Vector3(x, y, p.z), new Vector3(0, 0, z), Mathf.Abs(z + skin)))
            {
                canMove = false;
            }
        }
        if (canMove)
            transform.Translate(new Vector3(0, 0, z));
    }
}