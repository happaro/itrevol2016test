using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

public class ProductInputer : MonoBehaviour 
{
	public static ProductInputer ins;

	public GameObject[] products = new GameObject[10];
	public GameObject productPrefab;
	public Transform invButton;

	float step = 1.1f;

	public void Awake()
	{
		products = new GameObject[10];
	}

	public void InputProduct(BuildingType type, bool isMain = false)
	{
		var newProd = Instantiate (productPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		if (!isMain) 
			MainController.ins.inventory.productsCounts[(int)type]++;
		else MainController.ins.inventory.mainProductCount++;

		int place = 1;
		for (int i = 0; i < products.Length; i++) 
		{
			if (products [i] != null && products [i].GetComponent<SpriteRenderer> ().sprite != BASE.Instance.GetBuildingResource (type))
				return;
			if (products [i] == null) 
			{
				place = i + 1;
				products [i] = newProd;
				break;
			}
		}
		newProd.transform.position = new Vector3 (invButton.position.x, invButton.position.y + (place * step), 0.5f);

		if (!isMain) 
		{
			newProd.transform.GetChild (0).GetComponent<TextMesh> ().text = "" + (MainController.ins.inventory.productsCounts[(int)type] - 1);
			newProd.GetComponent<SpriteRenderer> ().sprite = BASE.Instance.GetBuildingResource (type);
		}
		else newProd.transform.GetChild (0).GetComponent<TextMesh> ().text = "" + (MainController.ins.inventory.mainProductCount - 1);
		newProd.transform.localScale = Vector3.zero;
		newProd.transform.parent = WindowManager.Instance.GetWindow<GUI> ().transform;

		StartCoroutine (Input (newProd, type, isMain));
	}

	IEnumerator Input(GameObject prod, BuildingType type, bool isMain = false)
	{
		HOTween.To (prod.transform, 0.5f, "localScale", Vector3.one * 0.7f);
		yield return new WaitForSeconds (0.5f);
		prod.transform.GetChild (0).GetComponent<TextMesh> ().text = isMain ? (MainController.ins.inventory.mainProductCount).ToString() : (MainController.ins.inventory.productsCounts[(int)type]).ToString();
		WindowManager.Instance.GetWindow<WindowProductCreation> ().UpdateButtonState ();
		yield return new WaitForSeconds (0.8f);
		HOTween.To (prod.transform, 0.3f, "position", prod.transform.position - new Vector3(3, 0, 0));
		yield return new WaitForSeconds (0.3f);
		Destroy (prod);
	}

	void Start () 
	{
		ProductInputer.ins = this;
	}
}
