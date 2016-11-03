using UnityEngine;
using System.Collections;

public enum BuildingType {HandBuilding = 0, FootBuilding, HandFooter}

public class Building : MonoBehaviour 
{
	public int buildLevel = 1;
	public bool isBuilded = false;
	public BuildingType buildingType;

	public Button collectButton;

	public int taskCount = 0;
	public int speed = 10;
	public int doneTasks = 0;

	public SpriteRenderer doneIcon;
	public GameObject resourceObject;
	public TextMesh doneCountText;
	protected float timer;

	protected MainController main;

	public BuildingType inputResourceType1, inputResourceType2;
	public bool inputMeat = true;


	public void Start()
	{
		main = GameObject.FindObjectOfType<MainController> ();
		collectButton.myAction = () => {
			CollectResources ();
			UpdateGetter();
		};
	}

	public virtual void InitializeBuilding(BuildingType type)
	{
		buildingType = type;
		var props = BASE.Instance.properties [(int)buildingType];
		this.GetComponent<SpriteRenderer> ().sprite = props.sprite;
		doneIcon.sprite = props.resourceSprite;
	}

	public void Update()
	{
		if (isBuilded)
			DoTasks ();	
	}

	public virtual void DoTasks()
	{
		if (taskCount == 0)
			return;
		timer += Time.deltaTime;
		if (timer > speed) 
		{
			taskCount--;
			doneTasks++;
			timer = 0;
		}
		UpdateGetter ();
	}

	public virtual void CollectResources()
	{
		main.inventory.productsCounts [(int)buildingType] += doneTasks;
		doneTasks = 0;
	}

	public void UpdateGetter()
	{
		resourceObject.SetActive (doneTasks > 0);
		doneCountText.text = doneTasks > 0 ? doneTasks.ToString () : "";
	}

	public void AddTasks(int count)
	{
		taskCount += count;
	}
}
