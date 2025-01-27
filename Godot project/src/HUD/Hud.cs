using Godot;

public partial class Hud : MarginContainer {

	public Lives Lives { get; set; } = default!;

	public Score Score { get; set; } = default!;

	public override void _Ready() {
		Lives = GetNode<Lives>("%Lives");
		Score = GetNode<Score>("%Score");
	}

	public void Setup(int numLives) {
		Lives.Setup(numLives);
	}

	public void LoseALife(int health) {
		Lives.Update(health);
	}

	public void ShowScore(int score) { }
}
