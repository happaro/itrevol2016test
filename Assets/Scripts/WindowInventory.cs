using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindowInventory : Window 
{
	public Button closeButton;
	public GameObject productPrefab;

	public List<BuyButton> buttons = new List<BuyButton> ();
	int currentPage = 0;
	float step = 1.2f;

	void Start () 
	{
		closeButton.myAction = () => 
		{
            base.Close(true);
			WindowManager.Instance.GetWindow<GUI>().Open();
		};
	}

	public override void Open()
	{
		base.Open(false);
		UpdateInventory();
	}

	void UpdateInventory()
	{
		foreach (var button in buttons)
			Destroy (button.gameObject);
		buttons.Clear();

		int i = 0;
		if (MainController.ins.inventory.mainProductCount > 0) 
		{
			var go = Instantiate(productPrefab, this.transform.position + productPrefab.transform.position, productPrefab.transform.rotation) as GameObject;
			var button = go.GetComponent<BuyButton> ();
			button.price.text = "0$";
			button.name.text = MainController.ins.inventory.mainProductCount.ToString ();
			button.transform.parent = transform;
			buttons.Add (button);
			i = 1;
		}
			
		for (; i < MainController.ins.inventory.productsCounts.Length; i++) 
		{
			if (MainController.ins.inventory.productsCounts [i] > 0) 
			{
				var go = Instantiate(productPrefab, this.transform.position + productPrefab.transform.position + new Vector3((i % 3) * step, (i / 3) * -step), productPrefab.transform.rotation) as GameObject;
				var button = go.GetComponent<BuyButton> ();
				button.type = (BuildingType)i;
				button.price.text = BASE.Instance.GetResourcePrice (button.type).ToString() + "$";
				button.name.text = MainController.ins.inventory.mainProductCount.ToString ();
				button.icon.sprite = BASE.Instance.GetBuildingResource (button.type);
				button.transform.parent = transform;
				buttons.Add (button);
			}
		}
	}
}
