using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindowInventory : Window 
{
	public Button closeButton;
	public GameObject productPrefab;

	public List<SaleButton> buttons = new List<SaleButton> ();
	int currentPage = 0;
	float stepX = 1.8f;
	float stepY = 1.4f;

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

		int j = 0;
		if (MainController.ins.inventory.mainProductCount > 0) 
		{
			var go = Instantiate(productPrefab, this.transform.position + productPrefab.transform.position, productPrefab.transform.rotation) as GameObject;
			var button = go.GetComponent<SaleButton> ();
			button.price.text = "0$";
			button.count.text = MainController.ins.inventory.mainProductCount.ToString ();
			button.transform.parent = transform;
			buttons.Add (button);
			j = 1;
			button.myAction = () => 
			{
				WindowManager.Instance.GetWindow<WindowInventory>().Close(false);
				WindowManager.Instance.GetWindow<WindowInfo>().Open("Нет", "Данный ресурс\nне подлежит продажи\n", () => 
					{
						WindowManager.Instance.GetWindow<WindowInfo>().Close();
						WindowManager.Instance.GetWindow<WindowInventory>().Open(false);
					});
			};
		}

		for (int i = 0; i < MainController.ins.inventory.productsCounts.Length; i++) 
		{
			if (MainController.ins.inventory.productsCounts [i] > 0) {
				var go = Instantiate (productPrefab, this.transform.position + productPrefab.transform.position + new Vector3 (((i + j) % 2) * stepX, ((i + j) / 2) * -stepY), productPrefab.transform.rotation) as GameObject;
				var button = go.GetComponent<SaleButton> ();
				button.type = (BuildingType)i;
				button.price.text = BASE.Instance.GetResourcePrice (button.type).ToString () + "$";
				button.count.text = MainController.ins.inventory.productsCounts [i].ToString ();
				button.icon.sprite = BASE.Instance.GetBuildingResource (button.type);
				button.transform.parent = transform;
				buttons.Add (button);

				button.myAction = () => {
					WindowManager.Instance.GetWindow<WindowProductSaling> ().Open (button.type);
					WindowManager.Instance.GetWindow<WindowInventory> ().Close (false);
				};

			} else
				j--;
		}
	}
}
