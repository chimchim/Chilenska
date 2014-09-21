using System;
using System.Collections.Generic;

namespace Sol.Game
{
	public class InputManager
	{
		public interface IMouseListener
		{
			void Release(int button, Vector2f position);
			void Press(int button, Vector2f position);
			void Move(Vector2f position);
		}

		private List<IMouseListener> mouseListeners = new List<IMouseListener>();
		private static InputManager instance = new InputManager();

		public static InputManager Instance {get {return instance;}}

		private InputManager () {}

		public void SendMousePress(int button, Vector2f position)
		{
			foreach (IMouseListener listener in mouseListeners)
			{
				listener.Press(button, position);
			}
		}

		public void SendMouseRelease(int button, Vector2f position)
		{
			foreach (IMouseListener listener in mouseListeners)
			{
				listener.Release(button, position);
			}
		}

		public void SendMousePosition(Vector2f position)
		{
			foreach (IMouseListener listener in mouseListeners)
			{
				listener.Move(position);
			}
		}

		public void RegisterMouseListener(InputManager.IMouseListener mouseListener)
		{
			mouseListeners.Add(mouseListener);
		}

	}
}

