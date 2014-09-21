using System.Collections;

using Sol.Game;

public class InputListener 
{
	public class MouseListener : InputManager.IMouseListener
	{
		public class Button
		{
			public bool Released {get; set;}
			public bool Pressed {get;set;}
			public bool Down {get;set;}
			public Button()
			{
				Released = false;
				Pressed = false;
				Down = false;
			}
		}

		public Vector2f Position {get; private set;}
		public Button Left {get; private set;}
		public Button Right {get; private set;}
		public Button Middle {get; private set;}

		public MouseListener()
		{
			Left = new Button();
			Right = new Button();
			Middle = new Button();
		}
		public void Release(int button, Vector2f position)
		{
			switch (button)
			{
			case 0:
				Left.Released = true;
				Left.Down = false;
				break;
			case 1:
				Right.Released = true;
				Right.Down = false;
				break;
			case 2:
				Middle.Released = true;
				Middle.Down = false;
				break;
			}

			Position = position;
		}
		
		
		public void Press(int button, Vector2f position)
		{
			switch (button)
			{
			case 0:
				Left.Pressed = true;
				Left.Down = true;
				break;
			case 1:
				Right.Pressed = true;
				Right.Down = true;
				break;
			case 2:
				Middle.Pressed = true;
				Middle.Down = true;
				break;
			}

			Position = position;
		}

		public void Move(Vector2f position)
		{
			Position = position;
		}

		public void Update()
		{
			Left.Released = false;
			Left.Pressed = false;
			Right.Released = false;
			Right.Pressed = false;
			Middle.Pressed = false;
			Middle.Released = false;
		}
	}

	private MouseListener mouse = new MouseListener();

	public MouseListener Mouse {get {return mouse;}}
}
