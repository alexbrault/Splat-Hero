using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	public Texture2D HUDBackground;
	public Texture2D Player1_Active;
	public Texture2D Player1_Bench;
	public Texture2D Player2_Active;
	public Texture2D Player2_Bench;
	public Font TimerFont;
	
	private float scale;
	
	private void Start() {
		Texture2D cropped = new Texture2D(581,60);
		cropped.SetPixels(HUDBackground.GetPixels(0, 4, 581, 60));
		cropped.Apply();
		HUDBackground = cropped;
		
		scale = Screen.width / 581.0f;
	}
	
	private void OnGUI() {
		GUIStyle style = new GUIStyle();
		style.fontSize = (int)(27 * scale);
		style.font = TimerFont;
		style.alignment = TextAnchor.MiddleCenter;
		
		
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.width / 10), HUDBackground);
		ScalableRect r = new ScalableRect(220, 0, 38, 38);
		GUI.DrawTexture(r * scale, Player1_Bench, ScaleMode.ScaleToFit);
		r = new ScalableRect(324, 0, 38, 38);
		GUI.DrawTexture(r * scale, Player2_Bench, ScaleMode.ScaleToFit);
		
		r = new ScalableRect(180, 0, 50, 50);
		GUI.DrawTexture(r * scale, Player1_Active, ScaleMode.ScaleToFit);
		r = new ScalableRect(351, 0, 50, 50);
		GUI.DrawTexture(r * scale, Player2_Active, ScaleMode.ScaleToFit);
		
		r = new ScalableRect(258, 0, 324-258, 40);
		GUI.Box (r * scale, GameManager.Instance.SecondsLeft.ToString("0"), style);
		
		
		r = new ScalableRect(4, 3, 44, 44);
		GUI.Box(r * scale, Goal.Player1_Goal.Score.ToString(), style);
		r = new ScalableRect(533, 3, 44, 44);
		GUI.Box(r * scale, Goal.Player2_Goal.Score.ToString(), style);
	}
}

public struct ScalableRect {
		private float x;
		private float y;
		private float w;
		private float h;
		
		public ScalableRect(Rect rhs) {
			x = rhs.x;
			y = rhs.y;
			w = rhs.width;
			h = rhs.height;
		}
		
		public ScalableRect(float left, float top, float width, float height) {
			x = left;
			y = top;
			w = width;
			h = height;
		}
		
		public static ScalableRect operator*(ScalableRect r, float m) {
			return new ScalableRect(r.x*m, r.y*m, r.w*m, r.h*m);
		}
		
		public static implicit operator Rect(ScalableRect r) {
			return new Rect(r.x, r.y, r.w, r.h);
		}
	}