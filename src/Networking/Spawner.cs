using Godot;
using System;

namespace Geolith.Networking
{
    public partial class Spawner : Node3D
    {
        [Export] private PackedScene playerPrefab;

	    // Called when the node enters the scene tree for the first time.
	    public override void _Ready()
	    {
            Multiplayer.PeerConnected += ctx => AddPlayer();
	    }

        public void AddPlayer(int id=1)
        {
            var player = playerPrefab.Instantiate<CharacterBody3D>();
            player.Name = id.ToString();
            //player.Position = new Vector3(Position.X + 10, Position.Y, 0);
            AddChild(player);
            //CallDeferred(nameof(AddChild), player);
        }

	    // Called every frame. 'delta' is the elapsed time since the previous frame.
	    public override void _Process(double delta)
	    {
	    }
    }
}
