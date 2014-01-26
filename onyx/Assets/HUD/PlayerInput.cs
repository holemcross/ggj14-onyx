using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	
	public Camera mainCamera;
	public PawnController pawnController;
	public int ownership = 0;
	
	// Use this for initialization
	void Start () {
		// DEBUG
		setOwner(1); // TEMP FOR NOW
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			HandleScreenClick(mainCamera);
		}
	}
	
	void HandleScreenClick( Camera cam )
	{
		Debug.Log("In HandleScreenClick Update");
		float dist = 200.0f;
		// Get Point Click
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast (ray, out hit, dist))
		{
			Debug.DrawLine(ray.origin, hit.point);
		}
		
		Debug.Log(hit.point);
		pawnController.SetWayPoint(new Vector3(hit.point.x,0,0), ownership);
	}
	
	void setOwner( int ownerValue)
	{
		ownership = ownerValue;
	}
	
}
