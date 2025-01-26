using System;
using Godot;

public partial class FlotsamItem : CharacterBody3D, IDespawn {
	[Export] public float riverWidth = 20;

	[Export] public float travelSpeed = 1;

	[Export] private bool sticky = false;
	private bool isFloating = true;
	private float hangTime = 5;
	private float hangTimer = 0;

	public override void _Ready() {
		// Start from a random X position;
		Position = new(Position.X + (GameScene.RandomNumberGenerator.RandfRange(-1, 1) * (riverWidth / 2)), Position.Y,
			Position.Z);
	}

	public override void _Process(double delta) {
		// Move the obstacle along the river (aka the Z axis)
		float fDelta = (float)delta;
		if (isFloating) {
			Velocity = new(0, 0, (float)(travelSpeed * delta));
			var collision = MoveAndCollide(Velocity);

			if (collision != null) {
				var collidedNode = (Node3D)collision.GetCollider();
				if (sticky) {
					Stick(collidedNode);
				}
				else {
					FlyAway(collidedNode);
				}
			}
		}
		else {
			var x = Position.X > 0 ? 1f : -1f;
			Velocity = new Vector3(x * 10 * fDelta, Mathf.Sin(hangTimer / 10f), -fDelta * 10);
			RandomNumberGenerator random = new();
			Rotation = new(
				Rotation.X + (random.Randf() * fDelta),
				Rotation.Y + (random.Randf() * fDelta),
				Rotation.Z + (random.Randf() * fDelta)
			);
			MoveAndCollide(Velocity);
			hangTimer += fDelta;
		}
	}

	private void FlyAway(Node3D collidedNode) {
		isFloating = false;

		this.SetCollisionMaskValue(1, false);
		this.SetCollisionLayerValue(2, false);
		this.SetCollisionLayerValue(3, true);
		GD.Print("Fly Away! " + this.Name);

		var controls = (PlayerControls)collidedNode;
		controls.TakeDamage();
	}

	private void Stick(Node3D collidedNode) {
		var relativePosition = GlobalPosition - collidedNode.GlobalPosition;

		GetParent()?.RemoveChild(this);

		var propResource = GD.Load<PackedScene>("res://src/Flotsam/FlotsamItems/Capsule/Capsule_prop_parent.tscn");
		//var propResource = GD.Load<PackedScene>("res://src/test_junk/junk_prop.tscn");
		var prop = (Node3D)propResource.Instantiate();
		collidedNode.AddChild(prop);
		var collisionShape = prop.GetNode<CollisionShape3D>("CollisionShape3D");
		prop.GlobalPosition = collidedNode.GlobalPosition + relativePosition;
		prop.RemoveChild(collisionShape);
		collidedNode.AddChild(collisionShape);
		collisionShape.Position = prop.Position;

		Vector3 rotation = Vector3.Zero;
		// use relative position to determine "rotation"
		// if it's up, positive x rotation
		if (relativePosition.Y > 0) {
			rotation.X = 1;
		}
		// if it's down, negative x rotation
		else if (relativePosition.Y < 0) {
			rotation.X = -1;
		}

		// if it's right, negative y rotation
		if (relativePosition.X > 0) {
			rotation.Y = -1;
		}
		// if it's left, positive y rotation
		else if (relativePosition.X < 0) {
			rotation.Y = 1;
		}

		// apply rotation
		var controls = (PlayerControls)collidedNode;
		controls.rotation += rotation * (1 - Vector3.Forward.Dot(relativePosition) + 5);
		controls.GainScore();

		QueueFree();
	}
}
