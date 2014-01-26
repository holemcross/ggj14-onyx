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
	
	public BuildingState buildingState = BuildingState.init;
 	public BuildingDamage buildingDamage = BuildingDamage.none;
	public int buildingLevel = 0;
	public float health = 1.0f;
	public int ownership = 0;
	private const float ICON_HEIGHT = 250f;
	public bool bCursorNear = false;
	
	private int baseSprId;
	
	// Use this for initialization
	void Start () {
		
		//Debug.Log (transform.GetComponent<tk2dSprite>().spriteId);
		//transform.GetComponent<tk2dSprite>().spriteId = 2;
		
		baseSprId = transform.GetComponent<tk2dSprite>().spriteId;
		
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
		UpdateAppearence();
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
		UpdateAppearence();
	}
	
	void Capping()
	{
		// TODO: How to implement this feature?
	}
	
	public void UpdateAppearence()
	{
		// TODO: Texture Swap logic
		//if(health<=0) transform.GetComponent<tk2dSprite>().color = Color.black;
		transform.GetComponent<tk2dSprite>().spriteId = spriteMap(baseSprId,ownership,buildingLevel,buildingDamage);
	}
	
	private int spriteMap(int baseId, int owner, int lvl,BuildingDamage damged) {
		switch(baseId) {
			case 2:
				if(damged==BuildingDamage.destroyed) return 11;
				else return 2;
				break;
			case 15:
				if(damged==BuildingDamage.destroyed) return 11;
				else return 15;
				break;
			case 16:
				if(damged==BuildingDamage.destroyed) return 7;
				else return 16;
				break;
			case 1:
				if(owner==0) return 1;
				else if(owner==1) {
					if(lvl==1) return 20;
					if(lvl==2) return 17;
					if(lvl==3) return 9;
				} else if(owner==2) {
					if(lvl==1) return 19;
					if(lvl==2) return 18;
					if(lvl==3) return 0;
				}
				break;
		}
		return 0;
	}
	
}
