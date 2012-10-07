using UnityEngine;
using System.Collections;

public class Stunned : Status {
	
	// Use this for initialization
	void Start () {
		statusTime = 2;
	}
	
	public override void StartStatusEffect()
	{
		if(attachedEntity is Player)
			attachedEntity.CanMove = false;
	}
	
	public override void ProcessStatusEffect()
	{
		if(attachedEntity is Player)
			attachedEntity.gameObject.rigidbody.velocity = new Vector3(0,0,0);
	}
	
	public override void EndStatusEffect()
	{
		if(attachedEntity is Player)
			attachedEntity.CanMove = true;
		
		Destroy(this);
	}
}
