using UnityEngine;
using System.Collections;
using SimpleJSON;

public class GameResources : MonoBehaviour {

    public int p1_mana;
    public int p2_mana;

    public int pop_all;
    public int pop_p1;
    public int pop_p2;

    private float syncTimer = 0f;
    private float syncCycle = 0.066f;
	
	private float resForAll = 0f;
	
	private PhotonView pview;
	//private NetworkView nview;
	
	// Use this for initialization
	void Start () {
		
		//nview = gameObject.GetComponent<NetworkView>();
		//nview.viewID = Network.AllocateViewID();
		pview = gameObject.GetComponent<PhotonView>();
		
		if (PhotonNetwork.isMasterClient)
        {
            DoSync();
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(PhotonNetwork.isMasterClient || PhotonNetwork.room==null) {
			resForAll += Time.fixedDeltaTime;
			if(resForAll>5f) {
				resForAll -= 5f;
				p1_mana += Random.Range(1,10+1);
				p2_mana += Random.Range(1,10+1);
			}
		}
		
        if (PhotonNetwork.isMasterClient)
        {
            	
			// we are server, broadcast update every 1/30 s
            syncTimer += Time.fixedDeltaTime;
            if (syncTimer >= syncCycle)
            {
                syncTimer -= syncCycle;
                DoSync();
            }
        }
	}

    void DoSync()
    {
		
		JSONClass cl = new JSONClass();
		
		JSONData dt_p1_mana = new JSONData(p1_mana);
		JSONData dt_p2_mana = new JSONData(p2_mana);
		JSONData dt_pop_all = new JSONData(pop_all);
		JSONData dt_pop_p1 = new JSONData(pop_p1);
		JSONData dt_pop_p2 = new JSONData(pop_p2);
		cl.Add("p1_mana",dt_p1_mana);
		cl.Add("p2_mana",dt_p2_mana);
		cl.Add("pop_all",dt_pop_all);
		cl.Add("pop_p1",dt_pop_p1);
		cl.Add("pop_p2",dt_pop_p2);
		
		string data = cl.SaveToBase64();
		
		pview.RPC("GameResources_DoSyncGet",PhotonTargets.Others,data);
        //nview.RPC("GameResources_DoSyncGet", RPCMode.Others, data);
		Debug.Log ("sending data to client");
    }

    [RPC]
    void GameResources_DoSyncGet(string data)
    {
		Debug.Log ("recieved data from server");
		JSONClass cl = (JSONClass) JSONNode.LoadFromBase64(data);
		p1_mana = cl["p1_mana"].AsInt;
		p2_mana = cl["p2_mana"].AsInt;
		pop_all = cl["pop_all"].AsInt;
		pop_p1 = cl["pop_p1"].AsInt;
		pop_p2 = cl["pop_p2"].AsInt;
    }

}
