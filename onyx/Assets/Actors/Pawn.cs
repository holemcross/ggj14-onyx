using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour {
	
	public const float BEHAVIOR_TIMER_TICK = 0.01f;
	public const float BEHAVIOR_TIMER_DEFAULT = 1;
	
	public Transform rootTransform;
	
	public enum PawnState
	{
		idle,
		march,
		attack,
		flee
	}
	private float speed = 120.0f;
	public int ownership = 0;
	private float health = 1.0f;
	public PawnState pawnState = PawnState.idle;
	private bool bApproachMarker = false;
	private float behaviorTimer = 0.0f;
	public Vector3 idolPos = Vector3.zero; 
	
	private Vector3 lastPos;
	private Vector3 curPos;
	private float lerpTime;
	private float lastTime;
	private float diffTime;
	
	public bool bKilled = false;

	// Use this for initialization
	void Start () {
		// DEBUG
		wayPoint = new Vector3(-10000.0f,0.0f,0.0f);
		//pawnState = PawnState.march;
	}
	
	public void setPawn( Vector3 newPos, int newOwner)
	{
		this.rootTransform.position = newPos;
		idolPos = newPos;
		ownership = newOwner;
		pawnState = PawnState.idle;
		lastPos = this.transform.position;
		lastTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(!PhotonNetwork.isMasterClient) {
			// walk based on position lerping
			lerpTime += Time.fixedDeltaTime;
			float lerper = lerpTime/diffTime;
			if(lerper>1) lerper = 1;
			transform.position = Vector3.Lerp(lastPos,curPos,lerper);
			//transform.position += velocity * Time.fixedDeltaTime;
		}
	}
	
	public void RelayedPosition(float newx) {
		Vector3 newPos = new Vector3(newx,transform.position.y,transform.position.z);
		lastPos = transform.position;
		curPos = newPos;
		lerpTime = 0;
		diffTime = Time.fixedTime - lastTime;
		lastTime = Time.fixedTime;
	}
	
	Vector3 _wayPoint;
	public Vector3 wayPoint
	{
		get{return _wayPoint;}
		set{_wayPoint = value;}
	}
	
	public void IdleMovement()
	{
		// Going round and round baby.
		//rootTransform.Translate( new Vector3( idolPos.x + Mathf.Cos(Time.time) * Time.deltaTime ,0.0f,0.0f) );
	}
	
	public void MoveTowardsWayPoint()
	{
		
		// Check for collisions
		
		// Check for Sub Goals
		
		// Move towards Goal
		// Get Dir
		
		Vector3 wpt = new Vector3(wayPoint.x,0,0);
		Vector3 org = new Vector3(rootTransform.position.x,0,0);
		Vector3 dir = ( wpt - org);
		dir.Normalize();
		
		//rootTransform.position = (rootTransform.position + dir * speed) * Time.deltaTime;
		rootTransform.Translate(dir * speed * Time.deltaTime);
	}
	
	public void FleeMovement()
	{
		// Hack - Assume fleeing to own's side
		Vector3 dir;
		if (this.ownership == 1)
		{
			dir = new Vector3(-1,0,0) * speed * Time.deltaTime;
		}
		else
		{
			dir = new Vector3(1,0,0) * speed * Time.deltaTime;
		}
		rootTransform.Translate(dir);
	}
	
	public void AttackMovement()
	{
		// Hack - Assume attacking towards enemy's side
		Vector3 dir;
		if (this.ownership == 1)
		{
			dir = new Vector3(1,0,0) * speed * Time.deltaTime;
		}
		else
		{
			dir = new Vector3(-1,0,0) * speed * Time.deltaTime;
		}
		rootTransform.Translate(dir);
	}
	
	public void TakeDamage( float dmgValue )
	{
		if( health > 0.0f)
		{
			health -= dmgValue;
			
			// Change State
			if( health <= 0.0f)
			{
				KillPawn();
			}
		}
	}
	
	public void KillPawn()
	{
		// Clear Stats
		//ownership = -1;
		// Hide
		bKilled = true;
		// Report Death
		
		// Spawn Corpse
		
		// Remove from game
	}
}
