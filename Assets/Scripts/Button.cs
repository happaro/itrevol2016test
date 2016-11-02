using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
	private bool isPressed;

	public delegate void MyAction();
	public MyAction myAction;

	private Vector3 startScale;

	void Awake()
	{
		startScale = transform.localScale;
	}

	void DownAction()
	{
		transform.localScale = startScale * 0.9f;	
	}

	void UpAction()
	{
		transform.localScale = startScale;
	}

	void OnMouseDown()
	{
		DownAction ();
		isPressed = true;
	}
		
	void OnMouseExit()
	{
		UpAction ();
		isPressed = false;	
	}

	void OnMouseUp()
	{
		if (isPressed) 
		{
			UpAction ();
			Action ();
		}
	}

	protected virtual void Action()
	{
		myAction ();
	}
}
