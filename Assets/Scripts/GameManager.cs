using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public List<GameObject> entities = new List<GameObject>();
	public List<GameObject> Entities { get{ return entities; } }
	
	public GameObject goblinBall;
	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;
	
	private Transform player1_Transform;
	private Transform player2_Transform;
	private Transform player3_Transform;
	private Transform player4_Transform;
	private Vector3 player1_Initial;
	private Vector3 player2_Initial;
	private Vector3 player3_Initial;
	private Vector3 player4_Initial;
	
	public float SecondsLeft = 30;
	private bool ballPlayed = false;
	
	public static GameManager Instance { get; private set; }
	
	// Use this for initialization
	void Start () {
		if (Instance == null) {
			Instance = this;
		}
		
		GameObject p1o = (GameObject)Instantiate(player1);
		player1_Transform = p1o.transform;
		player1_Initial = player1_Transform.position;
		
		GameObject p2o = (GameObject)Instantiate(player2);
		player2_Transform = p2o.transform;
		player2_Initial = player2_Transform.position;
		
		GameObject p3o = (GameObject)Instantiate(player3);
		player3_Transform = p3o.transform;
		player3_Initial = player3_Transform.position;
		
		GameObject p4o = (GameObject)Instantiate(player4);
		player4_Transform = p4o.transform;
		player4_Initial = player4_Transform.position;
		
		Entities.Add(p1o);
		Entities.Add(p2o);
		Entities.Add(p3o);
		Entities.Add(p4o);
		
		Entities.AddRange(GameObject.FindGameObjectsWithTag("Goal"));
		
		NewBall ();
	}
	
	public void DestroyObject(GameObject o) {
		if (o.GetComponent<GoblinBall>() != null) {
			ballPlayed = false;
		}
		
		Entities.Remove(o);
		Destroy (o);
	}
	
	public void NewBall() {
		Entities.Add((GameObject)Instantiate(goblinBall));
		player1_Transform.position = player1_Initial;
		player2_Transform.position = player2_Initial;
		player3_Transform.position = player3_Initial;
		player4_Transform.position = player4_Initial;
		ballPlayed = true;
	}
	
	void OnDestroy() {
		if (Instance == this) {
			Instance = null;
		}
	}
	
	private void Update() {
		if (ballPlayed) {
			SecondsLeft -= Time.deltaTime;
			if (SecondsLeft <= 0) {
				//goto end;
			}
		}
	}
}
