using UnityEngine;
using System.Collections;

public class WindowShop : Window 
{
	public Button closeButton;

	void Start () 
	{
		closeButton.myAction = () => {
			base.CloseWindow();
		};
	}
}
