using UnityEngine;
using System.Collections;

public class WindowInfo : Window 
{
	public Button okButton;

	void Start () 
	{
		okButton.myAction = () => {
			base.CloseWindow();
		};
	}
}
