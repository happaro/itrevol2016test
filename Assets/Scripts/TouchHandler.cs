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
    public bool isGUI = false;
    public bool moving = false;

    public float orthoZoomSpeed = 0.01f;        // The rate of change of the orthographic size in orthographic mode.
    public float orthoMinSize = 0.8f;
    public float orthoMaxSize = 4f;
    private bool wasDoubletouch = false;
    void Start()
    {
        upgradingWindow = WindowManager.Instance.GetWindow<WindowBuildingUpdating>();
        creatingWindow = WindowManager.Instance.GetWindow<WindowBuildingCreation>();
        scrolling = false;
    }

    void Update()
    {

		#if UNITY_ANDROID
        if(Input.touchCount == 0)
			return;
        #endif
        //Zoom
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ... change the orthographic size based on the change in distance between the touches.
            villageCam.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

            // Make sure the orthographic size never drops below zero.
            villageCam.orthographicSize = Mathf.Max(villageCam.orthographicSize, 0.1f);

            if (villageCam.orthographicSize > orthoMaxSize) villageCam.orthographicSize = orthoMaxSize;
            if (villageCam.orthographicSize < orthoMinSize) villageCam.orthographicSize = orthoMinSize;
            wasDoubletouch = true;
            return;
        }
        bool began = Input.GetMouseButtonDown(0); 
        #if UNITY_ANDROID
        began = Input.GetTouch(0).phase == TouchPhase.Began;
        #endif
        if (began || wasDoubletouch)
        {
            wasDoubletouch = false;
            startMousePosition = GUICam.ScreenToWorldPoint(Input.mousePosition);
            startCameraPosition = villageCam.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && creatingWindow.selectedBuilding != null && hit.collider.gameObject == creatingWindow.selectedBuilding.gameObject)
                moving = true;
        }

        bool moved = Input.GetMouseButton(0); 
        #if UNITY_ANDROID
        moved = Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary;
        #endif
        if (moved)
        {
            if (moving)
            {
                BuildingTapCheck();
                return;
            }

            if (!allowScroll)
                return;
            var newMousePosition = GUICam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = startMousePosition - newMousePosition;
            //HOTween.Kill (villageCam.gameObject);
            //HOTween.To(villageCam.transform, 0.3f, "position", startCameraPosition + new Vector3 (newPosition.x, newPosition.y, 0)); 
            villageCam.transform.position = startCameraPosition + new Vector3(newPosition.x, newPosition.y, 0);
            if (villageCam.transform.position.x > 7) villageCam.transform.position = new Vector3(7, villageCam.transform.position.y, villageCam.transform.position.z);
            if (villageCam.transform.position.x < -7) villageCam.transform.position = new Vector3(-7, villageCam.transform.position.y, villageCam.transform.position.z);
            if (villageCam.transform.position.y > 12) villageCam.transform.position = new Vector3(villageCam.transform.position.x, 12, villageCam.transform.position.z);
            if (villageCam.transform.position.y < 3) villageCam.transform.position = new Vector3(villageCam.transform.position.x, 3, villageCam.transform.position.z);
            if (Mathf.Abs(newPosition.x) > 0.3f || Mathf.Abs(newPosition.y) > 0.3f)
            {
                scrolling = true;
            }
            else
            {
                scrolling = false;
            }
           
        }
        bool ended = Input.GetMouseButtonUp(0); 
        #if UNITY_ANDROID
        ended = Input.GetTouch(0).phase == TouchPhase.Ended;
        #endif
        if (ended)
        {
            if (!moving && !isGUI && !scrolling)
                BuildingTapCheck();
            moving = false;
        }


    }

    void BuildingTapCheck()
    {
        Debug.Log("BuildingTapCheck");
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && (hit.transform.tag == "Tile" || hit.transform.tag == "Building"))
        {
            Debug.Log(hit.transform.tag);
            //якщо відкрито вікно оновлення - закриваєм його
            if (upgradingWindow.selectedBuilding)
            {
                if (hit.collider != null && hit.transform.tag == "Building")
                {
                    var building = hit.transform.GetComponent<Building>();
                    if (upgradingWindow.selectedBuilding == building)
                    {
                        if (!(building is HumanInputer))
                        {
                            WindowManager.Instance.GetWindow<WindowProductCreation>().Open(building);
                            WindowManager.Instance.GetWindow<GUI>().Close(false);
                            upgradingWindow.SetSelectedBuilding(null);
                        }
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
        if (!creatingWindow.selectedBuilding)
        {
            // і натискаєм на побудований будинок
            if (hit.collider != null && hit.transform.tag == "Building")
            {
                var building = hit.transform.GetComponent<Building>();
                upgradingWindow.SetSelectedBuilding(building);
                Point point = CoordinateConvertor.IsoToSimple(building.transform.position);
                upgradingWindow.SetPosition(CoordinateConvertor.SimpleToIso(point));
            }
        }
    }

}
