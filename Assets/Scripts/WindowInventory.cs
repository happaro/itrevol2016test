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
			base.Close();
			WindowManager.Instance.GetWindow<GUI>().Open();
		};
	}

	public override void Open ()
	{
		base.Open ();
		UpdateInventory ();
	}

	void UpdateInventory()
	{
		foreach (var button in buttons)
			Destroy (button.gameObject);
		buttons.Clear ();

		for (int i = 0; i < MainController.ins.inventory.productsCouns.Length; i++) 
		{
			if (MainController.ins.inventory.productsCouns [i] > 0) 
			{
				var go = Instantiate(productPrefab, this.transform.position + productPrefab.transform.position + new Vector3((i % 3) * step, (i / 3) * -step), productPrefab.transform.rotation) as GameObject;
				var button = go.GetComponent<BuyButton> ();
				button.type = (BuildingType)i;
				button.price.text = BASE.Instance.GetResourcePrice (button.type).ToString() + "$";
				button.icon.sprite = BASE.Instance.GetBuildingResource (button.type);
				button.transform.parent = transform;
				buttons.Add (button);
			}
		}
	}
}
