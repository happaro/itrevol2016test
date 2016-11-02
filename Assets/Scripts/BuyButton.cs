using UnityEngine;
using System.Collections;

public class BuildButton : Button
{
	public enum BuildingType
	{
		HandBuilding,
		FootBuilding,
		Shop
	}

	protected virtual void Action()
	{
		BuyBuild ();
	}

	void BuyBuild()
	{
		
	}

	void UpgradeBuild()
	{
		
	}
}
