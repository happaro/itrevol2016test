using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour 
{
	protected virtual void CloseWindow()
	{
		gameObject.SetActive (false);
	}

}
