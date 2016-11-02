using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour 
{
	public TextMesh timerText;

	float timer = 300;

	void Update () 
	{
		timer -= Time.deltaTime;
		timerText.text = string.Format ("{0:00}:{1:00}", (int) timer / 60, (int) timer % 60); 
	}
}
