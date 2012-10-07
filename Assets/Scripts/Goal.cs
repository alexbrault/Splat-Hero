using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
	public int Score;
	private GUIText label;
	
	private void Start() {
		label = GetComponentInChildren<GUIText>();
		label.text = Score.ToString();
	}
	
	private void OnCollisionEnter(Collision other) {
		GoblinBall candidate = other.gameObject.GetComponent<GoblinBall>();
		if (candidate != null && candidate.isLocked) {
			Score++;
			label.text = Score.ToString();
			GameManager.Instance.DestroyObject(candidate.gameObject);
			StartCoroutine(MakeBallSoonish());
			SplatterShower.Instance.ShowSplatter();
		}
	}
	
	private IEnumerator MakeBallSoonish() {
			yield return new WaitForSeconds(2.5f);
			GameManager.Instance.NewBall();
	}
}
