using Godot;
using System;

public class Mob : RigidBody2D
{
	[Export] public int minSpeed = 150;
	[Export] public int maxSpeed = 400;
	
	public override void _Ready()
	{
		var animSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		animSprite.Playing = true;
		
		string[] mobTypes = animSprite.Frames.GetAnimationNames();

		animSprite.Animation = mobTypes[GD.Randi() % mobTypes.Length];
	}

	public void OnVisibilityNotifier2DScreenExited()
	{
		QueueFree();
	}
}


