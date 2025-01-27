using Godot;

public partial class GameScene : Node3D {
	public static RandomNumberGenerator RandomNumberGenerator { get; set; }
		= new();

	public Hud HUD { get; set; } = default!;

	public const int HEALTH_DEFAULT = 5;

	public int Health { get; set; } = HEALTH_DEFAULT;
	public int Score { get; set; }

	public override void _Ready() {
		PlayerControls.OnHit += OnPlayerHit;
		PlayerControls.OnGainScore += OnPlayerGainScore;

		HUD = GetNode<Hud>("%Hud");

		HUD.Setup(Health);
	}

	public override void _ExitTree() {
		PlayerControls.OnHit -= OnPlayerHit;
		PlayerControls.OnGainScore -= OnPlayerGainScore;
	}
	public void OnPlayerHit() {
		Health--;
		GD.Print("Health: " + Health);
		HUD.LoseALife(Health);

		if (Health == 0) {
			Health = HEALTH_DEFAULT;
			SceneManager.EndGame();
		}
	}

	public void OnPlayerGainScore() {
		Score++;
		HUD.ShowScore(Score);
	}
}
