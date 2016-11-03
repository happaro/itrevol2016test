using UnityEngine;
using System.Collections;

public class WindowBuildingUpdating : Window
{
    public Button updateButton;
    public Button deleteButton;
    public Building selectedBuilding;

    public void SetSelectedBuilding(Building building)
    {
        selectedBuilding = building;
        if (selectedBuilding != null)
        {
            updateButton.gameObject.SetActive(CanUpdate());
            base.Open();
        }
        else
        {
            base.Close();
        }
    }
    
    public bool CanUpdate()
    {
        // якщо нехватає бабла -> false
        return true;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x, position.y, position.z - 0.1f);
    }

    void Start()
    {
        updateButton.myAction = () =>
        {
            //TODO: Update
            SetSelectedBuilding(null);
        };

        deleteButton.myAction = () =>
        {
            //TODO: Delete
            SetSelectedBuilding(null);
        };
    }
}