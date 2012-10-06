using UnityEngine;
using System.Collections;

public class Stunned : Status {

	// Use this for initialization
	void Start () {
		statusTime = 2;
	}
	
	public override void StartStatusEffect()
	{
		attachedPlayer.CanMove = false;
	}
	
	public override void ProcessStatusEffect()
	{
		attachedPlayer.gameObject.rigidbody.velocity = new Vector3(0,0,0);
	}
	
	public override void EndStatusEffect()
	{
		attachedPlayer.CanMove = true;
		Destroy(this);
	}
}
