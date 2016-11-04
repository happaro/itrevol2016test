using UnityEngine;
using System.Collections;

public class HumanInputer : Building 
{
	public override void DoTasks ()
	{
        resourceObject.gameObject.SetActive(true);
        float xScale = timer / speed * 1.4f;
        doneIcon.transform.localScale = new Vector3(xScale, doneIcon.transform.localScale.y, doneIcon.transform.localScale.z);

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
	}

	public override void CollectResources ()
	{
		main.inventory.mainProductCount += doneTasks;
		doneTasks = 0;
	}
}
