using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public List<GameObject> entities = new List<GameObject>();
	public List<GameObject> Entities { get{ return entities; } }
	
	public GameObject goblinBall;
	public GameObject player1;
	public GameObject player2;
	
	private Transform player1_Transform;
	private Transform player2_Transform;
	private Vector3 player1_Initial;
	private Vector3 player2_Initial;
	
	public static GameManager Instance { get; private set; }
	
	// Use this for initialization
	void Start () {
		if (Instance == null) {
			Instance = this;
		}
		
		GameObject p1o = (GameObject)Instantiate(player1);
		p1o.GetComponent<Player>().playerID = Player.PlayerID.PLAYER1;
		p1o.GetComponent<Player>().SetPowers();
		player1_Transform = p1o.transform;
		player1_Initial = player1_Transform.position;
		
		GameObject p2o = (GameObject)Instantiate(player2);
		p2o.GetComponent<Player>().playerID = Player.PlayerID.PLAYER2;
		p2o.GetComponent<Player>().SetPowers();
		player2_Transform = p2o.transform;
		player2_Initial = player2_Transform.position;
		
		Entities.Add(p2o);
		Entities.Add(p1o);
		
		Entities.AddRange(GameObject.FindGameObjectsWithTag("Goal"));
		
		NewBall ();
	}
	
	public void DestroyObject(GameObject o) {
		Entities.Remove(o);
		Destroy (o);
	}
	
	public void NewBall() {
		Entities.Add((GameObject)Instantiate(goblinBall));
		player1_Transform.position = player1_Initial;
		player2_Transform.position = player2_Initial;
	}
	
	void OnDestroy() {
		if (Instance == this) {
			Instance = null;
		}
	}
}
