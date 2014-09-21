using System;
using System.Collections.Generic;

using Sol.Game.Rules;

namespace Sol.Game.States
{
	public class StateMachine
	{
		private HashSet<string> performedActions = new HashSet<string>();
		private Dictionary<string, object> variables = new Dictionary<string, object>();
		private State currentState;

		public StateMachine(State state)
		{
			currentState = state;
		}

		public void Ponder(float delta)
		{
			currentState.Ponder(this, delta);
		}

		public void Transition(State next)
		{
			currentState = next;
		}

		public void AddAction(string action)
		{
			performedActions.Add(action);
		}

		public bool IsAllowed(string action)
		{
			return currentState.IsAllowed(action);
		}

		public bool PerformedActionsMatch(HashSet<string> other)
		{
			return (other.IsSubsetOf(performedActions) && performedActions.IsSubsetOf(other));
		}

		public void SetValue(string variable, object value)
		{
			variables[variable] = value;
		}

		public void RemoveValue(string variable)
		{
			variables.Remove(variable);
		}

		public T GetValue<T>(string variable)
		{
			object value;
			if (variables.TryGetValue(variable, out value))
			{
				if (value is T)
				{
					return (T)value;
				}
				else
				{
					return default(T);
				}
			}
			return default(T);
		}
	}
}

