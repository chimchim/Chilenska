using System.Collections.Generic;
using Sol.Game.Rules;

namespace Sol.Game.States
{
	public class Transition
	{
		private HashSet<string> requiredActions = new HashSet<string>();
		private State next;

		public Transition(State next)
		{
			this.next = next;
		}

		public State TryNextState(StateMachine machine)
		{
			if (machine.PerformedActionsMatch(requiredActions))
			{
				return next;
			}
			else
			{
				return null;
			}
		}

		public void AddRequiredAction(string action)
		{
			requiredActions.Add(action);
		}
	}
}

