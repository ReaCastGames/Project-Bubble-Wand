using System;
using Godot;

public partial class MainMenu : Control {

	public Button PlayButton { get; set; } = default!;
	public Button QuitButton { get; set; } = default!;

	public Action? Play;

	public override void _Ready() {
		PlayButton = GetNode<Button>("%PlayButton");
		QuitButton = GetNode<Button>("%QuitButton");

		PlayButton.Pressed += OnPlay;
		QuitButton.Pressed += OnQuit;
	}

	public void OnPlay() => Play?.Invoke();

	public void OnQuit() {
		GetTree().Quit();
	}
}
