using UnityEngine;
using System.Collections;

public class GameResources : MonoBehaviour {

    public int p1_mana;
    public int p2_mana;

    public int pop_all;
    public int pop_p1;
    public int pop_p2;

    private float syncTimer = 0f;
    private float syncCycle = 0.066f;
	
	private NetworkView nview;
	
	// Use this for initialization
	void Start () {
        if (Network.isServer || Network.isClient)
        {
            DoSync();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Network.isServer)
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
        string data = "";
        networkView.RPC("GameResources_DoSyncGet", RPCMode.Others, nview.viewID, data);
    }

    [RPC]
    void GameResources_DoSyncGet(string data)
    {

    }

}
