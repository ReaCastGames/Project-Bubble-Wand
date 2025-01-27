using Godot;
using System;

public partial class LookAtThing : Node3D {
	[Export] public Node3D target;

	public override void _Process(double delta) {
		LookAt(target.GlobalPosition, Vector3.Up, true);
	}
}
