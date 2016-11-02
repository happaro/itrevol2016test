using UnityEngine;
using System.Collections;

public class TouchHandler : MonoBehaviour 
{
	
	public MapController map;

	void Start()
	{
		
	}

	void Update () 
	{
		if (map.movingBuilding != null && Input.GetMouseButton(0))
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if(hit.collider != null && hit.transform.tag == "Tile")
			{
			//	if(hit.transform.tag == "Tile")
			//		hit.transform.GetComponent<Tile>().ChangeTileType();	
				float X = hit.point.x - hit.point.x % 0.5f;
				float Y = hit.point.y - hit.point.y % 0.25f;

				//int xPos = Mathf.RoundToInt(X/ 0.5f);
				//int yPos = Mathf.RoundToInt(Y / 0.25f);

				//map.y == screen.y / TILE_HEIGHT_HALF - (screen.x / TILE_WIDTH_HALF + map.y)

				int xPos = (int)(X / 0.5f - Y / 0.25f)/2;
				int yPos = (int)(Y / 0.25f + (X / 0.5f + Y))/2-3;



				Debug.LogWarning (xPos + " " + yPos);

				map.movingBuilding.transform.position = new Vector3 ((yPos + xPos) * 0.5f, (yPos - xPos) * 0.25f, Mathf.Sqrt(xPos * xPos + yPos * yPos));

					//new Vector3(j * xStep - i * xStep, j * yStep + i * yStep, Mathf.Sqrt(i*i + j*j)), Quaternion.identity) as GameObject;
			}
		}
	}


}
