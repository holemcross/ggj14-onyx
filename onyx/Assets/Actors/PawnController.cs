using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PawnController : MonoBehaviour {
	
	public List<GameObject> pawnObjList;
	

	// Use this for initialization
	void Start () {
		// Debug\
		Debug.Log("PawnController Init");
		// Gen 5 Pawns
		pawnObjList = new List<GameObject>();
		
		for(int i=0; i<5;i++)
		{
			GameObject myPawn = (GameObject)Instantiate(Resources.Load("NPC_Pawn", typeof(GameObject)));
			
			if(i==3)// Debug
			{
				myPawn.GetComponent<Pawn>().setPawn(new Vector3(1 *i,0,0), 1);
			}
			else
			{
				myPawn.GetComponent<Pawn>().setPawn(new Vector3(1 *i,0,0), 2);
			}
			pawnObjList.Add(myPawn);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		foreach(GameObject pawn in pawnObjList)
		{
			UpdatePawnBehavior( pawn );
		}
		
	
	}
	
	void UpdatePawnBehavior( GameObject targetPawn )
	{
		Pawn pawn = targetPawn.GetComponent<Pawn>();
		
		// Coward
		if( CalcCoward( targetPawn ) )
		{
			Debug.Log("CalcCoward Triggered");
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
	
	// Calculate Pawn Behavior
	// Calc Coward
	bool CalcCoward( GameObject targetPawn)
	{
		float threatRange = 15.0f;
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
				Debug.Log("TempPos: " + tempPos.ToString());
				Debug.Log("TempOwner: " + tempOwner);
				if(tempOwner != 0)
				{
					Debug.Log("Magnitude: " +(origin - tempPos).magnitude);
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
		float threatRange = 15.0f;
		
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
		return false;
	}
	
}
