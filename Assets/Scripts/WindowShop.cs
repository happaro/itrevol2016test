using UnityEngine;
using System.Collections;

public class WindowShop : Window 
{
	public Button closeButton;
    public Button nextButton;
    public Button prevButton;
	public BuyButton[] buttons;
	int currentPage = 0;
	void Start () 
	{
		closeButton.myAction = () => 
		{
            base.Close(true);
			WindowManager.Instance.GetWindow<GUI>().Open();
		};
        nextButton.myAction = () =>
        {
            if (currentPage < BASE.Instance.properties.Length/3)
            currentPage++;
            UpdateItems();
        };
        prevButton.myAction = () =>
        {
            if (currentPage > 0)
            {
                currentPage--;
            }            
            UpdateItems();
        };
		//UpdateItems ();
	}

	public override void Open ()
	{
		base.Open(false);
		UpdateItems ();
	}

	void UpdateItems()
	{
        if (currentPage == 0)
        {
            prevButton.gameObject.SetActive(false);
        }
        else
        {
            prevButton.gameObject.SetActive(true);
        }
        if (currentPage == BASE.Instance.properties.Length / 3)
        {
            nextButton.gameObject.SetActive(false);
        }
        else
        {
            nextButton.gameObject.SetActive(true);
        }
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
                buttons[i].transform.parent.transform.parent.gameObject.SetActive(true);
			}
			else 
			{
                buttons[i].transform.parent.transform.parent.gameObject.SetActive(false);
			}
			
		}
	}
}
