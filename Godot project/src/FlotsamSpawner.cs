using Godot;
using System;

public partial class FlotsamSpawner : Node3D {

	[Export]
	private Timer timer;

	[Export] private PackedScene obstacleScene;
	[Export] private PackedScene capsuleScene;

	[Export] private float spawnDelay = 0.5f; // This is how long to wait until spawning a new item.
	[Export] private float flotsamVsCapsuleRatio = 0; // 0 is 100% flotsam, 1 is 100% capsules

	public override void _Ready() {
		timer = GetNode<Timer>("Timer");
		timer.WaitTime = spawnDelay;
		timer.Timeout += SpawnFlotsam;
	}

	private void SpawnFlotsam() {
		///
		AddChild(obstacleScene.Instantiate());
	}

}
