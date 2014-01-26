using UnityEngine;
using System.Collections;
using SimpleJSON;

public class PlayerInput : MonoBehaviour {
	
	public Camera mainCamera;
	public PawnController pawnController;
	public FireBallController fireBallController;
	public int ownership = 0;
	
	private PhotonView pview;
	
	// Use this for initialization
	void Start () {
		// DEBUG
		if(PhotonNetwork.isMasterClient) setOwner(1);
		else setOwner(2);
		
		pview = gameObject.GetComponent<PhotonView>();
		
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			HandleScreenClick(mainCamera);
		}
		else if (Input.GetMouseButtonDown(1))
		{
			HandleScreenClickRight(mainCamera);
		}
	}
	
	void HandleScreenClick( Camera cam )
	{
		Debug.Log("In HandleScreenClick Update");
		float dist = 5000.0f;
		// Get Point Click
		
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast (ray, out hit, dist))
		{
			Debug.DrawLine(ray.origin, hit.point);
		}
		
		Debug.Log(hit.point);
		
		if(PhotonNetwork.isMasterClient) HandleScreenClickMerge(hit.point.x,ownership);
		else {
			JSONClass cl = new JSONClass();
			JSONData px = new JSONData(hit.point.x);
			cl.Add("pointx",px);
			cl.Add ("owner",new JSONData(ownership));
			string data = cl.SaveToCompressedBase64();
			pview.RPC("PlayerInput_RemoteHandleScreenClick",PhotonTargets.Others,data);
		}
	}
	
	[RPC]
	void PlayerInput_RemoteHandleScreenClick(string data) {
		JSONClass cl = (JSONClass) JSONNode.LoadFromCompressedBase64(data);
		float nx = cl["pointx"].AsFloat;
		HandleScreenClickMerge(nx,cl["owner"].AsInt);
	}
	
	void HandleScreenClickMerge(float pointx, int owner) {
		pawnController.SetWayPoint(new Vector3(pointx,0,0), owner);
	}
	
	void HandleScreenClickRight( Camera cam )
	{
		Debug.Log("In HandleScreenClick Update");
		float dist = 5000.0f;
		// Get Point Click
		
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast (ray, out hit, dist))
		{
			Debug.DrawLine(ray.origin, hit.point);
		}
		
		Debug.Log(hit.point);
		
		if(PhotonNetwork.isMasterClient) HandleScreenClickRightMerge(hit.point.x);
		else {
			JSONClass cl = new JSONClass();
			JSONData px = new JSONData(hit.point.x);
			cl.Add("pointx",px);
			string data = cl.SaveToCompressedBase64();
			pview.RPC("PlayerInput_RemoteHandleScreenClickRight",PhotonTargets.Others,data);
		}
	}
	
	[RPC]
	void PlayerInput_RemoteHandleScreenClickRight(string data) {
		JSONClass cl = (JSONClass) JSONNode.LoadFromCompressedBase64(data);
		float nx = cl["pointx"].AsFloat;
		HandleScreenClickRightMerge(nx);
	}
	
	void HandleScreenClickRightMerge(float pointx) {
		fireBallController.genFireball( new Vector3(pointx-300, 100, -30));
	}
	
	void setOwner( int ownerValue)
	{
		ownership = ownerValue;
	}
	
}
