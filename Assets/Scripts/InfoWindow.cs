using UnityEngine;
using System.Collections;

public class InfoWindow : Window 
{
	public Button okButton;

	void Start () 
	{
		okButton.myAction = () => {
			base.CloseWindow();
		};
	}
	

	void Update () 
	{
	
	}
}
