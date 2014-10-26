using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.GEntity;
using Game.Component;

namespace Game.System
{
    public class GPhysicsSystem : GSystem
    {

        private float _skin = 0.05f;
        private float acceleration = 30;
        public override void Update(GameManager game, float delta)
        {

            foreach (int entity in _entityList)
            {
                GMovement movement = game.Entities.GetComponentOf<GMovement>(entity);
                if (movement._grounded)
                    movement._gravity = 0;

                movement._gravity -= 20 * Time.deltaTime;
                movement._grounded = false;

                GTransform transform = game.Entities.GetComponentOf<GTransform>(entity);
                movement._currentSpeed = IncrementTowards(movement._currentSpeed, movement._targetSpeed, acceleration);
                movement._targetSpeed = 0;
                RaycastHit hit;
                float deltaX = movement._currentSpeed * Time.deltaTime;
                float deltaY = movement._gravity * Time.deltaTime;
                Vector2 s = transform._bounds;
                Vector3 p = transform._position;
                Vector2 finalTransform1 = Vector2.zero;
                Vector2 direction = Vector2.zero;

                if (deltaX <= 0)
                {
                    for (int i = 2; i > -1; i--)
                    {
                        float dir = Mathf.Sign(deltaY);
                        float x = (p.x - s.x / 2) + s.x / 2 * i;
                        float y = p.y + s.y / 2 * dir;

                        Ray ray = new Ray(new Vector3(x, y, p.z), new Vector2(0, dir));
                        Debug.DrawRay(ray.origin, ray.direction);


                        if (Physics.Raycast(new Vector3(x, y, p.z), new Vector2(0, dir), out hit, Mathf.Abs(deltaY + (dir * _skin)), 1 << 11))
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
                                transform._position = new Vector3(transform._position.x, hit.point.y + (-dir * (_skin + 0.5f)), transform._position.z);
                                finalTransform1 = new Vector2(direction.x, direction.y);
                                movement._grounded = true;
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
                        float x = (p.x - s.x / 2) + s.x / 2 * i; // Left, centre and then rightmost point of collider
                        float y = p.y + s.y / 2 * dir; // Bottom of collider

                        Ray ray = new Ray(new Vector3(x, y, p.z), new Vector2(0, dir));
                        Debug.DrawRay(ray.origin, ray.direction);
                        //hit = Physics.Raycast(new Vector2(x, y), new Vector2(0, dir), out hit, Mathf.Abs(deltaY + (dir * _skin)), 1 << 11);

                        if (Physics.Raycast(new Vector3(x, y, p.z), new Vector2(0, dir), out hit, Mathf.Abs(deltaY + (dir * _skin)), 1 << 11))
                        {
                            direction = Vector3.Cross(hit.normal, hit.collider.transform.forward);
                            if (hit.normal.y < 0.3f && hit.normal.y > 0.0f)
                            {
                                deltaX = direction.x * deltaY - deltaX;
                                deltaY = direction.y * deltaY;
                            }
                            else
                            {
                                transform._position = new Vector3(transform._position.x, hit.point.y + (-dir * (_skin + 0.5f)), transform._position.z);
                                finalTransform1 = new Vector2(direction.x, direction.y);
                                movement._grounded = true;
                            }
                            break;
                        }
                    }
                }
                if (!movement._grounded)
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
                    float x = p.x  + s.x / 2 * dir;
                    float y = p.y  - s.y / 2 + s.y / (3 - 1) * i;

                    Ray ray = new Ray(new Vector3(x, y, p.z), new Vector2(0, dir));

                    Debug.DrawRay(ray.origin, finalTransform1.normalized);
                   // hit = Physics2D.Raycast(new Vector2(x, y), finalTransform1.normalized, finalTransform1.magnitude + _skin, 1 << 11, transform._position.z - 2, transform._position.z + 2);
                    if (Physics.Raycast(new Vector3(x, y, p.z), finalTransform1.normalized, out hit, finalTransform1.magnitude + _skin, 1 << 11))
                    {

                        if (i == 0 && 0.3f < hit.normal.y)
                        {
                            movement._grounded = true;
                            direction = Vector3.Cross(hit.normal, hit.collider.transform.forward);
                            newdir = direction * deltaX;
                        }
                        else
                        {
                            if (movement._grounded && hit.normal.y < 0.3f)
                            {
                                movement._currentSpeed = 0;
                                deltaY = 0;
                            }

                            newdir = new Vector2(0, deltaY);
                        }
                        hited = true;

                    }
                }

                transform._position += new Vector3(newdir.x, newdir.y, 0);
                if (!hited)
                    transform._position += new Vector3(finalTransform1.x, finalTransform1.y, 0);

            }
                
        }

        public override void InitSystems(GameManager game)
        {

        }

        private float IncrementTowards(float n, float target, float a)
        {
            return target;
            //if (n == target)
            //{
            //    return n;
            //}
            //else
            //{
            //    float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
            //    n += a * Time.deltaTime * dir;
            //    return (dir == Mathf.Sign(target - n)) ? n : target; // if n has now passed target then return target, otherwise return n
            //}
        }
    }
}