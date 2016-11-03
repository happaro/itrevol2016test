using UnityEngine;
using System.Collections;

public class WindowDialog : Window 
{
	public Button okButton, cancelButton;
	public TextMesh title;
	public TextMesh info;

	public void Start()
	{
		cancelButton.myAction = () => {
			base.Close ();
		};
	}

	public void Open(string title, string newText)
	{
		info.text = newText;
		gameObject.SetActive (true);
		okButton.myAction = () => {base.Close();};
	}

	public void Open(string titleText, string infoText, Button.MyAction okAction)
	{
		okButton.myAction = okAction;
		title.text = titleText;
		info.text = infoText;
		gameObject.SetActive (true);
	}
}
