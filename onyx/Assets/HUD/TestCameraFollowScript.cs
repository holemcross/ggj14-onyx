using UnityEngine;
using System.Collections;

public class TestCameraFollowScript : Photon.MonoBehaviour {
	
	private float lastTime = 0f;
	private Vector3 lastPos = Vector3.zero;
	private Vector3 velocity = Vector3.zero;
	
	// Use this for initialization
	void Start () {
		Network.sendRate = 120f;
		lastPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(!PhotonNetwork.isMasterClient) {
			transform.position += velocity * Time.fixedDeltaTime;
		} else {
			transform.position = new Vector3(Mathf.Sin(Time.fixedTime / 10f * Mathf.PI)*20f, transform.position.y, transform.position.z);   //10s cycle
		}

	}
	
	public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		//Debug.Log ("syncing");
		// Send data to server
		if (stream.isWriting)
		{
			stream.SendNext(transform.position);
			//Debug.Log ("writing");
		}
		else
		{
			Vector3 pos = (Vector3) stream.ReceiveNext();
			lastPos = transform.position;
			float deltaTime = Time.fixedTime - lastTime;
			lastTime = Time.fixedTime;
			transform.position = pos;
			velocity = (pos - lastPos) / deltaTime;
			Debug.Log (velocity);
		}
	}

}
