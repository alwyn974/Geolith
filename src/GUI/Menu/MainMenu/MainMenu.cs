using Godot;

namespace Geolith.GUI.Menu.MainMenu
{
    [GlobalClass]
    public partial class MainMenu : Control
    {
        private VBoxContainer _mainMenu;
        private Settings _settingsMenu;

        public override void _Ready()
        {
            _mainMenu = GetNode<VBoxContainer>("Menu");
            _settingsMenu = GetNode<Settings>("Settings");
        }

        private void GoToGame()
        {
            GetTree().ChangeSceneToFile("res://scene/game.tscn");
        }

        private void GoToOptions()
        {
            _mainMenu.Visible = false;
            _settingsMenu.ToggleSettings();
        }

        private void QuitGame()
        {
            GetTree().Quit();
        }
    }
}
