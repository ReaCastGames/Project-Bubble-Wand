using Godot;
using System;

public partial class Score : HBoxContainer
{
	Label scoreLabel;

	public override void _Ready() {
		scoreLabel = GetNode<Label>("Label");
		scoreLabel.Text = "Score: 0";
	}

	public void UpdateScore(int score) {
		scoreLabel.Text = "Score: " + score;
	}
}
