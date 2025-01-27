using System;
using Godot;

public partial class FlotsamItem : CharacterBody3D, IDespawn {
	[Export] public float riverWidth = 20;

	[Export] public float travelSpeed = 1;

	[Export] private bool sticky = false;
	private bool isFloating = true;
	private float hangTime = 5;
	private float hangTimer = 0;
	private float randomX = 1;
	private Vector3 rotationValue;

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
					FlyAway(collidedNode, fDelta);
				}
			}
		}
		else {
			var x = Position.X > 0 ? 1f : -1f;
			RandomNumberGenerator random = new();
			Velocity = new Vector3(x * 10 * fDelta * randomX , Mathf.Sin(hangTimer*2.5f) * fDelta * 20, -fDelta * 10);
			Rotation += rotationValue;
			MoveAndCollide(Velocity);
			hangTimer += fDelta;
			if(hangTimer >= hangTime)
				QueueFree();
		}
	}

	private void FlyAway(Node3D collidedNode, float fDelta) {
		isFloating = false;

		this.SetCollisionMaskValue(1, false);
		this.SetCollisionLayerValue(2, false);
		this.SetCollisionLayerValue(3, true);
		GD.Print("Fly Away! " + this.Name);

		RandomNumberGenerator random = new();
		randomX = random.RandfRange(0.5f, 2.5f);
		float range = 20;
		rotationValue = new Vector3(
			0,
			random.RandfRange(-range, range) * fDelta,
			random.RandfRange(-range, range) * fDelta
			);

		var controls = (PlayerControls)collidedNode;
		controls.TakeDamage();
	}

	private void Stick(Node3D collidedNode) {
		var relativePosition = GlobalPosition - collidedNode.GlobalPosition;

		GetParent()?.RemoveChild(this);

		// var children = this.GetChildren();
		Node toy = GetChild(-1);
		// foreach (var child in children) {
		// 	GD.Print("child: " + child.Name);
		// 	if (child.Name == "Toy") {
		// 		child.GetParent().RemoveChild(child);
		// 		toy = child;
		// 		break;
		// 	}
		// }

		Capsule? capsule = default;
		var children = GetChildren();
		foreach (var child in children) {
			if (child is Capsule) {
				capsule = child as Capsule;
				break;
			}
		}

		var propResource = GD.Load<PackedScene>("res://src/Flotsam/FlotsamItems/Capsule/Capsule_prop_parent.tscn");
		var prop = (Node3D)propResource.Instantiate();
		collidedNode.AddChild(prop);

		var cap = prop.GetChild(-1).GetChild(0) as MeshInstance3D;
		var material = (BaseMaterial3D)cap.GetSurfaceOverrideMaterial(0);
		material.AlbedoColor = capsule.capColor;
		cap.SetSurfaceOverrideMaterial(0, material);
		var collisionShape = prop.GetNode<CollisionShape3D>("CollisionShape3D");
		prop.GlobalPosition = collidedNode.GlobalPosition + relativePosition;
		toy.GetParent().RemoveChild(toy);
		prop.AddChild(toy);
		((Node3D)toy).Position = Vector3.Zero;
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
