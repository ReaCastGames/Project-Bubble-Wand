using Godot;
using System;

public partial class PlayerControls : CharacterBody3D {

	[Export]
	public float speed, acceleration, brakeMultiplier;

	public override void _Ready() {
		InputMap.LoadFromProjectSettings();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame
	public override void _Process(double delta) {
		float fDelta = (float)delta;
		float horizontalDirection = Input.GetAxis("moveLeft", "moveRight");

		if (Input.IsActionJustPressed("jump")) {
			// Jump.
		}

		if (Input.IsActionJustPressed("charge")) {
			// charge or some other second action
		}

		if (Input.IsActionJustPressed("closeGame")) {
			GetTree().Quit();
		}

		var horizontalVelocity = Velocity.X;

		if (horizontalDirection == 0) {
			horizontalVelocity = Mathf.MoveToward(horizontalVelocity, 0, acceleration * fDelta);
		}
		else {
			var addition = horizontalDirection * acceleration * fDelta;
			if ((horizontalDirection > 0 && horizontalVelocity < 0) ||
				(horizontalDirection < 0 && horizontalVelocity > 0)) {
				addition *= brakeMultiplier;
			}

			horizontalVelocity += addition;
			horizontalVelocity = Math.Clamp(horizontalVelocity, -speed, speed);
		}

		Velocity = new Vector3(horizontalVelocity, 0, speed);

		MoveAndSlide();
	}
}
