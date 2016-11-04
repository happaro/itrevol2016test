using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class TouchHandler : MonoBehaviour
{
	WindowBuildingUpdating upgradingWindow;
	WindowBuildingCreation creatingWindow;
	bool scrolling = false;

	public Vector3 startMousePosition;
	public Vector3 startCameraPosition;
	public Camera villageCam;
	public Camera GUICam;

	public bool allowScroll = false;
	public bool moving = false;

	void Start()
	{
		upgradingWindow = WindowManager.Instance.GetWindow<WindowBuildingUpdating> ();
		creatingWindow = WindowManager.Instance.GetWindow<WindowBuildingCreation> ();
		scrolling = false;
	}

    void Update()
    {

		if (Input.GetMouseButtonDown (0))
		{
			startMousePosition = GUICam.ScreenToWorldPoint(Input.mousePosition);
			startCameraPosition = villageCam.transform.position;
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (hit.collider != null && creatingWindow.selectedBuilding != null && hit.collider.gameObject == creatingWindow.selectedBuilding.gameObject)
				moving = true;
			scrolling = false;
		}
			

        if (Input.GetMouseButton(0))
        {
			if (moving) 
			{
				BuildingTapCheck ();
				return;
			}

			if (!allowScroll)
				return;
			var newMousePosition = GUICam.ScreenToWorldPoint(Input.mousePosition);
			Vector3 newPosition = startMousePosition - newMousePosition;
			HOTween.Kill (villageCam.gameObject);
			HOTween.To(villageCam.transform, 0.3f, "position", startCameraPosition + new Vector3 (newPosition.x, newPosition.y, 0));
			//villageCam.transform.position = startCameraPosition + new Vector3 (newPosition.x, newPosition.y, 0);
			if (Mathf.Abs(newPosition.x) > 0.3f || Mathf.Abs(newPosition.y) > 0.3f)
				scrolling = true;
        }

		if (Input.GetMouseButtonUp (0) && !scrolling) 
		{
			if (!moving) 
				BuildingTapCheck ();
			moving = false;
		}


    }
	void BuildingTapCheck()
	{
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (hit.collider != null && (hit.transform.tag == "Tile" || hit.transform.tag == "Building"))
		{
			//якщо відкрито вікно оновлення - закриваєм його
            if (upgradingWindow.selectedBuilding)
            {
                if (hit.collider != null && hit.transform.tag == "Building")
                {
                    var building = hit.transform.GetComponent<Building>();
                    if (upgradingWindow.selectedBuilding == building)
                    {
                        WindowManager.Instance.GetWindow<WindowProductCreation>().Open(building);
                    }
                }
                upgradingWindow.SetSelectedBuilding(null);
            }
			if (creatingWindow.selectedBuilding)
			{
				Point point = CoordinateConvertor.IsoToSimple(hit.point);
				creatingWindow.SetPosition(CoordinateConvertor.SimpleToIso(point));
			}
		}
		// якщо начого не строїмо
		if(!creatingWindow.selectedBuilding)
		{
			// і натискаєм на побудований будинок
			if (hit.collider != null && hit.transform.tag == "Building")
			{
				var building = hit.transform.GetComponent<Building>();
				upgradingWindow.SetSelectedBuilding (building);
				Point point = CoordinateConvertor.IsoToSimple(building.transform.position);
				upgradingWindow.SetPosition (CoordinateConvertor.SimpleToIso (point));
			}
		}	
	}

}
