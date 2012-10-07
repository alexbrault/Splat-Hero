using UnityEngine;
using System.Collections;

public class Whorl : Power {
	
	public override void StartPower()
	{
		useCooldown = 0.2f;
		powerCooldown = 2;
	}
	
	public override void ActivatePower()
	{
		if( !powerInCooldown && (
			(attachedPlayer.playerID == Player.PlayerID.PLAYER1 && Input.GetAxisRaw("Player1_Fire") > 0) ||
			(attachedPlayer.playerID == Player.PlayerID.PLAYER2 && Input.GetAxisRaw("Player2_Fire") > 0) ||
			(attachedPlayer.playerID == Player.PlayerID.PLAYER3 && Input.GetAxisRaw("Player3_Fire") > 0) ||
			(attachedPlayer.playerID == Player.PlayerID.PLAYER4 && Input.GetAxisRaw("Player4_Fire") > 0)))
		{
			ProcessWhorl();
			powerInUse = true;
			powerInCooldown = true;
		}
	}

	public void ProcessWhorl ()
	{
		Collider[] collisions = Physics.OverlapSphere(gameObject.transform.position, 20);
		
		foreach (Collider collision in collisions)
		{
			if(collision.collider == gameObject.collider)
				continue;
			
			if(collision.collider.CompareTag("Ball"))
			{
				GoblinBall ball = (GoblinBall)collision.collider.gameObject.GetComponent("GoblinBall");
				ball.Lock();
				
				Vector3 vect = collision.collider.transform.position - gameObject.transform.position;
				vect.Normalize();
				vect *= 75;
	            collision.collider.rigidbody.AddForce(vect);
			}
			
			else if(collision.collider.CompareTag("Player"))
			{
				if(collision.collider.gameObject.GetComponent<Stunned>() == null)
				{
					Status grabbed = collision.collider.gameObject.AddComponent<Stunned>();
					grabbed.SetEntity(collision.collider.GetComponent<Player>());
				}
			}
		}
		
	}
	
	public override void ProcessPower(){}
	public override void UseCooldownCallback(){}
	public override void PowerCooldownCallback(){ResetPower();}
}