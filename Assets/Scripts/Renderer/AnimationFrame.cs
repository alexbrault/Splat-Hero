using UnityEngine;
using System.Collections;

public class AnimationFrame
{
	public int X { get; private set; }

	public int Y { get; private set; }

	public int Width { get; private set; }

	public int Height { get; private set; }

	public int OffsetX { get; private set; }

	public int OffsetY { get; private set; }
	
	public AnimationFrame (int x, int y, int width, int height, int offX = 0, int offY = 0)
	{
		X = x;
		Y = y;
		Width = width;
		Height = height;
		OffsetX = offX;
		OffsetY = offY;
	}
}
