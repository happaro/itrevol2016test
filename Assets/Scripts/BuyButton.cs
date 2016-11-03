using UnityEngine;
using System.Collections;

public class BuyButton : Button
{
	public BuildingType type;
	public TextMesh name;
	public TextMesh price;
	public SpriteRenderer icon;

	protected override void Action ()
	{
		if (SaveManager.coinsCount >= BASE.Instance.GetBuildPrice (type, 0)) 
		{
			//WindowManager.Instance.GetWindow<GUI> ().Close ();
            WindowManager.Instance.GetWindow<WindowShop>().Close(true);
			BuyBuild ();
		}
		else 
		{
            WindowManager.Instance.GetWindow<WindowShop>().Close(true);
			WindowManager.Instance.GetWindow<WindowInfo> ().Open ("Облом", "У вас недостаточно средств \nдля приобритения \n " + BASE.Instance.GetBuildName(type), () => 
				{
					WindowManager.Instance.GetWindow<WindowInfo>().Close(true);
					WindowManager.Instance.GetWindow<WindowShop>().Open(false);
				});
		}
	}

	void BuyBuild()
	{
		GameObject.FindObjectOfType<MapController> ().CreateBuilding (type);
		//WindowManager.Instance.GetWindow<GUI>
	}
}
