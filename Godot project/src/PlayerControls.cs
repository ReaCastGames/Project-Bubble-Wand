using Godot;
using System;

public partial class PlayerControls : CharacterBody2D {

	[Export]
	public float speed;

	public override void _Ready() {
		InputMap.LoadFromProjectSettings();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame
	public override void _Process(double delta) {

		float horizontalDirection = Input.GetAxis("moveLeft", "moveRight");
		Velocity = new Vector2(horizontalDirection * speed, 0);

		if (Input.IsActionJustPressed("jump")) {
			// Jump.
		}

		if (Input.IsActionJustPressed("charge")) {
			// charge or some other second action
		}

		if (Input.IsActionJustPressed("closeGame")) {
			GetTree().Quit();
		}

		MoveAndSlide();
	}
}
