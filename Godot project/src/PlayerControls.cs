using Godot;
using System;

public partial class PlayerControls : CharacterBody3D {
	public static event Action? OnHit;
	public static event Action? OnGainScore;

	[Export] public float speed, acceleration, brakeMultiplier;

	public Vector3 rotation;

	public override void _Ready() {
		InputMap.LoadFromProjectSettings();
	}

	public override void _Notification(int what) {
		if (what == NotificationPredelete) {
			OnHit = null;
			OnGainScore = null;
		}
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
		Rotate(fDelta);
		MoveAndSlide();
	}

	private void Rotate(float delta) {
		// RandomNumberGenerator random = new();
		// rotation += new Vector3(
		// 	rotation.X + (random.Randf() * delta),
		// 	rotation.Y + (random.Randf() * delta),
		// 	rotation.Z + (random.Randf() * delta)
		// );
		rotation = new Vector3(Mathf.MoveToward(rotation.X, 0, delta), Mathf.MoveToward(rotation.Y, 0, delta),
			Mathf.MoveToward(rotation.Z, 0, delta));
		Rotation += rotation * delta;
	}

	public void GainScore() {
		OnGainScore?.Invoke();
	}

	public void TakeDamage() {
		OnHit?.Invoke();
	}
}
