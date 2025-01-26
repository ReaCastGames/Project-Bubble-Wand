using Godot;

public partial class FlotsamItem : CharacterBody3D, IDespawn {

	[Export]
	public float riverWidth = 20;

	[Export]
	public float travelSpeed = 1;

	public override void _Ready() {
		// Start from a random X position;
		Position = new(Position.X + (GameScene.RandomNumberGenerator.RandfRange(-1, 1) * (riverWidth / 2)), Position.Y, Position.Z);
	}

	public override void _Process(double delta) {
		// Move the obstacle along the river (aka the Z axis)
		Position = new(Position.X, Position.Y, +Position.Z + (travelSpeed * (float)delta));
	}
}
