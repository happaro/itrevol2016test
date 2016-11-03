using UnityEngine;
using System.Collections;

public class WindowBuildingUpdating : Window
{
    public Button updateButton;
    public Button deleteButton;
    public Building selectedBuilding;

	public TextMesh nameText, descriptionText;


    public void SetSelectedBuilding(Building building)
    {
        selectedBuilding = building;
        if (selectedBuilding != null)
        {
            updateButton.gameObject.SetActive(CanUpdate());

			nameText.text = string.Format("{0}\n(Уровень {1})", BASE.Instance.GetBuildName (building.buildingType), building.buildLevel);
			descriptionText.text = BASE.Instance.GetDescription (building.buildingType);
            base.Open();
        }
        else
        {
            base.Close();
        }
    }
    
    public bool CanUpdate()
    {
		if (selectedBuilding.buildLevel > 2 || SaveManager.coinsCount < BASE.Instance.GetBuildPrice (selectedBuilding.buildingType, selectedBuilding.buildLevel))
			return false;
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
			SaveManager.coinsCount -= BASE.Instance.GetBuildPrice (selectedBuilding.buildingType, selectedBuilding.buildLevel);
			selectedBuilding.buildLevel++;
            SetSelectedBuilding(null);
        };

        deleteButton.myAction = () =>
        {
			GameObject.FindObjectOfType<MapController>().buildings.Remove(selectedBuilding);
			Destroy(selectedBuilding.gameObject);
            SetSelectedBuilding(null);
        };
    }
}