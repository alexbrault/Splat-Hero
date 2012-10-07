using UnityEngine;
using System.Collections;

public class Grabbed : Status {

	// Use this for initialization
	void Start () {
		infinite = true;
	}
	
	public override void StartStatusEffect()
	{
		attachedEntity.CanMove = false;
	}
	
	public override void ProcessStatusEffect()
	{
		if(gameObject.CompareTag("Ball"))
		{
			gameObject.rigidbody.velocity = new Vector3(0,0,0);
			gameObject.transform.position = attachedEntity.transform.position;
			gameObject.transform.Translate(0, 0, -15);
		}
		
		else if(gameObject.CompareTag("Player"))
		{
			gameObject.GetComponent<Player>().CanMove = false;
			
			Vector3 dashDirection = new Vector3(0, 0, 1);
			dashDirection.Normalize();
			gameObject.rigidbody.AddForce( dashDirection * 50 );
		}
	}
	
	public override void EndStatusEffect()
	{
		if(gameObject.CompareTag("Ball"))
		{
			Vector3 position = attachedEntity.rigidbody.velocity;
			position.Normalize();
			
			gameObject.transform.position = attachedEntity.transform.position + (position * 15);
			
			attachedEntity.CanMove = true;
			gameObject.collider.enabled = true;
		}
		
		else if(gameObject.CompareTag("Player"))
		{
			gameObject.GetComponent<Player>().CanMove = true;
		}
		
		Destroy(this);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Player"))
		{
			EndStatusEffect();
		}
	}
}
