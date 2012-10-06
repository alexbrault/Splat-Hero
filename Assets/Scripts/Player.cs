using UnityEngine;
using System.Collections;

public class Player : Entity {
	
	public enum PlayerID
	{
		PLAYER1,
		PLAYER2
	};
	
	public PlayerID playerID { get; set; }
	
	protected const float MAX_HERO_SPEED = 40.0f;
    protected const float HERO_ACCELERATION = 6.0f;
    protected const float HERO_DECELERATION_ACTIVE = 0.5f;
    protected const float HERO_DECELERATION_PASSIVE = 0.3f;
	
	// Use this for initialization
	void Start () {
		base.Start();
		
		spritesheet = new Spritesheet(gameObject);
		spritesheet.Load("Sprites/ironman2");
		spritesheet.CreateAnimation("Patrick", 300);
		spritesheet.AddFrame("Patrick", 48, 0, 16, 16);
		spritesheet.SetCurrentAnimation("Patrick");
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

        if (xMovement == 0)
        {
            gameObject.rigidbody.AddForce(new Vector3(-actualForce.x * HERO_DECELERATION_PASSIVE, 0, 0));
        } else if (xMovement * actualForce.x < 0.0f)
        {
            gameObject.rigidbody.AddForce(new Vector3(-actualForce.x * HERO_DECELERATION_ACTIVE, 0, 0));
        }

        if (zMovement == 0)
        {
            gameObject.rigidbody.AddForce(new Vector3(0, 0, -actualForce.z * HERO_DECELERATION_PASSIVE));
        }else if (zMovement * actualForce.z < 0.0f)
        {
            gameObject.rigidbody.AddForce(new Vector3(0, 0, -actualForce.z * HERO_DECELERATION_ACTIVE));
        }

        actualForce = gameObject.rigidbody.velocity;

        if (actualForce.x > MAX_HERO_SPEED)
        { 
            xMovement = MAX_HERO_SPEED - actualForce.x;
        }else if (actualForce.x < -MAX_HERO_SPEED) 
        {
            xMovement = -MAX_HERO_SPEED - actualForce.x;
        }

        if (actualForce.z > MAX_HERO_SPEED)
        {
            zMovement = MAX_HERO_SPEED - actualForce.z;
        } else if (actualForce.z < -MAX_HERO_SPEED)
        {
            zMovement = -MAX_HERO_SPEED - actualForce.z;
        }

        gameObject.rigidbody.AddForce(new Vector3(HERO_ACCELERATION * xMovement, 0, HERO_ACCELERATION * zMovement));
	}
	
	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts) {
			if(contact.otherCollider.gameObject.CompareTag("Ball"))
			{
				GoblinBall ball = (GoblinBall)contact.otherCollider.gameObject.GetComponent("GoblinBall");
				ball.Lock();
				
				Vector3 vect = contact.otherCollider.transform.position - gameObject.transform.position;
				vect.Normalize();
				vect *= 50;
	            contact.otherCollider.rigidbody.AddForce(vect);
			}
        }
	}
}
