using UnityEngine;
using System.Collections;

public class Player : Entity {
	
	public enum PlayerID
	{
		PLAYER1,
		PLAYER2
	};
	
	public enum Facing
	{
		LEFT,
		RIGHT
	}
	
	public PlayerID playerID { get; set; }
	public Facing facing = Facing.LEFT;
	
	protected const float MAX_HERO_SPEED = 40.0f;
    protected const float HERO_ACCELERATION = 6.0f;
    protected const float HERO_DECELERATION_ACTIVE = 0.5f;
    protected const float HERO_DECELERATION_PASSIVE = 0.3f;
	
	// Use this for initialization
	void Start () {
		base.Start();
		
		spritesheet = new Spritesheet(gameObject);
		spritesheet.Load("Sprites/truc");
		
		spritesheet.CreateAnimation("RunLeft", 300);
		spritesheet.AddFrame("RunLeft", 0, 0, 48, 64);
		spritesheet.AddFrame("RunLeft", 0, 64, 48, 64);
		spritesheet.AddFrame("RunLeft", 0, 128, 48, 64);
		spritesheet.AddFrame("RunLeft", 0, 192, 48, 64);
		
		spritesheet.CreateAnimation("RunRight", 300);
		spritesheet.AddFrame("RunRight", 48, 0, 48, 64);
		spritesheet.AddFrame("RunRight", 48, 64, 48, 64);
		spritesheet.AddFrame("RunRight", 48, 128, 48, 64);
		spritesheet.AddFrame("RunRight", 48, 192, 48, 64);
		
		spritesheet.CreateAnimation("IdleLeft", 0);
		spritesheet.AddFrame("IdleLeft", 0, 0, 48, 64);
		
		spritesheet.CreateAnimation("IdleRight", 0);
		spritesheet.AddFrame("IdleRight", 48, 0, 48, 64);
		
		spritesheet.SetCurrentAnimation("IdleLeft");

        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<GameCamera>().RegisterPlayer(this);
	}
	
	// Update is called once per frame
	void Update () {
		if(CanMove)
		{
			Move();
		
			Vector3 position = gameObject.transform.position;
			position.y = 0;
			gameObject.transform.position = position;
		}
		
		base.Update();
	}
	
	public void SetPowers()
	{
		if(playerID == PlayerID.PLAYER1)
		{
			Power kick = gameObject.AddComponent<SmoothKick>();
			kick.SetPlayer(this);
			
			Power dash = gameObject.AddComponent<Dash>();
			dash.SetPlayer(this);
		}
		
		else
		{
			Power grab = gameObject.AddComponent<Grab>();
			grab.SetPlayer(this);
		}
	}
	
	protected void Move()
	{
		float xMovement = 0.0f;
		float zMovement = 0.0f;
		
		if(playerID == PlayerID.PLAYER1)
		{
			xMovement = -Input.GetAxisRaw("Player1_MoveX");
			zMovement = -Input.GetAxisRaw("Player1_MoveZ");
		}
		
		else if(playerID == PlayerID.PLAYER2)
		{
			xMovement = -Input.GetAxisRaw("Player2_MoveX");
			zMovement = -Input.GetAxisRaw("Player2_MoveZ");
		}
		
		SetAnimation(xMovement);

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
	
	void SetAnimation(float xMovement)
	{
		if(xMovement > 0)
		{
			facing = Facing.LEFT;
			spritesheet.SetCurrentAnimation("RunLeft");
		}
		
		else if(xMovement < 0)
		{
			facing = Facing.RIGHT;
			spritesheet.SetCurrentAnimation("RunRight");
		}
		
		else
		{
			if(facing == Facing.LEFT)
				spritesheet.SetCurrentAnimation("IdleLeft");
			
			else
				spritesheet.SetCurrentAnimation("IdleRight");
		}
	}
	
	void OnCollisionStay(Collision collision)
	{
		if(CanMove)
		{
			foreach (ContactPoint contact in collision.contacts) {
				Vector3 vect = gameObject.transform.position - contact.point;
				vect.Normalize();
				
				gameObject.transform.Translate(vect * 0.1f);
	        }
		}
	}
}
