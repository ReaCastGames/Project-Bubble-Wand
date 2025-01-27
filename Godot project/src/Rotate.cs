using Godot;
using System;

public partial class Rotate : Node3D {
	[Export] public Vector3 rotationAxis;
	[Export] public float rotationAngle;

	public override void _Process(double delta) {
		Rotate(rotationAxis, rotationAngle * (float)delta);
	}
}
