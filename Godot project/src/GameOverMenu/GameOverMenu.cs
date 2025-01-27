using Godot;

public partial class GameOverMenu : Control {
	public Button MainMenuButton { get; set; } = default!;

	public override void _Ready() {
		MainMenuButton = GetNode<Button>("%MainMenuButton");

		MainMenuButton.Pressed += OnMainMenu;
	}

	public void OnMainMenu() => SceneManager.GoToMainMenu();
}
