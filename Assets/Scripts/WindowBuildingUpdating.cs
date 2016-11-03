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
			string textAnswer = string.Format("Вы уверены, что \nхотите улучшить \n\"{0}\"\nза {1}$?", BASE.Instance.GetBuildName(selectedBuilding.buildingType), BASE.Instance.GetBuildPrice (selectedBuilding.buildingType, selectedBuilding.buildLevel));
			WindowManager.Instance.GetWindow<WindowDialog>().Open("Улучшение", textAnswer, () => 
				{
					WindowManager.Instance.GetWindow<WindowDialog>().Close();
					SaveManager.coinsCount -= BASE.Instance.GetBuildPrice (selectedBuilding.buildingType, selectedBuilding.buildLevel);
					selectedBuilding.buildLevel++;
					SetSelectedBuilding(null);
				});
        };

        deleteButton.myAction = () =>
        {
			int cashBack = 0;
			for (int i = 0; i < selectedBuilding.buildLevel; i++)
				cashBack += BASE.Instance.GetBuildPrice (selectedBuilding.buildingType, i);
			cashBack /= 2;
					
			string textAnswer = string.Format("Вы уверены, что \nхотите удалить \n\"{0}\"\nи получить\n{1}$?", 
				BASE.Instance.GetBuildName(selectedBuilding.buildingType), 
				cashBack);
			WindowManager.Instance.GetWindow<WindowDialog>().Open("Удаление", textAnswer, () => 
				{
					WindowManager.Instance.GetWindow<WindowDialog>().Close();
					SaveManager.coinsCount += cashBack;
					GameObject.FindObjectOfType<MapController>().buildings.Remove(selectedBuilding);
					Destroy(selectedBuilding.gameObject);
					SetSelectedBuilding(null);
				});
			

        };
    }
}