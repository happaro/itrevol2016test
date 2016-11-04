using UnityEngine;
using System.Collections;

public class WindowProductSaling : Window
{
    public Button okButton, cancelButton, plusButton, minusButton, allButton;
    public TextMesh countText;
    public SpriteRenderer saleSprite;

	private MainController main;
	private BuildingType saleType;
	private int saleCount = 0;

	public void UpdateButtons()
	{
		okButton.myAction = () =>
		{
			main.inventory.productsCounts[(int)saleType] -= saleCount;
			SaveManager.coinsCount += BASE.Instance.GetResourcePrice(saleType) * saleCount;
			SaveManager.currentScore += BASE.Instance.GetResourcePrice(saleType) * saleCount;
			base.Close(true);
			WindowManager.Instance.GetWindow<GUI>().Open(true);
		};

		plusButton.myAction = () =>
		{
			if (main.inventory.productsCounts[(int)saleType] > saleCount)
				saleCount++;         
			UpdateText();
			UpdateButtonState();
		};

		minusButton.myAction = () =>
		{
			if (saleCount > 0)
			{
				saleCount--;
				UpdateText();
				UpdateButtonState();
			}
		};

		allButton.myAction = () =>
		{
			saleCount = main.inventory.productsCounts[(int)saleType];
			UpdateText();
			UpdateButtonState();
		};
	}

	public void Awake()
    {
		cancelButton.myAction = () =>
        {
            base.Close(true);
			WindowManager.Instance.GetWindow<GUI>().Open(true);
        };
    }

    void UpdateText()
    {
        countText.text = saleCount.ToString();
    }

	void UpdateButtonState()
	{
		okButton.gameObject.SetActive (saleCount > 0);	
		plusButton.SetActive (main.inventory.productsCounts [(int)saleType] > saleCount);
		minusButton.SetActive (saleCount > 0);
		allButton.SetActive(main.inventory.productsCounts [(int)saleType] > saleCount);
	}

	public void Open(BuildingType sType)
	{
		main = GameObject.FindObjectOfType<MainController> ();
		saleType = sType;
		saleCount = 0;
		saleSprite.sprite = BASE.Instance.GetBuildingResource (saleType);
        UpdateText();
		UpdateButtons ();
		UpdateButtonState ();
        base.Open(false);
    }
}
