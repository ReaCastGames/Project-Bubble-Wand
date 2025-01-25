using Godot;
using System;

public partial class ObstacleSpawner : Node3D {

	[Export]
	private Timer timer;

	public override void _Ready() {
		timer = GetNode<Timer>("Timer");
		timer.Timeout += SpawnObstacle;
	}

	private void SpawnObstacle() {
		PackedScene obstacle = GD.Load<PackedScene>("res://src/Obstacle.tscn");
		Node3D node = obstacle.Instantiate<Node3D>();
		AddChild(node);
		GD.Print("Spawn!");
	}

}
