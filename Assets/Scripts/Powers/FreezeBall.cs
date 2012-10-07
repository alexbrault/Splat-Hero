using UnityEngine;
using System.Collections;

public class FreezeBall : Power {
	
	GoblinBall ball = null;

	public override void StartPower()
	{
		useCooldown = 0.5f;
		powerCooldown = 5.0f;
	}
	
	public override void ActivatePower()
	{
		if( (attachedPlayer.playerID == Player.PlayerID.PLAYER1 && Input.GetAxisRaw("Player1_Fire") > 0 && !powerInCooldown) ||
			(attachedPlayer.playerID == Player.PlayerID.PLAYER2 && Input.GetAxisRaw("Player2_Fire") > 0 && !powerInCooldown) ||
			(attachedPlayer.playerID == Player.PlayerID.PLAYER3 && Input.GetAxisRaw("Player3_Fire") > 0 && !powerInCooldown) ||
			(attachedPlayer.playerID == Player.PlayerID.PLAYER4 && Input.GetAxisRaw("Player4_Fire") > 0 && !powerInCooldown))
		{
			attachedPlayer.CanMove = false;
			attachedPlayer.rigidbody.velocity = new Vector3(0, 0, 0);
			
			ball = GameObject.Find("GoblinBall(Clone)").GetComponent<GoblinBall>();
			
			AudioClip clip = (AudioClip)Resources.Load("Audio/spell");
			gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
			
			powerInUse = true;
			powerInCooldown = true;
			cooldown = 0;
			
			if(ball.gameObject.GetComponent<Frozen>() == null)
			{
				Status status = ball.gameObject.AddComponent<Frozen>();
				status.SetEntity(ball.gameObject.GetComponent<GoblinBall>());
				
				GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
				
				for(int i = 0; i < players.Length; i++)
				{
					if(players[i].GetComponent<Grab>() != null)
					{
						players[i].GetComponent<Grab>().InformBallFreeze();
					}
				}
			}
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
}
