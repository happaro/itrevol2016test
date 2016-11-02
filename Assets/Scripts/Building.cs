using UnityEngine;
using System.Collections;

public enum BuildingType {HandBuilding, FootBuilding, Shop}

public class Building : MonoBehaviour 
{
	public int buildLevel = 1;
	public BuildingType buildingType;
}
