﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class PawnController : MonoBehaviour {
	
	public List<GameObject> pawnObjList;
	
	private PhotonView pview;
	private float syncTimer = 0f;
    private float syncCycle = 0.03f;

	// Use this for initialization
	void Start () {
		
		pview = gameObject.GetComponent<PhotonView>();
		
		if(!PhotonNetwork.isMasterClient) return;	// reserved for server
		
		// Debug\
		Debug.Log("PawnController Init");
		GameObject myPawn;
		
		pawnObjList = new List<GameObject>();
		
		/* myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(-4790.491f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(-4695.656f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(-4487.628f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(-4215.352f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(-3651.837f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(-1868.871f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(-1709.827f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(-1544.65f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(-1403.988f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(-836.361f , -400f,-30.0f +  -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(-532.7227f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(-199.298f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(84.58902f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(520.3267f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(708.3323f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(1005.448f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(1229.839f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(1500.483f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(1682.016f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(1500.483f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(1919.638f , -400f,-30.0f +  -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(2411.318f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(3097.799f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(3355.161f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(3823.741f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(4292.422f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		
		myPawn = PhotonNetwork.Instantiate("Weeble_Neutral",Vector3.zero,Quaternion.identity,0);
		myPawn.GetComponent<Pawn>().setPawn(new Vector3(4566.352f , -400f,-30.0f + -10.0f), 0);
		pawnObjList.Add(myPawn);
		*/
		
		for(int i=0;i<30;i++) {
			GameObject pawn = PhotonNetwork.Instantiate("Weeble_Neutral",new Vector3(Random.Range(-1000f,1000f),-400f,-40f),Quaternion.identity,0);
			pawnObjList.Add(pawn);
		}
		
		pview.RPC("PawnController_MakePawnList",PhotonTargets.Others);
		
	}
	
	[RPC]
	void PawnController_MakePawnList() {
		pawnObjList = new List<GameObject>();
		Pawn[] pobjs = GameObject.FindObjectsOfType(typeof(Pawn)) as Pawn[];
		for(int i=0;i<pobjs.Length;i++) {
			Pawn p = pobjs[i];
			pawnObjList.Add(p.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!PhotonNetwork.isMasterClient) return;	// reserved for server
		
		foreach(GameObject pawn in pawnObjList)
		{
			if(pawn==null) continue;
			if(pawn.GetComponent<Pawn>().bKilled)
			{
				//pawnObjList.Remove(pawn);
				PhotonNetwork.Destroy(pawn);
			}
			continue;
			
			UpdatePawnBehavior( pawn );
			DoMovement( pawn );
			if(pawn.GetComponent<Pawn>().bKilled)
			{
				//pawnObjList.Remove(pawn);
				PhotonNetwork.Destroy(pawn);
			}
		}
		
		// send position to client
		syncTimer += Time.fixedDeltaTime;
        if (syncTimer >= syncCycle)
        {
            syncTimer -= syncCycle;
            DoSyncPawnPosition();
        }
		
		
	}
	
	void DoSyncPawnPosition() {
		JSONClass cl = new JSONClass();
		JSONArray arr = new JSONArray();
		foreach(GameObject tPawn in pawnObjList )
		{
			JSONClass cls = new JSONClass();
			if(tPawn!=null) {
				cls.Add("pointx",new JSONData(tPawn.transform.position.x));
				cls.Add ("bkilled",new JSONData(tPawn.GetComponent<Pawn>().bKilled));
			}
			arr.Add(cls);
		}
		cl.Add("pawnsdata",arr);
		string data = cl.SaveToCompressedBase64();
		
		pview.RPC("PawnController_UpdatePosition",PhotonTargets.Others,data);
	}
	
	[RPC]
	void PawnController_UpdatePosition(string data) {
		JSONClass cl = (JSONClass) JSONNode.LoadFromCompressedBase64(data);
		int i=0;
		foreach(GameObject tPawn in pawnObjList )
		{
			if(tPawn==null) continue;
			float px = cl["pawnsdata"][i]["pointx"].AsFloat;
			tPawn.GetComponent<Pawn>().RelayedPosition(px);
			tPawn.GetComponent<Pawn>().bKilled = cl["pawnsdata"][i]["bkilled"].AsBool;
			i++;
		}
	}
	
	void UpdatePawnBehavior( GameObject targetPawn )
	{
		
		return;
		
		Pawn pawn = targetPawn.GetComponent<Pawn>();
		
		// Coward
		if( CalcCoward( targetPawn ) )
		{
			//Debug.Log("CalcCoward Triggered");
			pawn.pawnState = Pawn.PawnState.flee;
		}
		else if(CalcBrave( targetPawn ))
		{
			//Debug.Log("CalcBrave Triggered");
			pawn.pawnState = Pawn.PawnState.attack;
		}
		// if goal set
		else if( CalcWayPoint( targetPawn ) )
		{
			//Debug.Log("CalcWayPoint Triggered");
			pawn.pawnState = Pawn.PawnState.march;
		}
		else
		{
			//Debug.Log("CalcIdle Triggered");
			pawn.pawnState = Pawn.PawnState.idle;
		}
	}
	
	void DoMovement( GameObject pawn )
	{
		Pawn p = pawn.GetComponent<Pawn>();
		
		// Save old Pos
		Vector3 prevPos = p.rootTransform.position;
		
		
		// Check collision and push back
		foreach(GameObject tPawn in pawnObjList )
		{
			if(tPawn==null) return;
			if( tPawn != pawn)
			{
				Pawn tp = tPawn.GetComponent<Pawn>();
				if( (tp.rootTransform.position - p.rootTransform.position).magnitude < 50.0f) // TODO: Remove hardcoded value
				{
					if(p.collider.bounds.Intersects( tp.collider.bounds))
					{
						// Collision
						
						// Move back
						if( p.pawnState != Pawn.PawnState.idle)
						{
							Vector3 pPos = new Vector3(p.rootTransform.position.x,0,0);
							Vector3 tpPos = new Vector3(tp.rootTransform.position.x,0,0);
							Vector3 dir = (pPos - tpPos).normalized;
							p.rootTransform.Translate( dir * 150.0f * Time.deltaTime );
						}
					}
				}
				
			}
		}
		
		switch(p.pawnState)
		{
		case Pawn.PawnState.idle:
			p.IdleMovement();
			break;
		case Pawn.PawnState.march:
			p.MoveTowardsWayPoint();
			break;
		case Pawn.PawnState.flee:
			p.FleeMovement();
			break;
		case Pawn.PawnState.attack:
			p.AttackMovement();
			break;	
		}
		
		
	}
	
	// Calculate Pawn Behavior
	// Calc Coward
	bool CalcCoward( GameObject targetPawn)
	{
		float threatRange = 300.0f;
		int threshold = 3;
		// Check for enemy pawns nearby
		Vector3 origin = targetPawn.GetComponent<Pawn>().rootTransform.position;
		int targetOwner = targetPawn.GetComponent<Pawn>().ownership;
		
		int enemyCount = 0;
		int friendCount = 0;
		foreach(GameObject pawn in pawnObjList )
		{
			if(targetPawn != pawn)
			{
				Vector3 tempPos = pawn.GetComponent<Pawn>().rootTransform.position;
				int tempOwner = pawn.GetComponent<Pawn>().ownership;
				//Debug.Log("TempPos: " + tempPos.ToString());
				//Debug.Log("TempOwner: " + tempOwner);
				if(tempOwner != 0)
				{
					//Debug.Log("Magnitude: " +(origin - tempPos).magnitude);
					if( (origin - tempPos).magnitude < threatRange )
					{
						
						if(targetOwner == tempOwner)
						{
							// Friendly
							friendCount++;
						}
						else if(targetOwner != tempOwner)
						{
							// Enemy
							enemyCount++;
						}
					}
				}
			}
		}
		
		if( enemyCount > friendCount + threshold )
		{
			return true;
		}
		return false;
	}
	
	// Calc Brave
	bool CalcBrave( GameObject targetPawn )
	{
		int threshold = 4;
		float threatRange = 300.0f;
		
		Vector3 origin = targetPawn.GetComponent<Pawn>().rootTransform.position;
		int targetOwner = targetPawn.GetComponent<Pawn>().ownership;
		
		int enemyCount = 0;
		int friendCount = 0;
		foreach(GameObject pawn in pawnObjList )
		{
			if(targetPawn != pawn)
			{
				Vector3 tempPos = pawn.GetComponent<Pawn>().rootTransform.position;
				int tempOwner = pawn.GetComponent<Pawn>().ownership;
				
				if(tempOwner != 0)
				{
					if( (origin - tempPos).magnitude < threatRange )
					{
						if(targetOwner == tempOwner)
						{
							// Friendly
							friendCount++;
						}
						else if(targetOwner != tempOwner)
						{
							// Enemy
							enemyCount++;
						}
					}
				}
			}
		}
		
		if( friendCount > enemyCount + threshold )
		{
			return true;
		}
			
		return false;
	}
	
	// Calc WayPoint
	bool CalcWayPoint( GameObject targetPawn )
	{
		float range = 800.0f;
		
		foreach(GameObject pawn in pawnObjList )
		{
			if(targetPawn != pawn)
			{
				Vector3 tempPos = pawn.GetComponent<Pawn>().rootTransform.position;
				Vector3 tempWaypt = pawn.GetComponent<Pawn>().wayPoint;
				
				if( (tempWaypt - tempPos).magnitude < range )
				{
					return true;
				}
			}
		}
			
		return false;
	}
	
	public void SetWayPoint( Vector3 newPos, int ownership )
	{
		foreach(GameObject pawn in pawnObjList)
		{
			Pawn p = pawn.GetComponent<Pawn>();
			
			if(p.ownership == ownership)
			{
				p.wayPoint = newPos;
			}
		}
	}
}
