using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpriteAnimation : IEnumerable
{
	private List<AnimationFrame> frames = new List<AnimationFrame> ();
	
	public int FrameRate { get; private set; }
	
	private int rawFrame = 0;
	private int currentFrameIndex = 0;
	
	public SpriteAnimation(int framerate) {
		FrameRate = framerate;
	}
	
	public void Add (AnimationFrame frame)
	{
		frames.Add (frame);
	}
	
	IEnumerator IEnumerable.GetEnumerator ()
	{
		return frames.GetEnumerator ();
	}
	
	public AnimationFrame CurrentFrame {
		get {
			rawFrame++;
			if (rawFrame >= FrameRate)
			{
				rawFrame -= FrameRate;
				currentFrameIndex = (currentFrameIndex + 1) % frames.Count;
			}
			
			return frames [currentFrameIndex];
		}
	}
}
