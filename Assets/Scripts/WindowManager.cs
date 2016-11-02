using UnityEngine;
using System.Collections;

public class WindowManager : MonoBehaviour 
{
	public static WindowManager Instance;

	void Start () 
	{
		Instance = this;
	}

	public T GetWindow<T>()
	{
		for (int i = 0; i < transform.childCount; i++) 
			if (transform.GetChild (i).GetComponent<T>() != null) 
				return transform.GetChild (i).GetComponent<T> ();
		return transform.GetChild (0).GetComponent<T>();
	}
}
