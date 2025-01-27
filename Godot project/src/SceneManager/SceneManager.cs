using System;
using Godot;

public partial class SceneManager : Control {
	public static event Action? GameOver;

	public PackedScene MainMenuScene = GD.Load<PackedScene>("res://src/MainMenu/MainMenu.tscn");
	public MainMenu MainMenu { get; set; } = default!;

	public PackedScene GameScene = GD.Load<PackedScene>("res://src/GameScene/GameScene.tscn");
	public GameScene? Game { get; set; } = default!;

	public SubViewport GameViewport { get; set; } = default!;

	public override void _Ready() {
		MainMenu = MainMenuScene.Instantiate<MainMenu>();

		GameViewport = GetNode<SubViewport>("%GameViewport");

		MainMenu.Play += StartGame;

		AddChild(MainMenu);

		GameOver += OnGameOver;
	}
	public void StartGame() {
		RemoveChild(MainMenu);

		Game = GameScene.Instantiate<GameScene>();
		GameViewport.AddChild(Game);
	}

	public static void EndGame() => GameOver?.Invoke();

	public void OnGameOver() {
		GD.Print("Game over!");
		GameViewport.RemoveChild(Game!);
		Game!.QueueFree();
		Game = null;

		AddChild(MainMenu);
	}
}
