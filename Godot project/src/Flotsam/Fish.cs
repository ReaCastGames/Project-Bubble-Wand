using Godot;
using System;
using System.Collections.Generic;

public partial class Fish : Node3D {

	[Export] public PackedScene[]? fishes;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		int randomFish = GameScene.randomNumberGenerator.RandiRange(0, 4);
		AddChild(fishes[randomFish].Instantiate());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {


	}
}
