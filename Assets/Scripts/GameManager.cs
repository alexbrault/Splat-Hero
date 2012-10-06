using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public List<GameObject> entities = new List<GameObject>();
	public List<GameObject> Entities { get{ return entities; } }
	
	public GameObject goblinBall;
	public GameObject player1;
	public GameObject player2;
	
	// Use this for initialization
	void Start () {
		Entities.Add((GameObject)Instantiate(goblinBall));
		
		GameObject p1o = (GameObject)Instantiate(player1);
		p1o.GetComponent<Player>().playerID = Player.PlayerID.PLAYER1;
		
		GameObject p2o = (GameObject)Instantiate(player2);
		p2o.GetComponent<Player>().playerID = Player.PlayerID.PLAYER2;
		
		Entities.Add(p2o);
		Entities.Add(p1o);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
