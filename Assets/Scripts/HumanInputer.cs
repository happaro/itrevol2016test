using UnityEngine;
using System.Collections;

public class HumanInputer : Building 
{
	public override void DoTasks ()
	{
		base.timer += Time.deltaTime;
		if (timer > speed) 
		{
			doneTasks++;
			timer = 0;
			ProductInputer.ins.InputProduct (BuildingType.FootBuilding, true);
			//UpdateGetter ();
		}
	}

	public override void InitializeBuilding (BuildingType type)
	{
		//base.InitializeBuilding (type);
	}

	public override void CollectResources ()
	{
		main.inventory.mainProductCount += doneTasks;
		doneTasks = 0;
	}
}
