using Godot;
using System;

public partial class PlayerControls : CharacterBody2D {

  [Export]
  public float speed;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
    float horizontalDirection = 0;
    if (Input.IsKeyPressed(Key.Right)) {
      horizontalDirection += 1;
    }
    if (Input.IsKeyPressed(Key.Left)) {
      horizontalDirection -= 1;
    }

    Velocity = new Vector2(horizontalDirection * speed, 0);

    MoveAndSlide();
  }
}
