using UnityEngine;
using System.Collections;

public class KeyHandler : MonoBehaviour 
{
	void Update () 
	{
		if (Input.GetKey (KeyCode.Escape))
			Application.LoadLevel ("Menu");
	}
}
