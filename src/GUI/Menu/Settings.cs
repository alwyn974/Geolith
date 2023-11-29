using Godot;

namespace Geolith.GUI.Menu
{
    public partial class Settings : Control
    {
        private Control _audioMenu;
        private Control _videoMenu;
        private VBoxContainer _settingsMenu;

        public override void _Ready()
        {
            _audioMenu = GetNode<Control>("Audio");
            _videoMenu = GetNode<Control>("Video");
            _settingsMenu = GetNode<VBoxContainer>("SettingsMenu");
        }

        public void ToggleSettings()
        {
            Visible = !Visible;
        }

        private void OnAudioPressed()
        {
            _audioMenu.Visible = true;
            _videoMenu.Visible = false;
            _settingsMenu.Visible = false;
        }

        private void OnVideoPressed()
        {
            _audioMenu.Visible = false;
            _settingsMenu.Visible = false;
            _videoMenu.Visible = true;
        }

        private void GoBackToSettingsMenu()
        {
            _audioMenu.Visible = false;
            _videoMenu.Visible = false;
            _settingsMenu.Visible = true;
        }

        private void OnBackButtonPressed()
        {
            if (_audioMenu.Visible || _videoMenu.Visible) GoBackToSettingsMenu();
            else ToggleSettings();
        }
    }
}
