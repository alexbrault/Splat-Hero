using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpriteAnimation : IEnumerable {
	private List<AnimationFrame> frames = new List<AnimationFrame>();
	
	public void Add(AnimationFrame frame) {
		frames.Add(frame);
	}
	
	IEnumerator IEnumerable.GetEnumerator() {
		return frames.GetEnumerator();
	}
}
