using System;
using Godot;

public partial class Capsule : Node3D {

	[Export] public Node3D toyNode;
	[Export] public PackedScene[] itemMeshes;

	public override void _Ready() {
		var newToy = itemMeshes[(new RandomNumberGenerator()).RandiRange(0, itemMeshes.Length - 1)];
		var newToyNode = newToy.Instantiate();
		((Node3D)newToyNode).Position = toyNode.Position;
		var parent = GetParent();
		newToyNode.CallDeferred("name", "Toy");
		parent.CallDeferred("add_child", newToyNode);
		parent.CallDeferred("remove_child", toyNode);
		// parent.AddChild(newToyNode);
		// parent.RemoveChild(toyNode);
	}

	public override void _Process(double delta) {
		RandomNumberGenerator random = new();
		float fDelta = (float)delta;
		Rotation = new(
			Rotation.X + (random.Randf() * fDelta),
			Rotation.Y + (random.Randf() * fDelta),
			Rotation.Z + (random.Randf() * fDelta)
		);
	}

}
