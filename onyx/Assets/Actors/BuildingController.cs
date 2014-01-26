using UnityEngine;
using System.Collections;
using SimpleJSON;

public class BuildingController : MonoBehaviour {
	
	public GameObject[] buildingObjList;
	
	private PhotonView pview;
	private float syncTimer = 0f;
    private float syncCycle = 0.1f;
	
	// Use this for initialization
	void Start () {
		
		//PhotonNetwork.offlineMode = true;
		pview = gameObject.GetComponent<PhotonView>();
		
		buildingObjList = GameObject.FindGameObjectsWithTag("Building");
		Debug.Log("BuildingCOntroller" + buildingObjList.Length);
		for(int i=0;i<buildingObjList.Length;i++) {
			buildingObjList[i].AddComponent("Building");
			buildingObjList[i].GetComponent<Building>().rootTransform = buildingObjList[i].transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!PhotonNetwork.isMasterClient) return;	// reserved for server
		
		// send position to client
		syncTimer += Time.fixedDeltaTime;
        if (syncTimer >= syncCycle)
        {
            syncTimer -= syncCycle;
            DoSyncBuildingData();
        }
		
		
	}
	
	void DoSyncBuildingData() {
		JSONClass cl = new JSONClass();
		JSONArray arr = new JSONArray();
		int i=0;
		for(i=0;i<buildingObjList.Length;i++) {
			Building b = buildingObjList[i].GetComponent<Building>();
			JSONClass cls = new JSONClass();
			cls.Add("health",new JSONData(b.health));
			cls.Add("state",new JSONData((int)b.buildingState));
			cls.Add("damage",new JSONData((int)b.buildingDamage));
			cls.Add("level",new JSONData(b.buildingLevel));
			cls.Add("ownership",new JSONData(b.ownership));
			arr.Add(cls);
		}
		cl.Add("bldsdata",arr);
		string data = cl.SaveToCompressedBase64();
		
		pview.RPC("BuildingController_UpdateState",PhotonTargets.Others,data);
	}
	
	[RPC]
	void BuildingController_UpdateState(string data) {
		JSONClass cl = (JSONClass) JSONNode.LoadFromCompressedBase64(data);
		int i=0;
		for(i=0;i<buildingObjList.Length;i++) {
			Building b = buildingObjList[i].GetComponent<Building>();
			b.health = cl["bldsdata"][i]["health"].AsFloat;
			b.buildingState = (Building.BuildingState) cl["bldsdata"][i]["state"].AsInt;
			b.buildingDamage = (Building.BuildingDamage) cl["bldsdata"][i]["damage"].AsInt;
			b.buildingLevel = cl["bldsdata"][i]["level"].AsInt;
			b.ownership = cl["bldsdata"][i]["ownership"].AsInt;
			b.UpdateAppearence();
		}
	}
}
