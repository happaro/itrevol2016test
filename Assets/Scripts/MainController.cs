using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour 
{
	public TextMesh timerText, moneyText, expText;
	public Camera villageCamera;
	float timer = 300;

	public static MainController ins;

	void Start()
	{
		Application.targetFrameRate = 60;
		MainController.ins = this;
		SaveManager.coinsCount = 5000;
	}

	void FixedUpdate () 
	{
		timer -= Time.deltaTime;
		timerText.text = string.Format ("{0:00}:{1:00}", (int) timer / 60, (int) timer % 60); 
		moneyText.text = SaveManager.coinsCount.ToString ();
		expText.text = SaveManager.currentScore.ToString ();
	}
}
