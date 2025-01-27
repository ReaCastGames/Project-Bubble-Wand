using Godot;

public partial class GameScene : Node3D {
	public static RandomNumberGenerator RandomNumberGenerator { get; set; }
		= new();

	public Hud HUD { get; set; } = default!;

	public const int HEALTH_DEFAULT = 5;

	public int Health { get; set; } = HEALTH_DEFAULT;

	[Export] public AudioStreamPlayer audioStreamPlayerSFX = default!;
	[Export] public AudioStreamPlayer audioStreamPlayerMusic = default!;
	[Export] public AudioStreamOggVorbis gameOverSFX = default!;
	[Export] public AudioStreamOggVorbis capsuleAttachSFX = default!;
	[Export] public AudioStreamOggVorbis bubblePopSFX = default!;

	[Export] private AudioStreamPlayer flotsamCollideAudioPlayer = default!;
	[Export] private AudioStreamOggVorbis[] audioFiles = default!;

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

		PlayCollisionAudio();

		if (Health == 0) {
			Health = HEALTH_DEFAULT;
			SceneManager.EndGame();
			audioStreamPlayerMusic.Stop();
			audioStreamPlayerSFX.Stream = gameOverSFX;
			audioStreamPlayerSFX.Play();
		}
	}

	public void OnPlayerGainScore() {
		audioStreamPlayerSFX.Stream = capsuleAttachSFX;
		audioStreamPlayerSFX.Play();
	}

	private void PlayCollisionAudio() {
		flotsamCollideAudioPlayer.Stream = audioFiles[GameScene.RandomNumberGenerator.RandiRange(0, audioFiles.Length - 1)];
		flotsamCollideAudioPlayer.Play();
	}
}
