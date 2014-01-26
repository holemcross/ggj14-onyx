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
			
			myPawn.GetComponent<Pawn>().setPawn(new Vector3(3 *i,0,0), 1);
			pawnObjList.Add(myPawn);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		foreach(GameObject pawn in pawnObjList)
		{
			UpdatePawnBehavior( pawn );
			DoMovement( pawn );
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
			Debug.Log("CalcWayPoint Triggered");
			pawn.pawnState = Pawn.PawnState.march;
		}
		else
		{
			Debug.Log("CalcIdle Triggered");
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
			if( tPawn != pawn)
			{
				Pawn tp = tPawn.GetComponent<Pawn>();
				if( (tp.rootTransform.position - p.rootTransform.position).magnitude < 10.0f) // TODO: Remove hardcoded value
				{
					if(p.collider.bounds.Intersects( tp.collider.bounds))
					{
						// Collision
						
						// Move back
						if( p.pawnState != Pawn.PawnState.idle)
						{
							Vector3 dir = (p.rootTransform.position - tp.rootTransform.position).normalized;
							p.rootTransform.Translate( dir * 2.0f * Time.deltaTime );
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
		float range = 20.0f;
		
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
