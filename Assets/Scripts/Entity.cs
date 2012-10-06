using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	protected Spritesheet spritesheet;
	public bool CanMove{ get; set; }
	
	// Use this for initialization
	protected void Start () {
		CanMove = true;
	}
	
	// Update is called once per frame
	protected void Update () {
		spritesheet.Render();
	}
}
