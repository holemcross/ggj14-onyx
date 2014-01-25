using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour {

	public Transform rootTransform;

	private float speed = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Movement
		Vector3 curPos = rootTransform.position;
		Vector3 dir = new Vector3(1,0,0) * speed; // Move towards the right
		//rootTransform.position = (dir - curPos) * Time.deltaTime;
		rootTransform.Translate( dir * Time.deltaTime );
	}
}
