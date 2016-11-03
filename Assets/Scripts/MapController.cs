using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapController : MonoBehaviour 
{
	public GameObject tilePrefab, buildingPrefab, mainBuildingPrefab;
	public Transform tilesPlace;
    public Transform buildingPlace;
	public GameObject[] mapPatterns;
	//public Tile[,] tiles = new Tile[30, 30];

	public List<Building> buildings = new List<Building> ();

	public BASE basePrefab;

    public WindowBuildingCreation buildingCreationWindow;

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

	public void CreateFirstBuilding()
	{
		buildingCreationWindow.transform.position = CoordinateConvertor.SimpleToIso(new Point(15, 15));
		var go = Instantiate(mainBuildingPrefab, CoordinateConvertor.SimpleToIso(new Point(15, 15)), Quaternion.identity) as GameObject;
		Building building = go.GetComponent<Building>();
		go.transform.parent = buildingCreationWindow.transform;
		buildingCreationWindow.SetSelectedBuilding(building);
		buildingCreationWindow.SetPosition(buildingCreationWindow.transform.position);
		building.InitializeBuilding (BuildingType.FootBuilding);
	}

    public void FinishBuilding()
    {
        buildingCreationWindow.selectedBuilding.transform.parent = buildingPlace;
        buildings.Add(buildingCreationWindow.selectedBuilding);
		buildingCreationWindow.selectedBuilding.isBuilded = true;
		SaveManager.coinsCount -= BASE.Instance.GetBuildPrice (buildingCreationWindow.selectedBuilding.buildingType, 0);

    }

    public void CancelBuilding()
    {
        Destroy(buildingCreationWindow.selectedBuilding.gameObject);
    }

	void Start () 
	{
		Instantiate (basePrefab);
		//InitTiles();
		ItitMap ();
		WindowManager.Instance.GetWindow<WindowInfo> ().Open ("Tutorial", "Hello, mutherfucker\n For now, build your\nfirst fucking building!", () =>
			{
				WindowManager.Instance.GetWindow<WindowInfo>().Close();
				//WindowManager.Instance.GetWindow<GUI>().Open();
				CreateFirstBuilding();
			});

	}

	void ItitMap()
	{
		var dt1 = DateTime.Now;
		int random = UnityEngine.Random.Range (0, mapPatterns.Length);
		var go = Instantiate(mapPatterns[random], mapPatterns[random].transform.position, mapPatterns[random].transform.rotation) as GameObject;
	}

    void InitTiles()
	{
		var dt1 = DateTime.Now;
		for (int i = 0; i < 30; i++)
			for (int j = 0; j < 30; j++) 
			{
                var go = Instantiate(tilePrefab, CoordinateConvertor.SimpleToIso(new Point(i,j)), Quaternion.identity) as GameObject;				
				go.transform.parent = tilesPlace;
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
