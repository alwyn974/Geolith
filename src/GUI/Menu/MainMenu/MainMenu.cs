using Godot;
namespace Geolith.GUI.Menu.MainMenu;

// ECOUTE PAS HUGO STOP METTRE DES UNDERSORE

[GlobalClass]
public partial class MainMenu: Control
{

    public override void _Ready()
    {
        var Menu = GetNode("Menu");
        var Options = GetNode("0ptions");
        var Video = GetNode("video");
        GetNode<TextureButton>("Play_Btn").GrabFocus();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_cancel"))
            _toggle();
    }

    private void _toggle()
    {
        Visible = !Visible;
        GetTree().Paused = Visible;
    }
    private void _goToGame()
    {
        GetTree().ChangeSceneToFile("res://scene/game.tscn");
    }

    private void _goToOptions()
    {
        //_showAndHide(Options, Menu);
    }

    public void _showAndHide(Node first, Node second)
    {
        //first.Show();
        //second.Hide();
    }
    private void _quitGame()
    {
        GetTree().Quit();
    }
}
