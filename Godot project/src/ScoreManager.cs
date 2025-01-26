using Godot;
using System;

public partial class ScoreManager : Node
{
	private int score = 0;
	private Label scoreLabel;

	public override void _Ready() {
		scoreLabel = GetNode<Label>("Canvas/ScoreLabel");
		scoreLabel.Text = "Score: 0";
		PlayerControls.OnGainScore += IncrementScore;
	}

	public void IncrementScore() {
		score++;
		scoreLabel.Text = "Score: " + score;
		GD.Print("Score: " + score);
	}
}
