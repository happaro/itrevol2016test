using UnityEngine;
using System.Collections;

public class WindowInfo : Window 
{
	public Button okButton;
	public TextMesh title;
	public TextMesh info;

	public void Open(string title, string newText)
	{
        base.Open(false);
		info.text = newText;
        okButton.myAction = () => { base.Close(true); };
	}

	public void Open(string titleText, string infoText, Button.MyAction closeActionPlus)
	{
        base.Open(false);
		okButton.myAction = closeActionPlus;
		title.text = titleText;
		info.text = infoText;
	}
}
