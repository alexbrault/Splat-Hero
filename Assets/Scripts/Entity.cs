using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
	
	Spritesheet spritesheet;
	
	// Use this for initialization
	void Start () {
		spritesheet = new Spritesheet();
		spritesheet.Load("Sprites/ironman2");
		spritesheet.CreateAnimation("Patrick");
		spritesheet.AddFrame("Patrick", 68, 96, 56, 96);
		spritesheet.SetCurrentAnimation("Patrick");
	}
	
	// Update is called once per frame
	void Update () {
		spritesheet.Render();
	}
}
