using Godot;
using System;

public partial class GameScene : Node3D {

	public static RandomNumberGenerator randomNumberGenerator;

	public override void _Ready() {
		randomNumberGenerator = new();
	}

	public override void _Process(double delta) {
	}
}
