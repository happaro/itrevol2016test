using UnityEngine;
using System.Collections;

public class SaleButton : Button
{
	public BuildingType type;

	public TextMesh count;
	public TextMesh price;
	public SpriteRenderer icon;

	/*protected override void Action ()
	{
		//base.myAction ();
		WindowManager.Instance.GetWindow<WindowProductSaling> ().Open (type);
		WindowManager.Instance.GetWindow<WindowInventory> ().Close (false);
	}*/
}
