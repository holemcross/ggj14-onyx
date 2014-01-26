using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireBallController : MonoBehaviour {
	
	public List<GameObject> fireBallObjList; 
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for(int i=0; i< fireBallObjList.FindLastIndex+1;i++)
		{
			if(fireBallObjList[i].GetComponent<FireBall>().bExploded)
			{
				fireBall = null; // Delete
			}
		}
	}
	
	public void genFireball( Vector3 position)
	{
		GameObject fireBall = (GameObject)Instantiate(Resources.Load("Weeble_Neutral", typeof(GameObject)));
		fireBallObjList.Add(fireball);
	}
}
