using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {
	
	bool powerInCooldown = false;
	float cooldown;
	
	Player attachedPlayer = null;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(powerInCooldown)
		{
			cooldown += Time.deltaTime;
			
			if(cooldown > 0.5)
				attachedPlayer.CanMove = true;
			
			if(cooldown > 3)
				powerInCooldown = false;
		}
	
		else
		{
			if( attachedPlayer.playerID == Player.PlayerID.PLAYER1 && Input.GetAxisRaw("Player1_Fire") > 0 && !powerInCooldown ||
				attachedPlayer.playerID == Player.PlayerID.PLAYER2 && Input.GetAxisRaw("Player2_Fire") > 0 && !powerInCooldown )
			{
				UseDash();
				powerInCooldown = true;
				cooldown = 0;
			}
		}
	}
	
	void UseDash()
	{
		Debug.Log("Dash!");
		
		attachedPlayer.CanMove = false;
		
		Vector3 dashDirection = gameObject.rigidbody.velocity;
		dashDirection.Normalize();
		gameObject.rigidbody.AddForce( dashDirection * 1000 );
	}
	
	public void SetPlayer(Player player)
	{
		attachedPlayer = player;
	}
}
