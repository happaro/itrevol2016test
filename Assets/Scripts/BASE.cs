using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BASE : MonoBehaviour
{
	public static BASE Instance;

	public BuildProperty[] properties = new BuildProperty[Enum.GetValues(typeof(BuildingType)).Length];
	public List<int[,]> mapPatterns = new List<int[,]>();

	public void Awake()
	{
		Instance = this;
	}

	public int GetBuildPrice(BuildingType type, int level)
	{
		return properties[(int)type].prices[level];
	}

	public int GetResourcePrice(BuildingType type)
	{
		return properties[(int)type].resourcePrice;
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
		return properties[(int)type].resourceSprite;
	}

	public string GetDescription(BuildingType type)
	{
		return properties[(int)type].description;
	}
    public bool IsNeedMainResource(BuildingType type)
    {
        return properties[(int)type].mainResource;
    }
    public BuildingType GetInputType1(BuildingType type)
    {
        return properties[(int)type].inputType1;
    }
    public BuildingType GetInputType2(BuildingType type)
    {
        return properties[(int)type].inputType2;
    }
}

[Serializable]
public class BuildProperty
{
	public int[] prices = new int[3];
	public int resourcePrice;
	public float startSpeed;
	public string buildName;
	public string resourceName;
	public string description;
	public Sprite sprite;
	public Sprite resourceSprite;
	public bool mainResource = true;
	public BuildingType inputType1, inputType2;

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
			_base.properties [i].buildName = EditorGUILayout.TextField(_base.properties [i].buildName);

			_base.properties [i].description = EditorGUILayout.TextArea(_base.properties [i].description);
			_base.properties [i].prices[0] = EditorGUILayout.IntSlider ("Level 1 :", _base.properties [i].prices[0], 0, 10000);
			_base.properties [i].prices[1] = EditorGUILayout.IntSlider ("Level 2 :", _base.properties [i].prices[1], 0, 10000);
			_base.properties [i].prices[2] = EditorGUILayout.IntSlider ("Level 3 :", _base.properties [i].prices[2], 0, 10000);
			GUILayout.Label ("Resource price:");
			_base.properties [i].resourcePrice = EditorGUILayout.IntSlider ("Resource price:", _base.properties [i].resourcePrice, 0, 300);
			_base.properties [i].startSpeed = EditorGUILayout.Slider ("Start speed (sec):", _base.properties [i].startSpeed, 0, 10);
			_base.properties [i].mainResource = EditorGUILayout.Toggle ("Main resource", _base.properties [i].mainResource);
			if (!_base.properties [i].mainResource) 
			{
				_base.properties [i].inputType1 = (BuildingType)EditorGUILayout.EnumPopup(_base.properties [i].inputType1);
				_base.properties [i].inputType2 = (BuildingType)EditorGUILayout.EnumPopup(_base.properties [i].inputType2);
			}


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