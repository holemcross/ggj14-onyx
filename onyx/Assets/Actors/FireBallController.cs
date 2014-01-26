using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireBallController : MonoBehaviour {
	
	public PawnController pawnController;
	public BuildingController buildingController;
	
	public List<GameObject> fireBallObjList; 
	
	// Use this for initialization
	void Start () {
		fireBallObjList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject fireBall in fireBallObjList)
		{
			if(fireBall.GetComponent<FireBall>().bExploded)
			{
				GameObject tempFB = fireBall;
				//tempFB = null;
			}
		}
	}
	
	public void genFireball( Vector3 position)
	{
		GameObject fireBall = (GameObject)Instantiate(Resources.Load("FireBallSprite", typeof(GameObject)));
		fireBall.GetComponent<FireBall>().pawnController = pawnController;
		fireBall.GetComponent<FireBall>().buildingController = buildingController;
		fireBallObjList.Add(fireBall);
	}
}
