using Godot;

public partial class Fish : Node3D {

	[Export] public PackedScene[] fishes = default!;

	public override void _Ready() {
		int randomFish = GameScene.RandomNumberGenerator.RandiRange(0, fishes.Length - 1);
		AddChild(fishes[randomFish].Instantiate());
	}

	public override void _Process(double delta) { }
}
