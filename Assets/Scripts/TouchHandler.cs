using UnityEngine;
using System.Collections;

public class TouchHandler : MonoBehaviour 
{


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButton(0))
		{
			//Debug.LogWarning("ss");
			// - new Vector3(0, Camera.main.transform.position.y)

			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			//Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
			//Debug.LogWarning(ray.origin);
			//RaycastHit hit;
			if(hit.collider != null)
			{
				if(hit.transform.tag == "Tile")
					hit.transform.GetComponent<Tile>().ChangeTileType();
				
			}
			
		}
	}
}
