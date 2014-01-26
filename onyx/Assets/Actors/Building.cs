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
	GameObject icon;
	public tk2dSprite buildingSprite;
	public tk2dSprite iconSprite;
	
	private BuildingState buildingState = BuildingState.init;
	private BuildingDamage buildingDamage = BuildingDamage.none;
	private int buildingLevel = 0;
	private float health = 1.0f;
	private int ownership = 0;
	private const float ICON_HEIGHT = 250f;
	public bool bCursorNear = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		// Check if Upgrade State
		if( bCursorNear )
		{
			showIcon();
		}
		else
		{
			
			icon = null;
		}
	}
	
	public void showIcon()
	{
		// Get Right Icon
		if (icon == null)
		{
			icon = (GameObject)Instantiate(Resources.Load("UpgradeSprite", typeof(GameObject)));
			icon.transform.position = new Vector3(rootTransform.position.x,rootTransform.position.y + ICON_HEIGHT,rootTransform.position.z);
		}
	}
	
	public void TakeDamage( float dmgValue )
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
	
	public void Upgrade( int newOwnership)
	{
		// TODO: Deplete Resources
		//Debug.Log("In UPGRADE!");
		ownership = newOwnership;
		buildingLevel++;
		if( ownership == 1)
		{
			switch(buildingLevel)
			{
			case 1:
				break;
			case 2:
				break;
			default:
				break;
			}
		}
		else if( ownership == 2)
		{
			switch(buildingLevel)
			{
			case 1:
				break;
			case 2:
				break;
			default:
				break;
			}
		}
	}
	
	void Capping()
	{
		// TODO: How to implement this feature?
	}
	
	public void UpdateAppearence()
	{
		// TODO: Texture Swap logic
	}
}
