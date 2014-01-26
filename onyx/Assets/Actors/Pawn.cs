﻿using UnityEngine;
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
	private float speed = 2.0f;
	public int ownership = 0;
	private float health = 1.0f;
	public PawnState pawnState = PawnState.idle;
	private bool bApproachMarker = false;
	private float behaviorTimer = 0.0f;
	public Vector3 idolPos = Vector3.zero; 

	// Use this for initialization
	void Start () {
		// DEBUG
		//wayPoint = new Vector3(100.0f,0.0f,0.0f);
		//pawnState = PawnState.march;
	}
	
	public void setPawn( Vector3 newPos, int newOwner)
	{
		this.rootTransform.position = newPos;
		idolPos = newPos;
		ownership = newOwner;
	}
	
	// Update is called once per frame
	void Update () {

		// Movement
		//Vector3 curPos = rootTransform.position;
		//Vector3 dir = new Vector3(1,0,0) * speed; // Move towards the right
		//rootTransform.position = (dir - curPos) * Time.deltaTime;
		//rootTransform.Translate( dir * Time.deltaTime );
		
		// Do Movement
		//DoMovement();
		
		
	}
	

	
	Vector3 _wayPoint;
	public Vector3 wayPoint
	{
		get{return _wayPoint;}
		set{_wayPoint = value;}
	}
	
	void DoMovement()
	{
		
		// Save old Pos
		Vector3 prevPos = this.rootTransform.position;
		
		switch(pawnState)
		{
		case PawnState.idle:
			IdleMovement();
			break;
		case PawnState.march:
			MoveTowardsWayPoint();
			break;
		case PawnState.flee:
			FleeMovement();
			break;
		case PawnState.attack:
			AttackMovement();
			break;	
		}
		
		// Check collision and push back
		
	}
	
	
	
	public void IdleMovement()
	{
		
	}
	
	public void MoveTowardsWayPoint()
	{
		
		// Check for collisions
		
		// Check for Sub Goals
		
		// Move towards Goal
		// Get Dir
		
		Vector3 dir = (wayPoint - rootTransform.position);
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
	
	void TakeDamage( float dmgValue )
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
	
	void KillPawn()
	{
		// Clear Stats
		ownership = -1;
		// Hide
		
		// Report Death
		
		// Spawn Corpse
		
		// Remove from game
	}
}
