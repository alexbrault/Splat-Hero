using UnityEngine;
using System.Collections;

public class Grab : Power {

	public override void StartPower()
	{
		useCooldown = 1.0f;
		powerCooldown = 1.0f;
	}
	
	public override void ProcessPower()
	{
		if( attachedPlayer.playerID == Player.PlayerID.PLAYER1 && Input.GetAxisRaw("Player1_Fire") > 0 && !powerInCooldown ||
			attachedPlayer.playerID == Player.PlayerID.PLAYER2 && Input.GetAxisRaw("Player2_Fire") > 0 && !powerInCooldown )
		{
			ProcessGrab();
			powerInUse = true;
			powerInCooldown = true;
			cooldown = 0;
		}
		
		if(!attachedPlayer.CanMove)
		{
			attachedPlayer.gameObject.rigidbody.velocity = new Vector3(0,0,0);
		}
	}
	
	public override void UseCooldownCallback()
	{
		attachedPlayer.CanMove = true;
	}
	
	public override void PowerCooldownCallback()
	{
	}
	
	void ProcessGrab()
	{
		attachedPlayer.CanMove = false;
		
		// Animation
		
		Collider[] collisions = Physics.OverlapSphere(gameObject.transform.position, 20);
		
		foreach (Collider collision in collisions)
		{
			if(collision.collider == gameObject.collider)
				continue;
			
			if(collision.collider.gameObject.CompareTag("Ball"))
			{
				Status grabbed = collision.collider.gameObject.AddComponent<Grabbed>();
				grabbed.SetEntity(gameObject.GetComponent<Player>());
			}
		}
	}
}
