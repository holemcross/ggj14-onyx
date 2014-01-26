using UnityEngine;
using System.Collections;

public class TestCameraFollowScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Network.sendRate = 120f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Network.isClient) return;
        transform.position = new Vector3(Mathf.Sin(Time.fixedTime / 10f * Mathf.PI)*20f, transform.position.y, transform.position.z);   //10s cycle
	}

}
