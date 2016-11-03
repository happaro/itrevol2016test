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
        if (map.buildingCreationWindow != null && Input.GetMouseButton(0))
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if(hit.collider != null && hit.transform.tag == "Tile")
			{
                Point point = CoordinateConvertor.IsoToSimple(hit.point);
                map.buildingCreationWindow.transform.position = CoordinateConvertor.SimpleToIso(point);
			}
		}
	}


}
