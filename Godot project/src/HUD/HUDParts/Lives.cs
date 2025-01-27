using Godot;
public partial class Lives : HBoxContainer {

	public static PackedScene BubbleScene { get; } = GD.Load<PackedScene>("res://src/HUD/HUDParts/Props/Bubble.tscn");
	public TextureRect Bubble { get; set; } = default!;

	private int _numLives;

	public void Setup(int numLives) {
		_numLives = numLives;
		for (var i = 0; i < numLives; i++) {
			var bubble = BubbleScene.Instantiate<TextureRect>();
			AddChild(bubble);
		}
	}

	public void Update(int health) {
		_numLives = health;
		for (var i = 0; i < GetChildCount(); i++) {
			GetChild<TextureRect>(i).Visible = i < _numLives;
		}
	}
}
