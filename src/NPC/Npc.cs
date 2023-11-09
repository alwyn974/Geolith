using Godot;

namespace Geolith.NPC
{
    public partial class Npc : CharacterBody3D
    {
        public override void _Ready()
        {
            //TODO: This doesn't work because Mouse mode is captured
            InputRayPickable = true;
        }

        public void OnNpcBodyEntered(Node3D body)
        {
            if (body is Player.Player player)
            {
                GD.Print("Player entered");
            }
        }

        //TODO: This doesn't work because Mouse mode is captured
        // Find a way around this
        public void OnMouseEntered()
        {
            GD.Print("Player aim me");
        }

        public void OnNpcBodyExited(Node3D body)
        {
            if (body is Player.Player player)
            {
                GD.Print("Player exited");
            }
        }

        public override void _Input(InputEvent @event)
        {
            if (@event.IsActionPressed("interact"))
            {
                GD.Print("Accept");
            }
        }
    }
}
