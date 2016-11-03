using UnityEngine;
using System.Collections;

public class TouchHandler : MonoBehaviour
{
	WindowBuildingUpdating upgradingWindow;
	WindowBuildingCreation creatingWindow;
	bool scrolling = false;

	public Vector3 startMousePosition;
	public Vector3 startCameraPosition;
	public Camera villageCam;
	public Camera GUICam;

	public bool allowScroll;
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
			scrolling = false;
		}
			

        if (Input.GetMouseButton(0))
        {
			if (!allowScroll)
				return;
			var newMousePosition = GUICam.ScreenToWorldPoint(Input.mousePosition);
			Vector3 newPosition = startMousePosition - newMousePosition;
			villageCam.transform.position = startCameraPosition + new Vector3 (newPosition.x, newPosition.y, 0);
			if (Mathf.Abs(newPosition.x) > 0.3f || Mathf.Abs(newPosition.y) > 0.3f)
				scrolling = true;
        }


		if (Input.GetMouseButtonUp (0) && !scrolling) 
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (hit.collider != null && (hit.transform.tag == "Tile" || hit.transform.tag == "Building"))
			{
				//якщо відкрито вікно оновлення - закриваєм його
				if (upgradingWindow.selectedBuilding)
					upgradingWindow.SetSelectedBuilding(null);
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
}
