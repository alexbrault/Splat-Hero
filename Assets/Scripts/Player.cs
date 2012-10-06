using UnityEngine;
using System.Collections;

public class Player : Entity {
		
	// Use this for initialization
	void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		base.Update();
	}
	
	protected void Move()
	{
		float xMovement = -Input.GetAxisRaw("Player1_MoveX");
		float zMovement = -Input.GetAxisRaw("Player1_MoveZ");

        Vector3 actualForce = gameObject.rigidbody.velocity;

        if(xMovement == 0)
            gameObject.rigidbody.AddForce(new Vector3(-actualForce.x / 3, 0, zMovement));

        if (xMovement * actualForce.x < 0.0f)
        {
            gameObject.rigidbody.AddForce(new Vector3(-actualForce.x/2, 0, zMovement));
        }

        if (zMovement == 0)
            gameObject.rigidbody.AddForce(new Vector3(0, 0, -actualForce.z / 3));

        if (zMovement * actualForce.z < 0.0f)
        {
            gameObject.rigidbody.AddForce(new Vector3(0, 0, -actualForce.z / 2));
        }

        actualForce = gameObject.rigidbody.velocity;

        if (actualForce.x > 30.0f) xMovement = 30.0f - actualForce.x;
        if (actualForce.x < -30.0f) xMovement = -30.0f - actualForce.x;

        if (actualForce.z > 30.0f) zMovement = 30.0f - actualForce.z;
        if (actualForce.z < -30.0f) zMovement = -30.0f - actualForce.z;

        gameObject.rigidbody.AddForce(new Vector3(6*xMovement, 0, 6*zMovement));
	}
}
