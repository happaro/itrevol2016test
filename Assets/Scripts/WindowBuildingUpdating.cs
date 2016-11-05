using UnityEngine;
using System.Collections;

public class WindowBuildingUpdating : Window
{
    public Button updateButton;
    public Button deleteButton;
    public Button productsButton;
    public Building selectedBuilding;

    public TextMesh nameText, descriptionText;


    public void SetSelectedBuilding(Building building)
    {
        if (building != null)
        {
            selectedBuilding = building;
            selectedBuilding.GetComponent<SpriteRenderer>().color = new Color(0.6f, 1, 1);
            if (selectedBuilding is HumanInputer)
            {
                nameText.text = string.Format("{0}\n(Уровень {1})", "Аэропорт нечести", building.buildLevel);
                descriptionText.text = "Поставка \nгрешников";
                deleteButton.gameObject.SetActive(false);
                productsButton.gameObject.SetActive(false);
                base.Open();
            }
            else
            {
                nameText.text = string.Format("{0}\n(Уровень {1})", BASE.Instance.GetBuildName(building.buildingType), building.buildLevel);
                descriptionText.text = BASE.Instance.GetDescription(building.buildingType);
                deleteButton.gameObject.SetActive(true);
                productsButton.gameObject.SetActive(true);
            }
            updateButton.SetActive(CanUpdate());
            WindowManager.Instance.GetWindow<GUI>().Close();
            base.Open();
        }
        else
        {
            if (selectedBuilding != null)
                selectedBuilding.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            selectedBuilding = null;
            WindowManager.Instance.GetWindow<GUI>().Open();
            base.Close();
        }
    }

    public bool CanUpdate()
    {
        if (selectedBuilding.buildLevel > 2 || SaveManager.coinsCount < BASE.Instance.GetBuildPrice(selectedBuilding.buildingType, selectedBuilding.buildLevel))
            return false;
        return true;
    }

    public void SetPosition(Vector3 position)
    {
        //transform.position = new Vector3(position.x, position.y, position.z - 0.1f);
    }

    void Start()
    {
        productsButton.myAction = () =>
            {
                WindowManager.Instance.GetWindow<WindowProductCreation>().Open(selectedBuilding);
                WindowManager.Instance.GetWindow<GUI>().Close(false);
                WindowManager.Instance.GetWindow<WindowBuildingUpdating>().SetSelectedBuilding(null);
            };
        base.Close();

        updateButton.myAction = () =>
        {
            string textAnswer = string.Format("Вы уверены, что \nхотите улучшить \n\"{0}\"\nза {1}$?", BASE.Instance.GetBuildName(selectedBuilding.buildingType), BASE.Instance.GetBuildPrice(selectedBuilding.buildingType, selectedBuilding.buildLevel));
            WindowManager.Instance.GetWindow<WindowDialog>().Open("Улучшение", textAnswer, () =>
                {
                    WindowManager.Instance.GetWindow<WindowDialog>().Close(true);
                    SaveManager.coinsCount -= BASE.Instance.GetBuildPrice(selectedBuilding.buildingType, selectedBuilding.buildLevel);
                    selectedBuilding.buildLevel++;
                    selectedBuilding.speed /= 2;
                    SetSelectedBuilding(null);
                });
            base.Close();
        };

        deleteButton.myAction = () =>
        {
            int cashBack = 0;
            for (int i = 0; i < selectedBuilding.buildLevel; i++)
                cashBack += BASE.Instance.GetBuildPrice(selectedBuilding.buildingType, i);
            cashBack /= 2;

            string textAnswer = string.Format("Вы уверены, что \nхотите удалить \n\"{0}\"\nи получить\n{1}$?",
                BASE.Instance.GetBuildName(selectedBuilding.buildingType),
                cashBack);
            WindowManager.Instance.GetWindow<WindowDialog>().Open("Удаление", textAnswer, () =>
                {
                    WindowManager.Instance.GetWindow<WindowDialog>().Close(true);
                    SaveManager.coinsCount += cashBack;
                    GameObject.FindObjectOfType<MapController>().buildings.Remove(selectedBuilding);
                    Destroy(selectedBuilding.gameObject);
                    SetSelectedBuilding(null);
                });
            base.Close();


        };
    }
}