using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public List<GameObject> entities = new List<GameObject>();
	public List<GameObject> Entities { get{ return entities; } }

	public GameObject goblinBall;
	public GameObject player1;

    // Use this for initialization
	void Start () 
    {
		Entities.Add((GameObject)Instantiate(goblinBall));
		Entities.Add((GameObject)Instantiate(player1));
	}
	
	// Update is called once per frame
	void Update () 
    {
	}
}
