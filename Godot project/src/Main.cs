//-:cnd:noEmit
namespace Chickensoft.GodotGame;

using Godot;

#if DEBUG
using System.Reflection;
using Chickensoft.GoDotTest;
using Chickensoft.GameTools.Environment;
#endif

// This entry-point file is responsible for determining if we should run tests.
//
// If you want to edit your game's main entry-point, please see Game.tscn and
// Game.cs instead.

public partial class Main : Node2D {
#if DEBUG
	public TestEnvironment Environment = default!;

	public const float THEME_SCALE = 4f;
#endif

	public override void _Ready() {
#if DEBUG
		// If this is a debug build, use GoDotTest to examine the
		// command line arguments and determine if we should run tests.
		Environment = TestEnvironment.From(OS.GetCmdlineArgs());
		if (Environment.ShouldRunTests) {
			CallDeferred("RunTests");
			return;
		}
#endif

		// If we don't need to run tests, we can just switch to the game scene.
		CallDeferred("RunScene");
	}

#if DEBUG
	private void RunTests()
	  => _ = GoTest.RunTests(Assembly.GetExecutingAssembly(), this, Environment);
#endif

	private void RunScene() {
		var window = GetWindow();
		var scaleInfo = Display.GetWindowDpiScaleInfo(window, themeScale: 3);
		window.ContentScaleFactor = scaleInfo.ContentScaleFactor;
		GetTree().ChangeSceneToFile("res://src/SceneManager/SceneManager.tscn");
	}
}
//+:cnd:noEmit
