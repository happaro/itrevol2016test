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
                int i = Mathf.RoundToInt(2 * hit.point.y - hit.point.x);
                int j = Mathf.RoundToInt(2 * hit.point.x + i);                
                int xPos = (int)((j - i) - (j * 0.25 + i * 0.25));
                int yPos = (int)((j + i) + (j * 0.5 - i * 0.5));
                map.movingBuilding.transform.position = new Vector3((j - i) * 0.5f, (j + i) * 0.25f, Mathf.Sqrt(xPos * xPos + yPos * yPos));
			}
		}
	}


}
