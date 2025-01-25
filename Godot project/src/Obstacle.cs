using Godot;
using System;

public partial class Obstacle : Node3D {

	[Export]
	public float riverWidth = 20;

	[Export]
	public float travelSpeed = 1;

	public override void _Ready() {
		// Start from a random X position;
		RandomNumberGenerator spawnX = new();
		Position = new(Position.X + (spawnX.RandfRange(-1, 1) * (riverWidth / 2)), Position.Y, Position.Z);
	}

	public override void _Process(double delta) {
		// Move the obstacle along the river (aka the Z axis)
		Position = new(Position.X, Position.Y, +Position.Z + (travelSpeed * (float)delta));
	}
}
