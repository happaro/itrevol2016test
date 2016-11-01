using UnityEngine;
using System.Collections;
using System.Timers;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapController : MonoBehaviour 
{
	float xStep = 0.5f, yStep = 0.25f;
	public GameObject tilePrefab;
	public Sprite standartSprite;

	public TilePack[] packs;
	public Transform tilesPlace;

	public TilePack currentPack {get{return packs[currentBuildPack];}}
	int currentBuildPack = 0;

	public Tile[,] tiles = new Tile[30, 30];
		
	void Start () 
	{
		InitTiles();
	}

	void InitTiles()
	{
		for (int i = 0; i < 30; i++)
		{
			for (int j = 0; j < 30; j++)
			{
				var go = Instantiate(tilePrefab, new Vector3(j * xStep - i * xStep, j * yStep + i * yStep, Mathf.Sqrt(i*i + j*j)), Quaternion.identity) as GameObject;
				go.transform.parent = tilesPlace;

			}
		}
	}

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