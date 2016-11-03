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

    void Start()
    {
        okButton.myAction = () =>
        {
            //TODO: BuildngCreation
            GameObject.FindObjectOfType<MapController>().FinishBuilding();
            SetSelectedBuilding(null);
        };
        cancelButton.myAction = () =>
        {
            SetSelectedBuilding(null);
        };
    }
}