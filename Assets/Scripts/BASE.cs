using UnityEngine;
using System.Collections;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BASE : MonoBehaviour
{
	public static BASE Instance;

	public BuildProperty[] properties = new BuildProperty[Enum.GetValues(typeof(BuildingType)).Length];

	public void Start()
	{
		Instance = this;
	}

	public int GetBuildPrice(BuildingType type, int level)
	{
		return properties[(int)type].prices[level];
	}

	public int GetResourcePrice(BuildingType type, int level)
	{
		return properties[(int)type].resourcePrices[level];
	}

	public string GetBuildName(BuildingType type)
	{
		return properties[(int)type].buildName;
	}

	public Sprite GetBuildingSprite(BuildingType type)
	{
		return properties[(int)type].sprite;
	}

	public Sprite GetBuildingResource(BuildingType type)
	{
		return properties[(int)type].sprite;
	}
}

[Serializable]
public class BuildProperty
{
	public int[] prices = new int[3];
	public int[] resourcePrices = new int[3];
	public string buildName;
	public string resourceName;
	public Sprite sprite;
	public Sprite resourceSprite;
}

	
#if UNITY_EDITOR
[CustomEditor(typeof(BASE))]
public class BASEEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		var _base = target as BASE;
		for (int i = 0; i < _base.properties.Length; i++) 
		{
			
			GUILayout.Space (20);
			GUILayout.Label (((BuildingType)i).ToString());
			_base.properties [i].buildName = EditorGUILayout.TextField (_base.properties [i].buildName);
			_base.properties [i].prices[0] = EditorGUILayout.IntSlider ("Level 1 :", _base.properties [i].prices[0], 0, 10000);
			_base.properties [i].prices[1] = EditorGUILayout.IntSlider ("Level 2 :", _base.properties [i].prices[1], 0, 10000);
			_base.properties [i].prices[2] = EditorGUILayout.IntSlider ("Level 3 :", _base.properties [i].prices[2], 0, 10000);

			GUILayout.BeginHorizontal ();

			GUILayout.BeginVertical();
			_base.properties [i].sprite = (Sprite)EditorGUILayout.ObjectField (_base.properties [i].sprite, typeof(Sprite), false);
			if (_base.properties [i].sprite != null)
				GUILayout.Label (_base.properties [i].sprite.texture, GUILayout.Height(100));
			GUILayout.EndVertical();

			GUILayout.BeginVertical();
			_base.properties [i].resourceSprite = (Sprite)EditorGUILayout.ObjectField (_base.properties [i].resourceSprite, typeof(Sprite), false);
			if (_base.properties [i].resourceSprite != null)
				GUILayout.Label (_base.properties [i].resourceSprite.texture, GUILayout.Height(100));
			GUILayout.EndVertical();

			GUILayout.EndHorizontal ();

		}

	}
}
#endif