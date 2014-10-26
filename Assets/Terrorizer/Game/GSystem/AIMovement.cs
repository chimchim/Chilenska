using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Game.Component;
using Game.Actions;

namespace Game.System
{
    public class AIMovement : GSystem
    {

        public bool sd = true;
        public override void Update(GameManager game, float delta)
        {

            foreach (int entity in _entityList)
            {
                GAstarComponent astarComponent = game.Entities.GetComponentOf<GAstarComponent>(entity);
                GTransform transform = game.Entities.GetComponentOf<GTransform>(entity);
                ActionQueue _actionQueue = game.Entities.GetComponentOf<ActionQueue>(entity);
                Vector3 current = astarComponent._current;
                bool wait = false;
                //Debug.Log("Astar value "  + astarComponent._path.ElementAt(astarComponent._index).Value);
                if (checkCollision(transform, current))
                {
                   if (!astarComponent._path.ElementAt(astarComponent._index).Value)
                   {
                     astarComponent._index++;
                     astarComponent._current = astarComponent._path.ElementAt(astarComponent._index).Key;
                   }
                }
                if (checkFullCollision(transform, current) && astarComponent._path.ElementAt(astarComponent._index).Value && game.Entities.GetComponentOf<GMovement>(entity)._grounded)
                {
                    
                    JumpAction a = JumpAction.Make();
                    a.Apply(game, transform.EntityID);
                    a.Recycle();
                   astarComponent._index++;
                   astarComponent._current = astarComponent._path.ElementAt(astarComponent._index).Key;
                }

                if (Mathf.Abs(transform._position.x - current.x) > 0.1f)
                {
                    MoveAction a = MoveAction.Make(current.x - transform._position.x);
                    a.Apply(game, transform.EntityID);
                    a.Recycle();
                }
                if (Mathf.Abs(transform._position.z - current.z) > 0.1f)
                {
                    //Debug.Log("Move Z " + Mathf.Sign(current.x - transform._position.x));
                    MoveZAction a = MoveZAction.Make(current.z - transform._position.z);
                    a.Apply(game, transform.EntityID);
                    a.Recycle();
                }

            }
        }

        private bool checkFullCollision(GTransform transform, Vector3 nodePos)
        {
            
            if (Mathf.Abs(transform._position.x - nodePos.x) < 0.1f &&
               Mathf.Abs(transform._position.z - nodePos.z) < 0.1f &&
                Mathf.Abs((transform._position.y+ transform._bounds.y/2 +0.05f) - nodePos.y) <1.6f)
                {
                    
                    return true;
                }
            else
                return false;
        }
        private bool checkCollision(GTransform transform, Vector3 nodePos)
        {
            //Debug.Log("NodePos "  + nodePos);
            if (Mathf.Abs(transform._position.x - nodePos.x) < 0.1f &&
               Mathf.Abs(transform._position.z - nodePos.z) < 0.1f)
            {
                
                return true;
            }
            else
                return false;
        }
        public override void InitSystems(GameManager game)
        {
            foreach (int entity in _entityList)
            {
                
            }
        }
    }
}
