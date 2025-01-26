
using Godot;

public partial class SceneManager : Control {

	public PackedScene MainMenuScene = GD.Load<PackedScene>("res://src/MainMenu/MainMenu.tscn");
	public MainMenu MainMenu { get; set; } = default!;

	public PackedScene GameScene = GD.Load<PackedScene>("res://src/GameScene/GameScene.tscn");
	public GameScene Game { get; set; } = default!;

	public SubViewport GameViewport { get; set; } = default!;

	public override void _Ready() {
		MainMenu = MainMenuScene.Instantiate<MainMenu>();
		Game = GameScene.Instantiate<GameScene>();
		GameViewport = GetNode<SubViewport>("%GameViewport");

		MainMenu.Play += StartGame;

		AddChild(MainMenu);
	}
	public void StartGame() {
		RemoveChild(MainMenu);

		GameViewport.AddChild(Game);
	}

	public void EndGame() {
		GameViewport.RemoveChild(Game);

		AddChild(MainMenu);
	}
}
