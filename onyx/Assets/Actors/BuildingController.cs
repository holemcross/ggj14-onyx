using UnityEngine;
using System.Collections;

public class BuildingController : MonoBehaviour {
	
	public GameObject[] buildingObjList;
	
	// Use this for initialization
	void Start () {
		buildingObjList = GameObject.FindGameObjectsWithTag("Building");
		Debug.Log("BuildingCOntroller" + buildingObjList.Length);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
