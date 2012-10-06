using UnityEngine;
using System.Collections;

public abstract class Status : MonoBehaviour {
	
	protected float statusTime = 0;
	protected Player attachedPlayer = null;
	
	bool inCooldown = false;
	float cooldown = 0;
	
	// Update is called once per frame
	protected void Update () {
	
		if(inCooldown)
		{
			ProcessStatusEffect();
			
			cooldown += Time.deltaTime;
			if(cooldown > statusTime)
			{
				inCooldown = false;
				EndStatusEffect();
			}
		}
	}
	
	public void SetPlayer(Player player)
	{
		attachedPlayer = player;
		inCooldown = true;
		
		StartStatusEffect();
	}
	
	public abstract void StartStatusEffect();
	public abstract void ProcessStatusEffect();
	public abstract void EndStatusEffect();
}
