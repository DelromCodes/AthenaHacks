using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager: MonoBehaviour {

	public GameObject DialogCanvasGO;
	public GameObject HUDCanvasGO;
	public GameObject GameboardGO;

	//Dialog related
	public static string DRAGON_DIALOG = "DragonDialog";

	//Projectiles
	public static string FIRE_PROJECTILE = "FireProjectile";

	//Gems
	public static string SINGLE_CUT = "OldSingleCut";
	public static string PEAR_CUT = "PearCut";
	public static string ROSE_CUT = "RoseCut";
	public static string ROUND_CUT = "RoundCut";

	protected Dictionary<string, GameObject> cachedResources = new Dictionary<string, GameObject>();

	public GameObject LoadDialog(string prefabName) {
		GameObject go = LoadPrefab(prefabName);
		if (go != null) {
			go.transform.SetParent(DialogCanvasGO.transform);
			RectTransform rt = go.GetComponent<RectTransform>();
			go.transform.position = Vector3.zero;
			go.transform.localScale = Vector3.one;            
			rt.sizeDelta = Vector2.zero;
			rt.anchoredPosition = Vector2.zero;
		}
		return go;
	}

	public GameObject LoadPrefab(string prefabName) {
		if (cachedResources.ContainsKey(prefabName)) {
			return GameObject.Instantiate(cachedResources[prefabName]);
		} else {
			if (Create(prefabName)) {
				return GameObject.Instantiate(cachedResources[prefabName]);
			}
		}
		return null;
	}

	protected bool Create(string prefabName) {
		GameObject newPrefab = Resources.Load(prefabName) as GameObject;
		if (newPrefab != null) {
			cachedResources.Add(prefabName, newPrefab);
			return true;
		} else {
			Debug.LogError("Prefab with name \"" + prefabName + "\" does not exist!  Please check the spelling or make sure the prefab is inside a Resources/ folder.");
			return false;
		}
	}

}
