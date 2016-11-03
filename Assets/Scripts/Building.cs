using UnityEngine;
using System.Collections;

public enum BuildingType {HandBuilding = 0, FootBuilding, Shop}

public class Building : MonoBehaviour 
{
	public int buildLevel = 1;
	public bool isBuilded = false;
	public BuildingType buildingType;

	public void InitializeBuilding(BuildingType type)
	{
		buildingType = type;
		var props = BASE.Instance.properties [(int)buildingType];
		this.GetComponent<SpriteRenderer> ().sprite = props.sprite;
	}
}
