using Godot;

public interface IDespawn;

public partial class Despawner : Area3D {
	public override void _EnterTree() {
		BodyEntered += OnBodyEntered;
	}

	public override void _ExitTree() {
		BodyEntered -= OnBodyEntered;
	}

	public void OnBodyEntered(Node3D body) {
		if (body is not IDespawn) {
			return;
		}

		var debugName = body.GetChildren().Count > 0
			? body.GetChild(0).Name
			: body.Name;
		GD.Print($"Despawning {debugName}");
		body.GetParent().RemoveChild(body);
		body.QueueFree();
	}
}
