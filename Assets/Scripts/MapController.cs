using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapController : MonoBehaviour 
{
	public GameObject tilePrefab, buildingPrefab;
	public Sprite standartSprite;

	public TilePack[] packs;
	public Transform tilesPlace;

	public TilePack currentPack {get{return packs[currentBuildPack];}}
	int currentBuildPack = 0;

	public Tile[,] tiles = new Tile[30, 30];

	public List<Building> buildings = new List<Building> ();

	public BASE basePrefab;

	public Building movingBuilding;

	public void CreateBuilding(BuildingType type)
	{
        var go = Instantiate(buildingPrefab, CoordinateConvertor.SimpleToIso(new Point(15, 15)), Quaternion.identity) as GameObject;	
		//buildings.Add (go.GetComponent<Building> ());
		movingBuilding = go.GetComponent<Building> ();
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
			}
	}



	//-----EDITOR
	public void NextPack()
	{
		if (currentBuildPack >= packs.Length - 1)
			currentBuildPack = 0;
		else currentBuildPack++;
	}

	public void PrevPack()
	{
		if (currentBuildPack == 0)
			currentBuildPack = packs.Length - 1;
		else currentBuildPack--;
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

#if UNITY_EDITOR
[CustomEditor(typeof(MapController))]
public class MapContorllerEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		var map = target as MapController;
		if (map.currentPack.sprite != null)
		{
			GUILayout.Label("Current tile");
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("<-"))
				map.NextPack();
			GUILayout.Label(map.currentPack.sprite.texture);
			if (GUILayout.Button("->"))
				map.PrevPack();
			GUILayout.EndHorizontal();
		}

	}
}
#endif