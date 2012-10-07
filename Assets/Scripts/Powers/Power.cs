using UnityEngine;
using System.Collections;

abstract public class Power : MonoBehaviour {
	
	protected bool powerInUse = false;
	protected bool powerInCooldown = false;
	
	protected float useCooldown;
	protected float powerCooldown;
	protected float cooldown;
	
	protected Player attachedPlayer = null;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(powerInCooldown)
		{
			cooldown += Time.deltaTime;
			
			if(powerInUse && cooldown > useCooldown)
			{
				powerInUse = false;
				UseCooldownCallback();
			}
			
			if(powerInCooldown && cooldown > powerCooldown)
			{
				powerInCooldown = false;
				PowerCooldownCallback();
			}
			
			ProcessPower();
		}
	
		else
		{
			ActivatePower();
		}
	}
	
	public void ResetPower()
	{
		powerInUse = false;
		powerInCooldown = false;
		cooldown = 0;
	}
	
	public void SetPlayer(Player player)
	{
		attachedPlayer = player;		
		StartPower();
	}
	
	public abstract void StartPower();
	public abstract void ActivatePower();
	public abstract void ProcessPower();
	public abstract void UseCooldownCallback();
	public abstract void PowerCooldownCallback();
}
