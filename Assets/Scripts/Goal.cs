using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
	public int Score;
	
	private void OnCollisionEnter(Collision other) {
		GoblinBall candidate = other.gameObject.GetComponent<GoblinBall>();
		if (candidate != null) {
			Score++;
			GameManager.Instance.DestroyObject(candidate.gameObject);
			GameManager.Instance.NewBall();
		}
	}
}
