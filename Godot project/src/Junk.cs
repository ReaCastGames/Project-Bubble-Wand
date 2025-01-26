using Godot;

public partial class Junk : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	public override void _Ready() {
		base._Ready();
		var velocity = Vector3.Zero;
		velocity.Z = 1;
		Velocity = velocity;
	}

	public override void _PhysicsProcess(double delta) {
		var velocity = new Vector3(0, 0, (float)(5 * delta));
		//Velocity = velocity;
		var collision = MoveAndCollide(velocity);
		if (collision != null) {
			//GD.Print(((Node)collision.GetCollider()).Name);
			var collidedNode = (Node3D)collision.GetCollider();
			var thisNode = (Node3D)this;
			var collNodePosition = collidedNode.GlobalPosition;
			var thisNodePosition = thisNode.GlobalPosition;
			GD.Print(thisNodePosition - collNodePosition);
			if (this.GetParent() != null) {
				this.GetParent().RemoveChild(this);
			}
			var propResource = GD.Load<PackedScene>("res://src/junk_prop.tscn");
			var prop = (Node3D)propResource.Instantiate();
			thisNode.QueueFree();
			collidedNode.AddChild(prop);
			prop.Position = thisNodePosition - collNodePosition;
		}
	}
}
