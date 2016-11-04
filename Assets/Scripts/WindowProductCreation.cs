using UnityEngine;
using System.Collections;

public class WindowProductCreation : Window
{
    public Button okButton, cancelButton, plusButton, minusButton;
    public TextMesh countText;
    public SpriteRenderer resultSprite;
    public SpriteRenderer resourseSprite1;
    public SpriteRenderer resourseSprite2;
    public int count = 1;

    public void Start()
    {
        cancelButton.myAction = () =>
        {
            base.Close(true);
        };
        okButton.myAction = () =>
        {
            base.Close(true);
        };
        plusButton.myAction = () =>
        {
            count++;
            countText.text = count.ToString();
        };
        minusButton.myAction = () =>
        {
            if (count > 1)
            {
                count--;
                countText.text = count.ToString();
            }
        };
    }

    public void Open(Building building)
    {
        count = 1;
        countText.text = count.ToString();
        resultSprite.sprite = BASE.Instance.GetBuildingResource(building.buildingType);
        if (BASE.Instance.IsNeedMainResource(building.buildingType))
        {
            resourseSprite2.transform.parent.transform.parent.gameObject.SetActive(false);
        }
        base.Open(false);

    }
}
