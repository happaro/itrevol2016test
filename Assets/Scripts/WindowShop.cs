using UnityEngine;
using System.Collections;

public class WindowShop : Window 
{
	public Button closeButton;
	public BuyButton[] buttons;
	int currentPage = 0;

	void Start () 
	{
		closeButton.myAction = () => 
		{
			base.Close();
			WindowManager.Instance.GetWindow<GUI>().Open();
		};
		//UpdateItems ();
	}

	public override void Open ()
	{
		base.Open ();
		UpdateItems ();
	}

	void UpdateItems()
	{
		for (int i = 0; i < buttons.Length; i++) 
		{
			if (BASE.Instance.properties.Length > currentPage * buttons.Length + i) 
			{
				var bType = (BuildingType)(currentPage * buttons.Length + i);
				buttons [i].type = bType;
				buttons [i].transform.parent.gameObject.SetActive (true);
				buttons [i].GetComponent<SpriteRenderer>().sprite = BASE.Instance.GetBuildingSprite(bType);
				buttons [i].name.text = BASE.Instance.GetBuildName(bType);
				buttons [i].price.text = BASE.Instance.GetBuildPrice(bType, 0).ToString();
			}
			else 
			{
				buttons [i].transform.parent.gameObject.SetActive (false);
			}
			
		}
	}
}
