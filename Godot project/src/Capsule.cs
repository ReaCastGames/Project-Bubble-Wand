using Godot;

public partial class Capsule : Node3D {

	public override void _Ready() {
	}

	public override void _Process(double delta) {
		RandomNumberGenerator random = new();
		float fDelta = (float)delta;
		Rotation = new(
			Rotation.X + (random.Randf() * fDelta),
			Rotation.Y + (random.Randf() * fDelta),
			Rotation.Z + (random.Randf() * fDelta)
		);
	}

}
