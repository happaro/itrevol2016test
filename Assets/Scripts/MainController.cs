using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour 
{
	public TextMesh timerText, moneyText, expText;
	public Camera villageCamera;
	float timer = 300;

	public static MainController ins;
	public Inventory inventory;

	void Awake()
	{
		MainController.ins = this;
		Application.targetFrameRate = 60;
		SaveManager.coinsCount = 5000;
	}

	void Start()
	{
		inventory = new Inventory ();
		inventory.productsCounts = new int[BASE.Instance.properties.Length];
	}

	void FixedUpdate () 
	{
		timer -= Time.deltaTime;
		timerText.text = string.Format ("{0:00}:{1:00}", (int) timer / 60, (int) timer % 60); 
		moneyText.text = SaveManager.coinsCount.ToString ();
		expText.text = SaveManager.currentScore.ToString ();
	}
}

[System.Serializable]
public class Inventory
{
	public int mainProductCount;
	public int[] productsCounts;
}
