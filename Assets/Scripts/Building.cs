using UnityEngine;
using System.Collections;

public enum BuildingType {HandBuilding = 0, FootBuilding, Shop}

public class Building : MonoBehaviour 
{
	public int buildLevel = 1;
	public bool isBuilded = false;
	public BuildingType buildingType;
}
