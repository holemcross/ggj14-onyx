using UnityEngine;
using System.Collections;

public class BuildingController : MonoBehaviour {
	
	public GameObject[] buildingObjList;
	
	// Use this for initialization
	void Start () {
		buildingObjList = GameObject.FindGameObjectsWithTag("Building");
		Debug.Log("BuildingCOntroller" + buildingObjList.Length);
		for(int i=0;i<buildingObjList.Length;i++) {
			buildingObjList[i].AddComponent("Building");
			buildingObjList[i].GetComponent<Building>().rootTransform = buildingObjList[i].transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
