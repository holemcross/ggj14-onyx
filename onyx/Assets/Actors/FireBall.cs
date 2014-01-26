using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {
	
	public Transform rootTransform;
	public BuildingController buildingController;
	public PawnController pawnController;
	
	private float explosionRadius = 150f;
	public bool bExploded = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		DoMovement();
	}
	
	void DoMovement()
	{
		rootTransform.Translate( Vector3.down * 0.9f);
		
		if( rootTransform.position.y < - 270f && !bExploded )
		{
			Vector3 missilePos = rootTransform.position;
			// Explode
			foreach( GameObject building in buildingController.buildingObjList )
			{
				Vector3 bPos = building.GetComponent<Building>().rootTransform.position;
				if ((bPos - missilePos).magnitude < explosionRadius)
				{
					building.GetComponent<Building>().TakeDamage(1000f);
				}
			}
			
			foreach( GameObject pawn in pawnController.pawnObjList )
			{
				Vector3 pPos = pawn.GetComponent<Pawn>().rootTransform.position;
				if ((pPos - missilePos).magnitude < explosionRadius)
				{
					pawn.GetComponent<Pawn>().TakeDamage(1000f);
				}
			}
			// Delete Self
			bExploded = true;
		}
	}
}
