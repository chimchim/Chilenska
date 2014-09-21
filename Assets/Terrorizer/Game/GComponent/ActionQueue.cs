using System.Collections.Generic;
using Game.Actions;

namespace Game.Component
{
    public class ActionQueue : GComponent
    {

        public List<Action> _actionQueue = new List<Action>();

        public ActionQueue()
        {

        }
        public static ActionQueue Make(int entityID)
        {
            ActionQueue comp = new ActionQueue();
            comp.EntityID = entityID;
            comp.FamilyID = 4;
            return comp;
        }
    }
}