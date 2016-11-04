using UnityEngine;
using System.Collections;
//using GooglePlayGames.BasicApi;
//using GooglePlayGames;

public class MainController : MonoBehaviour 
{
	public TextMesh timerText, moneyText, expText;
	public Camera villageCamera;
	public float timer = 300;
	bool isGameOver;

	public static MainController ins;
	public Inventory inventory;

	void Awake()
	{
		SaveManager.currentScore = 0;
		SaveManager.coinsCount = 5000;
		MainController.ins = this;
		Application.targetFrameRate = 60;
	}

	void ReportScore()
	{
		#if UNITY_ANDROID
		/*PlayGamesPlatform.Activate();
		if (!Social.localUser.authenticated)
		{
			Social.localUser.Authenticate(authenticated =>
				{
					Social.ReportScore(PlayerPrefs.GetInt ("Score"), "CgkI4Mqg1dYXEAIQAA", (bool success) =>
						{
							if (success)
								((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkI4Mqg1dYXEAIQAA");
							else{}
						});
				});
		}
		else
		{
			Social.ReportScore(PlayerPrefs.GetInt ("Score"), "CgkI4Mqg1dYXEAIQAA", (bool success) =>
				{
					if (success)
						((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkI4Mqg1dYXEAIQAA");
					else{}
				});
		}*/
		#endif
	}

	void Start()
	{
		inventory = new Inventory ();
		inventory.productsCounts = new int[BASE.Instance.properties.Length];
	}

	void FixedUpdate () 
	{
		if (isGameOver)
			return;
		timer -= Time.deltaTime;
		timerText.text = string.Format ("{0:00}:{1:00}", (int) timer / 60, (int) timer % 60); 
		moneyText.text = SaveManager.coinsCount.ToString ();
		expText.text = SaveManager.currentScore.ToString ();

		if (timer < 0) 
		{
			isGameOver = true;
			WindowManager.Instance.CloseAllWindow ();
			WindowManager.Instance.GetWindow<WindowInfo> ().Open ("Игра кончена", string.Format("Игра окончена\n\nСпасибо за игру! \n Ваш счет:\n{0}\n\nЛучший счет:\n{1}", SaveManager.currentScore, SaveManager.bestScore), () => {Application.LoadLevel(0);});
			SaveManager.bestScore = SaveManager.bestScore < SaveManager.currentScore ? SaveManager.currentScore : SaveManager.bestScore;
		}
	}
}

[System.Serializable]
public class Inventory
{
	public int mainProductCount;
	public int[] productsCounts;
}
