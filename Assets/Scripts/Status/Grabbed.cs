using UnityEngine;
using System.Collections;

public class Grabbed : Status {

	// Use this for initialization
	void Start () {
		statusTime = 5.0f;
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
			gameObject.transform.Translate(0, 0, -11);
		}
		
		else if(gameObject.CompareTag("Player"))
		{
		}
	}
	
	public override void EndStatusEffect()
	{
		Vector3 position = attachedEntity.rigidbody.velocity;
		position.Normalize();
		
		gameObject.transform.position = attachedEntity.transform.position + (position * 15);
		
		attachedEntity.CanMove = true;
		gameObject.collider.enabled = true;
		Destroy(this);
	}
}
