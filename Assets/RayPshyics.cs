using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BoxCollider2D))]
public class RayPshyics : MonoBehaviour
{

    public LayerMask collisionMask;

    private BoxCollider2D collider;
    private Vector3 s;
    private Vector3 c;

    private float skin = .005f;

    private Phy playerController;
    [HideInInspector]
    public bool grounded;

    Ray2D ray;
    RaycastHit2D hit;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        s = collider.size;
        c = collider.center;
        playerController = GetComponent<Phy>();
    }

    public void Move(Vector2 moveAmount)
    {

        float deltaY = moveAmount.y;
        float deltaX = moveAmount.x;
        float dirX = deltaX;
        float moveY = 0;
        Vector2 p = transform.position;
        Vector2 finalTransform1;
        Vector2 direction = Vector2.zero;
        // Check collisions above and below

        grounded = false;


        for (int i = 2; i > -1; i--)
        {
            float dir = Mathf.Sign(deltaY);
            float x = (p.x + c.x - s.x / 2) + s.x / 2 * i +deltaX; 
            float y = p.y + c.y + s.y / 2 * dir; 

            ray = new Ray2D(new Vector2(x, y), new Vector2(0, dir));
            Debug.DrawRay(ray.origin, ray.direction);
            hit = Physics2D.Raycast(new Vector2(x, y), new Vector2(0, dir), Mathf.Abs(deltaY + (dir * skin)));
            if (hit.collider != null)
            {
                //Debug.DrawLine(ray.origin, hit.point);
                grounded = true;
                moveY = hit.distance;
                transform.position = new Vector3(transform.position.x, hit.point.y + (-dir * (skin + 0.5f)), transform.position.z);
                deltaY = 0;
                Debug.Log("adsd");
                break;
            }
        }

           transform.Translate(new Vector2(deltaX, deltaY));
    }
}