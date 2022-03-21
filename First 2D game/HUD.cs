using Godot;
using System;

public class HUD : CanvasLayer
{
	[Signal] public delegate void StartGame();

	public void UpdateScore(int score) 
	{
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}
	
	public void ShowMessage(string text) 
	{
		GetNode<Label>("MessageLabel").Text = text;
		GetNode<Label>("MessageLabel").Show();
		GetNode<Timer>("MessageTimer").Start();
	}
	
	async public void ShowGameOver() 
	{
		GetTree().CallGroup("mobs", "queue_free");
		
		await ToSignal(GetTree().CreateTimer(0.2f), "timeout");
		GetNode<Label>("MessageLabel").Text = "Game over";
		GetNode<Label>("MessageLabel").Show();
		
		await ToSignal(GetTree().CreateTimer(0.6f), "timeout");
		
		GetNode<Button>("Button").Show();
		
	}

	public void _on_Button_pressed()
	{
		GetNode<Button>("Button").Hide();
		
		GetNode<Label>("MessageLabel").Hide();
		
		EmitSignal("StartGame");
	}
	
	public void _on_MessageTimer_timeout()
	{
		GetNode<Label>("MessageLabel").Hide();
		
	}
	
	
}
