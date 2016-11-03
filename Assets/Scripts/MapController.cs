using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class MapController : MonoBehaviour 
{
	public GameObject tilePrefab, buildingPrefab;
	public Sprite standartSprite;

	public TilePack[] packs;
	public Transform tilesPlace;
    public Transform buildingPlace;


	public Tile[,] tiles = new Tile[30, 30];

	public List<Building> buildings = new List<Building> ();

	public BASE basePrefab;

    public WindowBuildingCreation buildingCreationWindow;
    public WindowBuildingUpdating buildingUpdatingWindow;

	public void CreateBuilding(BuildingType type)
	{
        buildingCreationWindow.transform.position = CoordinateConvertor.SimpleToIso(new Point(15, 15));
        var go = Instantiate(buildingPrefab, CoordinateConvertor.SimpleToIso(new Point(15, 15)), Quaternion.identity) as GameObject;
        Building building = go.GetComponent<Building>();
        go.transform.parent = buildingCreationWindow.transform;
        buildingCreationWindow.SetSelectedBuilding(building);
        buildingCreationWindow.SetPosition(buildingCreationWindow.transform.position);
		building.InitializeBuilding (type);
	}

    public void FinishBuilding()
    {
        buildingCreationWindow.selectedBuilding.transform.parent = buildingPlace;
        buildings.Add(buildingCreationWindow.selectedBuilding);
    }

    public void CancelBuilding()
    {
        Destroy(buildingCreationWindow.selectedBuilding.gameObject);
    }

	void Start () 
	{
		Instantiate (basePrefab);
		InitTiles();
	}

    void InitTiles()
	{
		for (int i = 0; i < 30; i++)
			for (int j = 0; j < 30; j++) 
			{
                var go = Instantiate(tilePrefab, CoordinateConvertor.SimpleToIso(new Point(i,j)), Quaternion.identity) as GameObject;				
				go.transform.parent = tilesPlace;
				tiles [i, j] = go.GetComponent<Tile> ();
			}
	}
}

public enum TileType {Water, Dirt, Grass, Lot}

[System.Serializable]
public class TilesMatrix
{
	public int[,] tiles = new int[10, 10];
}

[System.Serializable]
public class TilePack
{
	public TileType type;
	public Sprite sprite;
}
