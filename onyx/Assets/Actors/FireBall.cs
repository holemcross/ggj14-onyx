using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {
	
	public Transform rootTransform;
	public BuildingController buildingController;
	public PawnController pawnController;
	
	private float explosionRadius = 350f;
	public bool bExploded = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!PhotonNetwork.isMasterClient) return;
		
		if(bExploded) return;
		DoMovement();
	}
	
	void DoMovement()
	{
		
		rootTransform.Translate( Vector3.down * 1.4f);
		
		if( rootTransform.position.y < - 200f && !bExploded )
		{
			Vector3 missilePos = rootTransform.position + Vector3.right*300f;
			// Explode
			foreach( GameObject building in buildingController.buildingObjList )
			{
				Vector3 bPos = building.GetComponent<Building>().rootTransform.position;
				if ((bPos - missilePos).magnitude < explosionRadius)
				{
					building.GetComponent<Building>().TakeDamage(1f);
				}
			}
			
			foreach( GameObject pawn in pawnController.pawnObjList )
			{
				if(pawn==null) continue;
				Vector3 pPos = pawn.GetComponent<Pawn>().rootTransform.position;
				if ((pPos - missilePos).magnitude < explosionRadius)
				{
					pawn.GetComponent<Pawn>().TakeDamage(1f);
				}
			}
			// Delete Self
			bExploded = true;
			
			//gameObject.SetActive(false);
			PhotonNetwork.Destroy(gameObject);
			
		}
	}
}
