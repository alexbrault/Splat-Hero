using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public List<GameObject> entities = new List<GameObject>();
	public List<GameObject> Entities { get{ return entities; } }
	
	public GameObject goblinBall;
	public GameObject player1;
	public GameObject player2;
	
	public static GameManager Instance { get; private set; }
	
	// Use this for initialization
	void Start () {
		if (Instance == null) {
			Instance = this;
		}
		
		GameObject p1o = (GameObject)Instantiate(player1);
		p1o.GetComponent<Player>().playerID = Player.PlayerID.PLAYER1;
		
		GameObject p2o = (GameObject)Instantiate(player2);
		p2o.GetComponent<Player>().playerID = Player.PlayerID.PLAYER2;
		
		Entities.Add(p2o);
		Entities.Add(p1o);
		
		NewBall ();
	}
	
	public void DestroyObject(GameObject o) {
		Entities.Remove(o);
		Destroy (o);
	}
	
	public void NewBall() {
		Entities.Add((GameObject)Instantiate(goblinBall));
	}
	
	void OnDestroy() {
		if (Instance == this) {
			Instance = null;
		}
	}
}
