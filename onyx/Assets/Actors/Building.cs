using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
	
	public enum BuildingState
	{
		init,
		inCapture,
		captured
	}
	
	public enum BuildingDamage
	{
		none,
		stage1,
		stage2,
		stage3,
		destroyed
	}
	
	public Transform rootTransform;
	private BuildingState buildingState = BuildingState.init;
	private BuildingDamage buildingDamage = BuildingDamage.none;
	private float health = 1.0f;
	private int ownership = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void TakeDamage( float dmgValue )
	{
		if( health > 0.0f)
		{
			health -= dmgValue;
			
			// Change State
			if( health <= 0.0f)
			{
				buildingDamage = BuildingDamage.destroyed;
			}
			else if (health <= 0.25f)
			{
				buildingDamage = BuildingDamage.stage3;
			}
			else if (health <= 0.50f)
			{
				buildingDamage = BuildingDamage.stage2;
			}
			else if (health <= 0.75f)
			{
				buildingDamage = BuildingDamage.stage1;
			}
		}
	}
	
	
	void Capping()
	{
		// TODO: How to implement this feature?
	}
	
	void UpdateAppearence()
	{
		// TODO: Texture Swap logic
	}
}
