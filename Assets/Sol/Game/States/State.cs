using System;
using System.Collections.Generic;

using Sol.Game.Rules;

namespace Sol.Game.States
{
	public class State
	{
		private List<Transition> transitions = new List<Transition>();
		private HashSet<string> allowedActions = new HashSet<string>();

		public void AddAllowedAction(string action)
		{
			allowedActions.Add(action);
		}

		public void AddTransition(Transition transition)
		{
			transitions.Add(transition);
		}

		public bool IsAllowed(string action)
		{
			return allowedActions.Contains(action);
		}

		public void Ponder(StateMachine machine, float delta)
		{
			foreach(Transition transition in transitions)
			{
				State next = transition.TryNextState(machine);
				if (next != null)
				{
					machine.Transition(next);
				}
			}
		}

		/* returns the starting state */
		public static State MakeStates()
		{
			return StartGame();
		}		

		private static State StartGame()
		{
			State ret = new State();
			ret.AddAllowedAction("start game");
			Transition t = new Transition(BeginTurn());
			t.AddRequiredAction("start game");
			return ret;
		}

		private static State BeginTurn()
		{
			State ret = new State();

			return ret;
		}
	}
}

