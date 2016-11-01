using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour 
{
	public int X;
	public int Y;
	public TileType type;
	public int level;
	public SpriteRenderer mySpriteRenderer;

	public void ChangeTileType()
	{
		type = GameObject.FindObjectOfType<MapController>().currentPack.type;
		this.GetComponent<SpriteRenderer>().sprite = GameObject.FindObjectOfType<MapController>().currentPack.sprite;
	}

	void Start () 
	{
		mySpriteRenderer = GetComponent<SpriteRenderer>();
	}
}