using UnityEngine;
using System.Collections;

public class BuyButton : Button
{
	public BuildingType type;

	protected override void Action ()
	{
		if (SaveManager.coinsCount > BASE.Instance.GetBuildPrice (type, 1)) 
		{
			WindowManager.Instance.GetWindow<GUI> ().Open ();
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
		Debug.LogWarning ("PLACE FOR BUY");
	}
}
