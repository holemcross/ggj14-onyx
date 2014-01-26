using UnityEngine;
using System.Collections;
using SimpleJSON;

public class PlayerInput : MonoBehaviour {
	
	public Camera mainCamera;
	public PawnController pawnController;
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
	}
	
	[RPC]
	void PlayerInput_RemoteHandleScreenClick(string data) {
		JSONClass cl = (JSONClass) JSONNode.LoadFromCompressedBase64(data);
		float nx = cl["pointx"].AsFloat;
		HandleScreenClickMerge(nx,ownership);
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
			string data = cl.SaveToCompressedBase64();
			pview.RPC("GameResources_DoSyncGet",PhotonTargets.Others,data);
		}
	}
	
	void HandleScreenClickMerge(float pointx, int owner) {
		pawnController.SetWayPoint(new Vector3(pointx,0,0), owner);
	}
	
	void setOwner( int ownerValue)
	{
		ownership = ownerValue;
	}
	
}
