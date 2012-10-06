using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
	
	Spritesheet spritesheet;
	
	// Use this for initialization
	protected void Start () {
		spritesheet = new Spritesheet();
		spritesheet.Load("Sprites/ironman2");
		spritesheet.CreateAnimation("Patrick");
		spritesheet.AddFrame("Patrick", 68, 96, 56, 96);
		spritesheet.SetCurrentAnimation("Patrick");
	}
	
	// Update is called once per frame
	protected void Update () {
		Move();
		spritesheet.Render(gameObject.transform.position.x, gameObject.transform.position.z);
	}
	
	protected void Move()
	{
		float xMovement = -Input.GetAxisRaw("Player1_MoveX");
		float zMovement = -Input.GetAxisRaw("Player1_MoveZ");
		
		gameObject.rigidbody.AddForce(new Vector3(xMovement, 0, zMovement));
	}
}
