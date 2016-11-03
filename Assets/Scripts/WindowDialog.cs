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
			base.Close (true);
		};
	}

	public void Open(string title, string newText)
	{
        base.Open(false);
		info.text = newText;
		okButton.myAction = () => {base.Close();};
	}

	public void Open(string titleText, string infoText, Button.MyAction okAction)
	{
        base.Open(false);
		okButton.myAction = okAction;
		title.text = titleText;
		info.text = infoText;
	}
}
