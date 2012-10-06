using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	protected Spritesheet spritesheet;
	
	// Use this for initialization
	protected void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
		spritesheet.Render();
	}
}
