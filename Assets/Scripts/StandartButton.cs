using UnityEngine;
using System.Collections;

public class StandartButton : Button
{
	public enum ButtonAction {StartGame, GoToMainMenu, GoShop}
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
		case ButtonAction.GoShop:
			//Debug.LogWarning (WindowManager.Instance.name);
			WindowManager.Instance.GetWindow<WindowShop> ().Open ();
			WindowManager.Instance.GetWindow<GUI> ().Close ();
			break;
		default:
			Debug.LogWarning ("There is no \"" + myActionType + "\" action");
			break;
		}
	}
}
