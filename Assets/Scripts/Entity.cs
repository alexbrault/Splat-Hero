using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
	
	Spritesheet spritesheet;
	
	// Use this for initialization
	protected void Start () {
		spritesheet = new Spritesheet(gameObject);
		spritesheet.Load("Sprites/ironman2");
		spritesheet.CreateAnimation("Patrick", 300);
		spritesheet.AddFrame("Patrick", 68, 96, 54, 96);
		spritesheet.AddFrame("Patrick", 122, 96, 54, 96);
		spritesheet.AddFrame("Patrick", 177, 96, 54, 96);
		spritesheet.SetCurrentAnimation("Patrick");
	}
	
	// Update is called once per frame
	protected void Update () {
		Move();
		spritesheet.Render();
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
