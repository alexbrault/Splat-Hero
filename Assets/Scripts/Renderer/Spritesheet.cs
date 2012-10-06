using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spritesheet {
	
	GameObject gameobject;
	Texture2D spritesheet = null;
	
	Dictionary<string, SpriteAnimation> animations = new Dictionary<string, SpriteAnimation>();
	SpriteAnimation activeAnimation = null;
	
	public Spritesheet()
	{
	}
	
	public void Render(float x, float z)
	{
		gameobject.transform.position = new Vector3(x, 0, z);
		gameobject.renderer.material.mainTexture = activeAnimation.CurrentFrame.GetFrame(spritesheet);
	}
	
	public bool Load(string sprite)
	{
		gameobject = GameObject.CreatePrimitive(PrimitiveType.Plane);
		spritesheet = (Texture2D)Resources.Load(sprite);
		
		if(spritesheet == null)
			return false;
		
		gameobject.renderer.material.shader = Shader.Find("Transparent/Diffuse");
		return true;
	}
	
	public void CreateAnimation(string name)
	{
		SpriteAnimation animation = new SpriteAnimation();
		animations.Add(name, animation);
	}
	
	public bool AddFrame(string animationName, int x, int y, int width, int height, int offsetX = 0, int offsetY = 0)
	{
		if(animations.ContainsKey(animationName))
		{
			AnimationFrame frame = new AnimationFrame(x, y, width, height, offsetX, offsetY);
			animations[animationName].Add(frame);
			
			return true;
		}
		
		else
			return false;
	}
	
	public void SetCurrentAnimation(string animation)
	{
		if(animations.ContainsKey(animation))
			activeAnimation = animations[animation];
	}
}
