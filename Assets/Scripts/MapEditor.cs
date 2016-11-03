using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapEditor : MonoBehaviour
{
	public BASE _base;
	public MapController map;
	public Sprite[] tilesTypes;

	//public TilePack[] packs;
	public Sprite[] tilesSprites;

	//public TilePack currentPack {get{return packs[currentBuildPack];}}
	//int currentBuildPack = 0;

	public int currentSpriteNum = 0;
	public bool allowBuild;

	public Vector3 startPosition;
	public Vector3 startCamPosition;

	public void Update()
	{
		if (Input.GetMouseButton(0))
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if(hit.collider != null && hit.transform.tag == "Tile")
			{
				hit.transform.GetComponent<SpriteRenderer> ().sprite = tilesSprites [currentSpriteNum];
				hit.transform.GetComponent<Tile> ().allowBuild = allowBuild;
			}
		}

		if (Input.GetMouseButtonDown (1)) 
		{
			Debug.LogWarning ("Down");
			startPosition = Input.mousePosition;
			startCamPosition = Camera.main.transform.position;
		}

		if (Input.GetMouseButton (1)) 
		{
			Camera.main.transform.position = new Vector3 (startCamPosition.x + (Input.mousePosition.x - startPosition.x) / 20f, startCamPosition.y + (Input.mousePosition.y - startPosition.y) / 20f, -10);

		}
	}

	//-----EDITOR
	public void NextPack()
	{
		if (currentSpriteNum >= tilesSprites.Length - 1)
			currentSpriteNum = 0;
		else currentSpriteNum++;
	}

	public void PrevPack()
	{
		if (currentSpriteNum == 0)
			currentSpriteNum = tilesSprites.Length - 1;
		else currentSpriteNum--;
	}


}


#if UNITY_EDITOR
[CustomEditor(typeof(MapEditor))]
public class MapEditorEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		var map = target as MapEditor;
		if (map.tilesSprites.Length > 0 && map.tilesSprites[map.currentSpriteNum] != null)
		{
			GUILayout.Label("Current tile");
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("<-"))
				map.NextPack();
			if (map.tilesSprites[map.currentSpriteNum] != null)
				GUILayout.Label(map.tilesSprites[map.currentSpriteNum].texture);
			if (GUILayout.Button("->"))
				map.PrevPack();
			GUILayout.EndHorizontal();
		}

	}
}
#endif