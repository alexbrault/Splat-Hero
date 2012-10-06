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
			
			if(cooldown > useCooldown)
			{
				powerInUse = false;
				UseCooldownCallback();
			}
			
			if(cooldown > powerCooldown)
			{
				powerInCooldown = false;
				PowerCooldownCallback();
			}
		}
	
		else
		{
			ProcessPower();
		}
	}
	
	public void SetPlayer(Player player)
	{
		attachedPlayer = player;		
		StartPower();
	}
	
	public abstract void StartPower();
	public abstract void ProcessPower();
	public abstract void UseCooldownCallback();
	public abstract void PowerCooldownCallback();
}
