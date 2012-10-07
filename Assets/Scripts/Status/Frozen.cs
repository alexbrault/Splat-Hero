using UnityEngine;
using System.Collections;

public class Frozen : Status {

	// Use this for initialization
	void Start () {
		if(gameObject.CompareTag("Ball"))
			statusTime = 10;
		
		else
			statusTime = 4;
	}
	
	public override void StartStatusEffect()
	{
		attachedEntity.CanMove = false;
	}
	
	public override void ProcessStatusEffect()
	{
		attachedEntity.gameObject.rigidbody.velocity = new Vector3(0,0,0);
	}
	
	public override void EndStatusEffect()
	{
		attachedEntity.CanMove = true;
		Destroy(this);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(gameObject.CompareTag("Ball"))
		{
			foreach (ContactPoint contact in collision.contacts) {
				if(contact.otherCollider.gameObject.CompareTag("Player"))
				{
					Status status = contact.otherCollider.gameObject.AddComponent<Frozen>();
					status.SetEntity(contact.otherCollider.gameObject.GetComponent<Player>());
				}
	        }
		}
	}
}
