using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public enum BuildingType { Hand = 0, Foot, Eye, HandFoot, EyeFoot }

public class Building : MonoBehaviour 
{
	public int buildLevel = 1;
	public bool isBuilded = false;
	public BuildingType buildingType;

	public Button collectButton;

	public int taskCount = 0;
	public float speed = 10;
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
		speed = props.startSpeed;
	}

	public void Update()
	{
		if (isBuilded)
			DoTasks ();	
	}

	public virtual void DoTasks()
	{
        if (taskCount == 0)
        {
            resourceObject.gameObject.SetActive(false);
            return;
        }
		timer += Time.deltaTime;
        doneCountText.text = taskCount.ToString();        
        float xScale = timer / speed * 1.4f;
        doneIcon.transform.localScale = new Vector3(xScale, doneIcon.transform.localScale.y, doneIcon.transform.localScale.z);
		if (timer > speed) 
		{
			taskCount--;            
			//doneTasks++;
			SendProduct();
			timer = 0;
		}
		//UpdateGetter ();
	}


	void SendProduct()
	{
		ProductInputer.ins.InputProduct (buildingType);
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
        resourceObject.gameObject.SetActive(true);
	}
}
