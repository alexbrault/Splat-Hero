using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
	
	Spritesheet spritesheet;
	
	// Use this for initialization
	protected void Start () {
		spritesheet = new Spritesheet(gameObject);
		spritesheet.Load("Sprites/ironman2");
		spritesheet.CreateAnimation("Patrick", 300);
		spritesheet.AddFrame("Patrick", 68, 96, 54, 96);
		spritesheet.AddFrame("Patrick", 122, 96, 54, 96);
		spritesheet.AddFrame("Patrick", 177, 96, 54, 96);
		spritesheet.SetCurrentAnimation("Patrick");
	}
	
	// Update is called once per frame
	protected void Update () {
		spritesheet.Render();
	}
}
