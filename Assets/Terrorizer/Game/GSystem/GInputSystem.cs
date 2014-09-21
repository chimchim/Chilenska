using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Game.Component;
using Game.Actions;

namespace Game.System
{
    public class GInputSystem : GSystem
    {
        // gör en input translator?

       private GBoxCollider2D _boxCollider;
       private GRawInput _playerInput;
       private GTransform _transform;
       private ActionQueue _actionQueue;
       public override void Update(GameManager game, float delta)
       {

           if (Input.GetKey(KeyCode.A))
           {                                       
               MoveAction a = MoveAction.Make(-Vector3.right, true);
               a.Apply(game, _transform.EntityID);
               a.Recycle();
           }


           if (Input.GetKey(KeyCode.D))
           {                                        
               MoveAction a = MoveAction.Make(Vector3.right, true);
               a.Apply(game, _transform.EntityID);
               a.Recycle();

           }


           if (Input.GetKeyDown(KeyCode.W))
           {                                            
               _actionQueue._actionQueue.Add(MoveAction.Make(Vector3.back, false));

           }

           if (Input.GetKeyDown(KeyCode.S))
           {                                            
               _actionQueue._actionQueue.Add(MoveAction.Make(-Vector3.back, false));

           }


           if (Input.GetKeyDown(KeyCode.Space))
           {
               JumpAction a = JumpAction.Make();
               a.Apply(game, _transform.EntityID);
               a.Recycle();
           }


           if (Input.GetMouseButton(0))
           {
              
               
           }

       }

       public override void InitSystems(GameManager game)
       {
           foreach (int entity in _entityList)
           {
               //_playerInput = game.Entities.GetComponentOf<GRawInput>(entity);
               _transform = game.Entities.GetComponentOf<GTransform>(entity);
               //_boxCollider = game.Entities.GetComponentOf<GBoxCollider2D>(entity);
               //_actionQueue = game.Entities.GetComponentOf<ActionQueue>(entity);
           }
       }
    }
}
