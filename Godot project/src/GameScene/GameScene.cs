using Godot;

public partial class GameScene : Node3D {
	public static RandomNumberGenerator RandomNumberGenerator { get; set; }
		= new();

	public Hud HUD { get; set; } = default!;

	public const int HEALTH_DEFAULT = 5;

	public int Health { get; set; } = HEALTH_DEFAULT;
	public int Score { get; set; }

	[Export] public AudioStreamPlayer audioStreamPlayerSFX;
	[Export] public AudioStreamPlayer audioStreamPlayerMusic;
	[Export] public AudioStreamOggVorbis gameOverSFX;
	[Export] public AudioStreamOggVorbis capsuleAttachSFX;
	[Export] public AudioStreamOggVorbis bubblePopSFX;

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
			audioStreamPlayerMusic.Stop();
			audioStreamPlayerSFX.Stream = gameOverSFX;
			audioStreamPlayerSFX.Play();
		}
	}

	public void OnPlayerGainScore() {
		Score++;
		HUD.ShowScore(Score);
		audioStreamPlayerSFX.Stream = capsuleAttachSFX;
		audioStreamPlayerSFX.Play();
	}
}
