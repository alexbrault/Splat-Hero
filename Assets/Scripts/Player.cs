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
	};
	
	public enum Character
	{
		RIRONMAN,
		LE_TRUC
	};
	
	public Character character = Character.RIRONMAN;
	
	public PlayerID playerID = PlayerID.PLAYER1;
	public Facing facing = Facing.LEFT;
	
	protected float MAX_HERO_SPEED = 40.0f;
    protected float HERO_ACCELERATION = 6.0f;
    protected float HERO_DECELERATION_ACTIVE = 0.5f;
    protected float HERO_DECELERATION_PASSIVE = 0.3f;
	
	public bool manageAnimation = true;
	
	// Use this for initialization
	void Start () {
		base.Start();
		
		switch(character)
		{
		case Character.RIRONMAN:
			CreateRironman();
			break;
			
		case Character.LE_TRUC:
			CreateLeTruc();
			break;
		}

        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<GameCamera>().RegisterPlayer(this);
	}
	
	void CreateRironman()
	{
		spritesheet = new Spritesheet(gameObject);
		spritesheet.Load("Sprites/ironMan");
		
		spritesheet.CreateAnimation("RunLeft", 300);
		spritesheet.AddFrame("RunLeft", 0, 0, 32, 32);
		spritesheet.AddFrame("RunLeft", 0, 32, 32, 32);
		spritesheet.AddFrame("RunLeft", 0, 64, 32, 32);
		spritesheet.AddFrame("RunLeft", 0, 96, 32, 32);
		
		spritesheet.CreateAnimation("RunRight", 300);
		spritesheet.AddFrame("RunRight", 32, 0, 32, 32);
		spritesheet.AddFrame("RunRight", 32, 32, 32, 32);
		spritesheet.AddFrame("RunRight", 32, 64, 32, 32);
		spritesheet.AddFrame("RunRight", 32, 96, 32, 32);
		
		spritesheet.CreateAnimation("IdleLeft", 0);
		spritesheet.AddFrame("IdleLeft", 0, 0, 32, 32);
		
		spritesheet.CreateAnimation("IdleRight", 0);
		spritesheet.AddFrame("IdleRight", 32, 0, 32, 32);
		
		spritesheet.SetCurrentAnimation("IdleLeft");
		
		// Stats
		MAX_HERO_SPEED = 70;
		HERO_ACCELERATION = 8.0f;
		
		// Powers
		Power kick = gameObject.AddComponent<SmoothKick>();
		kick.SetPlayer(this);
		
		Power dash = gameObject.AddComponent<Dash>();
		dash.SetPlayer(this);
	}
	
	void CreateLeTruc()
	{
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
		
		spritesheet.CreateAnimation("GrabLeft", 300);
		spritesheet.AddFrame("GrabLeft", 144, 0, 48, 64);
		spritesheet.AddFrame("GrabLeft", 144, 64, 48, 64);
		spritesheet.AddFrame("GrabLeft", 144, 128, 48, 64);
		spritesheet.AddFrame("GrabLeft", 144, 192, 48, 64);
		
		spritesheet.CreateAnimation("GrabRight", 300);
		spritesheet.AddFrame("GrabRight", 96, 0, 48, 64);
		spritesheet.AddFrame("GrabRight", 96, 64, 48, 64);
		spritesheet.AddFrame("GrabRight", 96, 128, 48, 64);
		spritesheet.AddFrame("GrabRight", 96, 192, 48, 64);
		
		spritesheet.CreateAnimation("IdleLeft", 0);
		spritesheet.AddFrame("IdleLeft", 0, 0, 48, 64);
		
		spritesheet.CreateAnimation("IdleRight", 0);
		spritesheet.AddFrame("IdleRight", 48, 0, 48, 64);
		
		spritesheet.CreateAnimation("IdleGrabLeft", 0);
		spritesheet.AddFrame("IdleGrabRight", 144, 0, 48, 64);
		
		spritesheet.CreateAnimation("IdleGrabRight", 0);
		spritesheet.AddFrame("IdleGrabLeft", 96, 0, 48, 64);		
		
		spritesheet.SetCurrentAnimation("IdleLeft");
		
		// Stats
		MAX_HERO_SPEED = 40.0f;
    	HERO_ACCELERATION = 6.0f;
		
		// Powers
		Power grab = gameObject.AddComponent<Grab>();
		grab.SetPlayer(this);
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
	
	protected void Move()
	{
		float xMovement = 0.0f;
		float zMovement = 0.0f;
		
		if(playerID == PlayerID.PLAYER1)
		{
			xMovement = -Input.GetAxis("Player1_MoveX");
			zMovement = -Input.GetAxis("Player1_MoveZ");
		}
		
		else if(playerID == PlayerID.PLAYER2)
		{
			xMovement = -Input.GetAxis("Player2_MoveX");
			zMovement = -Input.GetAxis("Player2_MoveZ");
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
		if(manageAnimation)
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
