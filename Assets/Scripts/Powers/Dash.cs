using UnityEngine;
using System.Collections;

public class Dash : Power {
	
	public override void StartPower()
	{
		useCooldown = 0.5f;
		powerCooldown = 3.0f;
	}
	
	public override void ActivatePower()
	{
		if( attachedPlayer.playerID == Player.PlayerID.PLAYER1 && Input.GetAxisRaw("Player1_Fire") > 0 && !powerInCooldown ||
			attachedPlayer.playerID == Player.PlayerID.PLAYER2 && Input.GetAxisRaw("Player2_Fire") > 0 && !powerInCooldown )
		{
			ProcessDash();
			powerInUse = true;
			powerInCooldown = true;
			cooldown = 0;
		}
	}
	
	public override void ProcessPower()
	{
	}
	
	public override void UseCooldownCallback()
	{
		attachedPlayer.CanMove = true;
	}
	
	public override void PowerCooldownCallback()
	{
	}
	
	void ProcessDash()
	{
		attachedPlayer.CanMove = false;
		
		Vector3 dashDirection = gameObject.rigidbody.velocity;
		dashDirection.Normalize();
		gameObject.rigidbody.AddForce( dashDirection * 1000 );
	}
	
	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts) {
			if(contact.otherCollider.gameObject.CompareTag("Player") && powerInUse)
			{
				Status status = contact.otherCollider.gameObject.AddComponent<Stunned>();
				status.SetEntity(contact.otherCollider.gameObject.GetComponent<Player>());
			}
        }
	}
}
