using System;
using Godot;

public partial class Junk : CharacterBody3D {
	private double sinOffset;

	public override void _Ready() {
		base._Ready();
		var velocity = Vector3.Zero;
		velocity.Z = 1;
		Velocity = velocity;
		sinOffset = Random.Shared.NextDouble();
	}

	public override void _PhysicsProcess(double delta) {
		var velocity = new Vector3(0, 0, (float)(5 * delta));
		var thisNode = (Node3D)this;
		thisNode.Position = new Vector3(
			thisNode.Position.X,
			(float)Math.Sin(thisNode.Position.Z + sinOffset),
			thisNode.Position.Z);

		var collision = MoveAndCollide(velocity);

		if (collision != null) {
			var collidedNode = (Node3D)collision.GetCollider();
			var relativePosition = thisNode.GlobalPosition - collidedNode.GlobalPosition;

			if (this.GetParent() != null) {
				this.GetParent().RemoveChild(this);
			}

			var propResource = GD.Load<PackedScene>("res://src/junk_prop.tscn");
			var prop = (Node3D)propResource.Instantiate();
			collidedNode.AddChild(prop);
			var collisionShape = prop.GetNode<CollisionShape3D>("CollisionShape3D");
			prop.GlobalPosition = collidedNode.GlobalPosition + relativePosition;
			prop.RemoveChild(collisionShape);
			collidedNode.AddChild(collisionShape);
			collisionShape.Position = collidedNode.GlobalPosition + relativePosition;

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
			var controls = collidedNode as PlayerControls;
			controls.rotation += rotation * (1 - Vector3.Forward.Dot(relativePosition) + 5);

			thisNode.QueueFree();
		}
	}
}
