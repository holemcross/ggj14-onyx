﻿using UnityEngine;
using System.Collections;

using SimpleJSON;

public class NetworkInitiator : MonoBehaviour {
	
	public UILabel StartStopServerLabel;
	public UILabel ConnectWithIPLabel;
	public UILabel StatusLabel;
	public UILabel QuickConnectLabel;
	
	enum MState {
		IDLE,
		SEARCHING,
		LISTENING,
		CONNECTING,
		STARTING,
	};
	private int DEFAULT_PORT = 5552;
	
	private MState state;
	
	LANBroadcastService lanbs;
	
	private string quickip;
	private int pcount = 0;
	
	// Use this for initialization
	void Start () {
		
		PhotonNetwork.ConnectUsingSettings("1.0");
		Debug.Log ((PhotonNetwork.connectionStateDetailed.ToString()));
		
		//lanbs = transform.GetComponent<LANBroadcastService>();
		state = MState.IDLE;
		quickip = "";
		
		
		// test json
		/* JSONData a1 = new JSONData("haha");
		JSONData a2 = new JSONData(2);
		JSONClass cl = new JSONClass();
		cl.Add("a1",a1);
		cl.Add ("a2",a2);
		string jsonstr = cl.SaveToBase64();
		Debug.Log (jsonstr);
		Debug.Log (JSONNode.LoadFromBase64(jsonstr)); */
		
	}
	
	// Update is called once per frame
	void Update () {
		if(state==MState.IDLE) {
			state = MState.SEARCHING;
			//lanbs.StartSearchBroadCasting(OnFoundServer,OnNotFoundServer);
		}
	}
	
	void OnFoundServer(string serverip) {
		//joinServer(serverip);
		quickip = serverip;
		QuickConnectLabel.text = "connect to "+serverip;
	}
	
	void OnNotFoundServer() {
		state = MState.IDLE;
	}
	
	public void joinServer(string ip) {
		state = MState.CONNECTING;
		StatusLabel.text = "CONNECTING TO "+ip;
		
		PhotonNetwork.JoinRoom(ip);
		
		/*
		// Validate IP
		try
		{
			NetworkConnectionError error = Network.Connect( ip,DEFAULT_PORT);
			if( error != NetworkConnectionError.NoError)
			{
				StatusLabel.text = "Error connecting to Server";
				state = MState.IDLE;
			}
			else
			{
				// nothing goes wrong so far, waiting
			}
		}
		catch(System.Exception e)// NetworkConnectionError
		{
			StatusLabel.text = "Failed to Connect to Server";
			state = MState.IDLE;
		}*/
		
	}
			
	
	public void StartStopServer() {
		
		if(state==MState.LISTENING) {
			//lanbs.StopBroadCasting();
			PhotonNetwork.LeaveRoom();
			StatusLabel.text = "Server stopped";
			StartStopServerLabel.text = "Start Server";
			state = MState.IDLE;
		} else {
			string svname = "server"+Random.Range(10000,100000);
			PhotonNetwork.CreateRoom(svname,false,true,2);
			pcount = 0;
			//Network.InitializeServer(2,DEFAULT_PORT,true);
			//lanbs.StartAnnounceBroadCasting();
			StatusLabel.text = "Waiting for connection with name "+svname;
			StartStopServerLabel.text = "Stop Server";
			state = MState.LISTENING;
		}
		
	}
	
	void OnPhotonPlayerConnected(PhotonPlayer otherPlayer) {
		Debug.Log ("client join");
		if(PhotonNetwork.isMasterClient) {
			StatusLabel.text = "Client connected";
			state = MState.STARTING;
			Application.LoadLevel("Scene_TestCameraHUD");
		}
	}
	
	void OnJoinedRoom() {
		Debug.Log ("someone join");
		if(!PhotonNetwork.isMasterClient) {
			Debug.Log ("it's the client");
			// we are the client
			StatusLabel.text = "Connected";
			state = MState.STARTING;
			Application.LoadLevel("Scene_TestCameraHUD");
		}
	}
	
	void OnPhotonJoinRoomFailed() {
		StatusLabel.text = "Error connecting to Server";
		state = MState.IDLE;
	}
	
	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player connected from " + player.ipAddress + ":" + player.port);
		StatusLabel.text = "Client connected";
		state = MState.STARTING;
		Application.LoadLevel("Scene_TestCameraHUD");
	}
	
	void OnConnectedToServer() {
		// Connected
		StatusLabel.text = "Connected";
		state = MState.STARTING;
		Application.LoadLevel("Scene_TestCameraHUD");
	}
	
	void OnFailedToConnect(NetworkConnectionError error) {
		StatusLabel.text = "Error connecting to Server";
		state = MState.IDLE;
	}
	
	public void QuickConnect() {
		if(quickip!="") {
			joinServer(quickip);
		}
	}
	
}
