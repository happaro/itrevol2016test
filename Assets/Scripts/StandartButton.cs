using UnityEngine;
using System.Collections;

public class StandartButton : Button
{
	public enum ButtonAction{StartGame, GoToMainMenu}
	public ButtonAction myActionType;

	protected override void Action ()
	{
		MyAction ();
	}


	void MyAction()
	{
		switch (myActionType) 
		{
		case ButtonAction.StartGame:
			Application.LoadLevel ("Gameplay");
			break;
		case ButtonAction.GoToMainMenu:
			Application.LoadLevel ("Menu");
			break;
		default:
			Debug.LogWarning ("There is no \"" + myActionType + "\" action");
			break;
		}
	}
}
