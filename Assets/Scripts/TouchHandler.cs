using UnityEngine;
using System.Collections;

public class TouchHandler : MonoBehaviour
{
    public MapController map;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && (hit.transform.tag == "Tile" || hit.transform.tag == "Building"))
            {
                //якщо відкрито вікно оновлення - закриваєм його
                if (map.buildingUpdatingWindow.selectedBuilding != null)
                {
                    map.buildingUpdatingWindow.SetSelectedBuilding(null);
                }
                //якщо строємо - переміщаєм вікно постойки
                if (map.buildingCreationWindow.selectedBuilding != null)
                {
                    Point point = CoordinateConvertor.IsoToSimple(hit.point);
                    map.buildingCreationWindow.SetPosition(CoordinateConvertor.SimpleToIso(point));
                }
            }
            // якщо начого не строїмо
            if( map.buildingCreationWindow.selectedBuilding == null)
            {
                // і натискаєм на побудований будинок
                if (hit.collider != null && hit.transform.tag == "Building")
                {
                    var building = hit.transform.GetComponent<Building>();
                    map.buildingUpdatingWindow.SetSelectedBuilding(building);
                    Point point = CoordinateConvertor.IsoToSimple(hit.point);
                    map.buildingUpdatingWindow.SetPosition(CoordinateConvertor.SimpleToIso(point));
                }
            }
        }
    }
}
