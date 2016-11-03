using UnityEngine;
using System.Collections;

public class BuyButton : Button
{
	public BuildingType type;
	public TextMesh name;
	public TextMesh price;

	protected override void Action ()
	{
		if (SaveManager.coinsCount >= BASE.Instance.GetBuildPrice (type, 0)) 
		{
			WindowManager.Instance.GetWindow<GUI> ().Open ();
			WindowManager.Instance.GetWindow<WindowShop> ().Close ();
			BuyBuild ();
		}
		else 
		{
			WindowManager.Instance.GetWindow<WindowShop> ().Close ();
			WindowManager.Instance.GetWindow<WindowInfo> ().Open ("Облом", "У вас недостаточно средств \nдля приобритения \n " + BASE.Instance.GetBuildName(type), () => 
				{
					WindowManager.Instance.GetWindow<WindowInfo>().Close();
					WindowManager.Instance.GetWindow<WindowShop>().Open();
				});
		}
	}

	void BuyBuild()
	{
		GameObject.FindObjectOfType<MapController> ().CreateBuilding (type);
		//WindowManager.Instance.GetWindow<GUI>
	}
}
