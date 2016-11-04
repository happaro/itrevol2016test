using UnityEngine;
using System.Collections;

public class WindowProductCreation : Window
{
    public Button okButton, cancelButton, plusButton, minusButton;
    public TextMesh countText;
    public TextMesh inputTypecount1Text;
    public TextMesh inputTypecount2Text;
    public SpriteRenderer resultSprite;
    public SpriteRenderer resourseSprite1;
    public SpriteRenderer resourseSprite2;
    private bool isMainResource = false;
    public Building building;
    private BuildingType inputType1, inputType2;
    public int count = 1;
    public int inputTypecount1 = 1;
    public int inputTypecount2 = 1;
    public void Start()
    {
        cancelButton.myAction = () =>
        {
            base.Close(true);
        };
        okButton.myAction = () =>
        {
            if (isMainResource)
            {
                GameObject.FindObjectOfType<MainController>().inventory.mainProductCount -= count;
                GameObject.FindObjectOfType<MainController>().inventory.productsCounts[(int)building.buildingType] += count;
            }
            else
            {
                GameObject.FindObjectOfType<MainController>().inventory.productsCounts[(int)inputType1] -= inputTypecount1;
                GameObject.FindObjectOfType<MainController>().inventory.productsCounts[(int)inputType2] -= inputTypecount2;
                GameObject.FindObjectOfType<MainController>().inventory.productsCounts[(int)building.buildingType] += count;
            }
            base.Close(true);
        };
        plusButton.myAction = () =>
        {
            if (isMainResource)
            {
                if(inputTypecount1 < GameObject.FindObjectOfType<MainController>().inventory.mainProductCount)
                {
                    count++;
                }
            }
            else
            {
                if (inputTypecount1 < GameObject.FindObjectOfType<MainController>().inventory.productsCounts[(int)inputType1] &&
                    inputTypecount2 < GameObject.FindObjectOfType<MainController>().inventory.productsCounts[(int)inputType2])
                {
                    count++;
                }                    
            }            
            inputTypecount1 = count;
            inputTypecount2 = count;
            UpdateText();
        };
        minusButton.myAction = () =>
        {
            if (count > 1)
            {
                count--;
                inputTypecount1 = count;
                inputTypecount2 = count;
                UpdateText();
            }
        };
    }

    void UpdateText()
    {
        countText.text = count.ToString();
        inputTypecount1Text.text = inputTypecount1.ToString();
        inputTypecount2Text.text = inputTypecount2.ToString();
    }

    public void Open(Building b)
    {
        building = b;
        inputTypecount1 = inputTypecount2 = count = 1;
        UpdateText();
        resultSprite.sprite = BASE.Instance.GetBuildingResource(building.buildingType);
        isMainResource = BASE.Instance.IsNeedMainResource(building.buildingType);
        if (isMainResource)
        {
            resourseSprite2.transform.parent.transform.parent.gameObject.SetActive(false);
            if(inputTypecount1 > GameObject.FindObjectOfType<MainController>().inventory.mainProductCount)
            {
                okButton.gameObject.SetActive(false);
            }
            else
            { 
                okButton.gameObject.SetActive(true); 
            }
        }
        else
        {
            resourseSprite2.transform.parent.transform.parent.gameObject.SetActive(true);
            inputType1 = BASE.Instance.GetInputType1(building.buildingType);
            inputType2 = BASE.Instance.GetInputType2(building.buildingType);
            resourseSprite1.sprite = BASE.Instance.GetBuildingResource(inputType1);
            resourseSprite2.sprite = BASE.Instance.GetBuildingResource(inputType2);
            if (inputTypecount1 > GameObject.FindObjectOfType<MainController>().inventory.productsCounts[(int)inputType1] ||
                    inputTypecount2 > GameObject.FindObjectOfType<MainController>().inventory.productsCounts[(int)inputType2])
            {
                okButton.gameObject.SetActive(false);
            }
            else
            {
                okButton.gameObject.SetActive(true);
            }
        }
        

        base.Open(false);

    }
}
