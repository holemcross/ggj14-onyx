using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {
	
	public NetworkInitiator ni;
	
	public enum BType {
		StartStopServer,
		ConnectToIp,
		QuickConnect
	};
	
	public BType type;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick() {
		if(type==BType.StartStopServer) ni.StartStopServer();
		else if(type==BType.ConnectToIp) {
			// read from ip box and put it into joinserver	
			UIInput inp = GameObject.Find("IPTextbox").GetComponent<UIInput>();
			ni.joinServer(inp.value);
		} else if(type==BType.QuickConnect) {
			ni.QuickConnect();	
		}
	}
	
}
