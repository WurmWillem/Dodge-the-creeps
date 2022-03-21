using Godot;
using System;

public class Main : Node
{
	[Export] private PackedScene mobScene;
	public int score = 0;
	
	public override void _Ready() {
		GD.Randomize();
	}
	
	private void _on_MobTimer_timeout()
	{
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.UnitOffset = GD.Randf();
		
		var mob = (Mob)mobScene.Instance();
		AddChild(mob);
		
		mob.Position = mobSpawnLocation.Position;	
		
		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;
		direction += (float)GD.RandRange(-Mathf.Pi / 4,Mathf.Pi / 4);
		
		mob.Rotation = direction;
		
		var velocity = new Vector2((float)GD.RandRange(mob.minSpeed, mob.maxSpeed), 0);
		
		mob.LinearVelocity = velocity.Rotated(direction);
		
	}
	
	public void _on_ScoreTimer_timeout()
	{
		score++;
		var hud = GetNode<HUD>("HUD");
		hud.UpdateScore(score);
	}

	async public void NewGame()
	{
		score = 0;
		var hud = GetNode<HUD>("HUD");
		
		GetNode<Area2D>("Player").Position = GetNode<Position2D>("Position2D").Position;
		GetNode<Area2D>("Player").Show();
		GetNode<CollisionShape2D>("Player/CollisionShape2D").Disabled = false;
		
		hud.UpdateScore(score);
		
		var startTimer =GetNode<Timer>("StartTimer");
		startTimer.Start();
		hud.ShowMessage("Get ready...");
		
		await ToSignal(startTimer, "timeout");
		
		GetNode<Timer>("ScoreTimer").Start();
		GetNode<Timer>("MobTimer").Start();
		
	}
	
	public void GameOver()
	{
		GetNode<Timer>("ScoreTimer").Stop();
		GetNode<Timer>("MobTimer").Stop();
		
		GetNode<HUD>("HUD").ShowGameOver();
		
		
	}
	
	
}

