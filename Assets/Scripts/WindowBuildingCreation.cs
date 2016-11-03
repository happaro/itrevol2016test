using UnityEngine;
using System.Collections;

public class WindowBuildingCreation : Window
{
    public Button okButton;
    public Button cancelButton;
    public Building selectedBuilding;

    public void SetSelectedBuilding(Building building)
    {
        selectedBuilding = building;
        if (selectedBuilding != null)
        {
            base.Open();
        }
        else
        {
            base.Close();
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x,position.y,position.z-0.1f);
        okButton.gameObject.SetActive(CanBuild());

		if(CanBuild())
        {
            selectedBuilding.GetComponent<SpriteRenderer>().color = new Color(150f / 255f, 255f / 255f, 100f / 255f);            
        }
        else
        {
            selectedBuilding.GetComponent<SpriteRenderer>().color = new Color(255f / 255f, 165f / 255f, 165f / 255f);
        }
    }

    public bool CanBuild()
    {
        foreach (var building in GameObject.FindObjectOfType<MapController>().buildings)
        {
            if(building.transform.position.x == transform.position.x && building.transform.position.y == transform.position.y)
            {
                return false;
            }
        }
        return true;
    }

    void Start()
    {
        okButton.myAction = () =>
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);
            selectedBuilding.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
            GameObject.FindObjectOfType<MapController>().FinishBuilding();
            SetSelectedBuilding(null);
			WindowManager.Instance.GetWindow<GUI>().Open();
        };

        cancelButton.myAction = () =>
        {
            GameObject.FindObjectOfType<MapController>().CancelBuilding();
            SetSelectedBuilding(null);
			WindowManager.Instance.GetWindow<GUI>().Open();
        };
    }
}