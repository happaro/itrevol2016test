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
        SurpriseMazafaka();
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
    
    public void SurpriseMazafaka()
    {
        int[] points = new int[]
        {
            11*30+19,12*30+18,13*30+17,13*30+18,13*30+19,13*30+20,14*30+20,15*30+20,15*30+19, //2
            15*30+15,16*30+15,16*30+14,16*30+16,17*30+15,                                     //x
            18*30+12,19*30+11,20*30+10,20*30+11,20*30+12,20*30+13,21*30+13,22*30+13,22*30+12  //2
        };
        foreach (var point in points)
        {
            var tileRender = tilesPlace.GetChild(point).GetComponent<SpriteRenderer>();
            tileRender.sprite = standartSprite;
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