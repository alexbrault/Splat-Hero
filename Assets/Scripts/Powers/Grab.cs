using UnityEngine;
using System.Collections;

public class Grab : Power {
	
	GameObject grabbedEntity = null;
	
	public void Drop()
	{
		if(grabbedEntity != null)
		{
			ResetPower();
			powerInCooldown = true;
			useCooldown = 1.0f;
			powerCooldown = 1.0f;
			cooldown = 0;
			
			grabbedEntity.renderer.enabled = true;
			attachedPlayer.manageAnimation = true;
			grabbedEntity.GetComponent<Grabbed>().EndStatusEffect();
			grabbedEntity = null;
		}
		
		else
			StartPower();
	}
	
	public override void StartPower()
	{
		useCooldown = 1.0f;
		powerCooldown = 5.0f;
		grabbedEntity = null;
	}
	
	public override void ActivatePower()
	{
		if( (attachedPlayer.playerID == Player.PlayerID.PLAYER1 && Input.GetAxisRaw("Player1_Fire") > 0 && !powerInCooldown) ||
			(attachedPlayer.playerID == Player.PlayerID.PLAYER2 && Input.GetAxisRaw("Player2_Fire") > 0 && !powerInCooldown) ||
			(attachedPlayer.playerID == Player.PlayerID.PLAYER3 && Input.GetAxisRaw("Player3_Fire") > 0 && !powerInCooldown) ||
			(attachedPlayer.playerID == Player.PlayerID.PLAYER4 && Input.GetAxisRaw("Player4_Fire") > 0 && !powerInCooldown))
		{
			ProcessGrab();
		}
		
		if(!attachedPlayer.CanMove)
		{
			attachedPlayer.gameObject.rigidbody.velocity = new Vector3(0,0,0);
			powerInUse = true;
			powerInCooldown = true;
			cooldown = 0;
		}
	}
	
	public override void ProcessPower()
	{
		if(grabbedEntity != null)
		{
			float xMovement = 0.0f;
			
			if(attachedPlayer.playerID == Player.PlayerID.PLAYER1)
				xMovement = -Input.GetAxis("Player1_MoveX");
			
			else if(attachedPlayer.playerID == Player.PlayerID.PLAYER2)
				xMovement = -Input.GetAxis("Player2_MoveX");
			
			else if(attachedPlayer.playerID == Player.PlayerID.PLAYER3)
				xMovement = -Input.GetAxis("Player3_MoveX");
			
			else if(attachedPlayer.playerID == Player.PlayerID.PLAYER4)
				xMovement = -Input.GetAxis("Player4_MoveX");
			
			if(xMovement < 0)
				attachedPlayer.PlayAnimation("GrabLeft");
			
			else if(xMovement > 0)
				attachedPlayer.PlayAnimation("GrabRight");
		}
		
		if(!powerInUse && powerInCooldown)
		{
			if( (attachedPlayer.playerID == Player.PlayerID.PLAYER1 && Input.GetAxisRaw("Player1_Fire") > 0 && grabbedEntity != null) ||
				(attachedPlayer.playerID == Player.PlayerID.PLAYER2 && Input.GetAxisRaw("Player2_Fire") > 0 && grabbedEntity != null) ||
				(attachedPlayer.playerID == Player.PlayerID.PLAYER3 && Input.GetAxisRaw("Player3_Fire") > 0 && grabbedEntity != null) ||
				(attachedPlayer.playerID == Player.PlayerID.PLAYER4 && Input.GetAxisRaw("Player4_Fire") > 0 && grabbedEntity != null))
			{
				Vector3 shoot = gameObject.rigidbody.velocity;
				shoot.Normalize();
				
				grabbedEntity.GetComponent<GoblinBall>().Lock();
				Destroy(grabbedEntity.GetComponent<Grabbed>());
				
				gameObject.GetComponent<Entity>().DelayIgnoreCollision(gameObject.collider, grabbedEntity.collider, 1, false);
				
				grabbedEntity.renderer.enabled = true;
				attachedPlayer.manageAnimation = true;
				grabbedEntity.rigidbody.velocity = new Vector3(0,0,0);
				grabbedEntity.rigidbody.AddForce(gameObject.transform.position + (shoot * 500));
				
				ResetPower();
				grabbedEntity = null;
				powerInCooldown = true;
				useCooldown = 1.0f;
				powerCooldown = 1.0f;
				cooldown = 0;
			}
		}
	}
	
	public override void UseCooldownCallback()
	{
		attachedPlayer.CanMove = true;
		
		if(grabbedEntity == null)
		{
			ResetPower();
			StartPower();
		}
	}
	
	public override void PowerCooldownCallback()
	{
		Drop();
	}
	
	void ProcessGrab()
	{
		attachedPlayer.CanMove = false;
		
		PlayGrabAnimation();
		
		Collider[] collisions = Physics.OverlapSphere(gameObject.transform.position, 20);
		
		foreach (Collider collision in collisions)
		{
			if(collision.collider == gameObject.collider)
				continue;
			
			if(collision.collider.gameObject.CompareTag("Ball"))
			{
				Status grabbed = collision.collider.gameObject.AddComponent<Grabbed>();
				grabbed.SetEntity(gameObject.GetComponent<Player>());
				
				grabbedEntity = collision.collider.gameObject;
				Physics.IgnoreCollision(gameObject.collider, grabbedEntity.collider);
				
				grabbedEntity.renderer.enabled = false;
				attachedPlayer.manageAnimation = false;
			}
			
			else if(collision.collider.gameObject.CompareTag("Player"))
			{
				Status grabbed = collision.collider.gameObject.AddComponent<Grabbed>();
				grabbed.SetEntity(gameObject.GetComponent<Player>());
			}
		}
	}
	
	void PlayGrabAnimation()
	{
		if(attachedPlayer.facing == Player.Facing.LEFT)
		{
			attachedPlayer.PlayAnimation("IdleLeft");
		}
		
		else
		{
			attachedPlayer.PlayAnimation("IdleRight");
		}
	}
}
