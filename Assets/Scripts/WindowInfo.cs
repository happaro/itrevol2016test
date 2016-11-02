using UnityEngine;
using System.Collections;

public class WindowInfo : Window 
{
	public Button okButton;
	public TextMesh title;
	public TextMesh info;

	public void Open(string title, string newText)
	{
		info.text = newText;
		gameObject.SetActive (true);
		okButton.myAction = () => {base.Close();};
	}

	public void Open(string titleText, string infoText, Button.MyAction closeActionPlus)
	{
		okButton.myAction = closeActionPlus;
		title.text = titleText;
		info.text = infoText;
		gameObject.SetActive (true);
	}
}
