using UnityEngine;
using System.Collections;

public class GoblinBall : Entity {
	
	float wanderTheta = 0.0f;
	
	public float wanderRadius = 16.0f;
	public float wanderDistance = 60.0f;
	public float change = 0.25f;
	public float maxspeed = 20;
	
	// Use this for initialization
	void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		Wander();
		base.Update();
	}
	
	void Wander()
	{
		wanderTheta += Random.Range(-change, change);
		
		Vector3 circleLocation = gameObject.rigidbody.velocity;
		circleLocation.Normalize();
		circleLocation *= wanderDistance;
		circleLocation += gameObject.transform.position;
		
		Vector3 circleOffset = new Vector3(wanderRadius * Mathf.Cos(wanderTheta), 0, wanderRadius * Mathf.Sin(wanderTheta));
		Vector3 target = circleLocation + circleOffset;
		
		gameObject.rigidbody.AddForce(Steer(target));
	}
	
	Vector3 Steer(Vector3 target)
	{
		Vector3 steer;
		Vector3 desired = target - gameObject.transform.position;
		
		float distance = desired.magnitude;
		
		if(distance > 0)
		{
			desired.Normalize();
			desired *= maxspeed;
			
			steer = desired - gameObject.rigidbody.velocity;
			
			if(steer.magnitude > maxspeed)
			{
				steer.Normalize();
				steer *= maxspeed;
			}
		}
		
		else
			steer = new Vector3(0,0,0);
		
		return steer;
	}
}
