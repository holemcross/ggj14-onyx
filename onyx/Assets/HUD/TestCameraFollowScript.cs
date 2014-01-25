using UnityEngine;
using System.Collections;

public class TestCameraFollowScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Mathf.Sin(Time.fixedTime / 10f * Mathf.PI)*20f, transform.position.y, transform.position.z);   //10s cycle
	}

}
