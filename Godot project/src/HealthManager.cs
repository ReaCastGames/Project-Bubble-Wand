using Godot;

public partial class HealthManager : Node {
	private int health = 5;
	private Label healthLabel = default!;

	public override void _Ready() {
		healthLabel = GetNode<Label>("Canvas/HealthLabel");
		healthLabel.Text = "Health: 5";
		PlayerControls.OnHit += DecrementHealth;
	}

	public void DecrementHealth() {
		health--;
		healthLabel.Text = "Health: " + health;
		if (health <= 0) {
			healthLabel.Text += "\nGAME OVER";
		}
	}
}
