using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

public class ProductInputer : MonoBehaviour 
{
	public static ProductInputer ins;

	public List<GameObject> products;
	public GameObject productPrefab;
	public Transform invButton;

	float step = 1.5f;

	public void InputProduct(BuildingType type, bool isMain = false)
	{
		var newProd = Instantiate (productPrefab, new Vector3(invButton.position.x, invButton.position.y + (products.Count + 1 * step), 0.5f), Quaternion.identity) as GameObject;
		if (!isMain) 
		{
			newProd.transform.GetChild (0).GetComponent<TextMesh> ().text = "" + (MainController.ins.inventory.productsCounts[(int)type]);
			newProd.GetComponent<SpriteRenderer> ().sprite = BASE.Instance.GetBuildingResource (type);
		}
		else newProd.transform.GetChild (0).GetComponent<TextMesh> ().text = "" + (MainController.ins.inventory.mainProductCount);
		newProd.transform.localScale = Vector3.zero;
		newProd.transform.parent = WindowManager.Instance.GetWindow<GUI> ().transform;
		products.Add (newProd);
		StartCoroutine (Input (newProd, type, isMain));
	}

	IEnumerator Input(GameObject prod, BuildingType type, bool isMain = false)
	{
		HOTween.To (prod.transform, 0.5f, "localScale", Vector3.one * 0.7f);
		yield return new WaitForSeconds (0.5f);
		prod.transform.GetChild (0).GetComponent<TextMesh> ().text = isMain ? (++MainController.ins.inventory.mainProductCount).ToString() : (++MainController.ins.inventory.productsCounts[(int)type]).ToString();
		yield return new WaitForSeconds (0.4f);
		HOTween.To (prod.transform, 0.5f, "position", prod.transform.position - new Vector3(3, 0, 0));
		products.Remove (prod);
		yield return new WaitForSeconds (1f);
		Destroy (prod);
	}

	void Start () 
	{
		ProductInputer.ins = this;
	}
}
