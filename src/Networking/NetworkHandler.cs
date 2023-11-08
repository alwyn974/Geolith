using Godot;

namespace Geolith.Networking
{
	public partial class NetworkHandler : Node2D
	{
		[Export] private int _port = 15;
		[Export] private string _ip = "0.0.0.0";
		
		[Export] private Spawner _spawner;
		
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			
		}

		// Handling client connection
		public void _on_join_pressed()
		{
			ENetMultiplayerPeer client = new ENetMultiplayerPeer();
			client.CreateClient(_ip, _port);

			if (client.GetConnectionStatus() ==
				MultiplayerPeer.ConnectionStatus.Disconnected)
			{
				GD.PrintErr("[CLIENT] ERROR cannot connect to server !");
				return;
			}

			Multiplayer.MultiplayerPeer = client;
			GD.Print("Successfully connected to the server !");

			// Start instance
			Hide();
			_spawner.AddPlayer(Multiplayer.GetUniqueId());
		}

		// Handling server creation
		public void _on_host_pressed()
		{
			ENetMultiplayerPeer server = new ENetMultiplayerPeer();
			server.CreateServer(_port);

			if (server.GetConnectionStatus() == 
				MultiplayerPeer.ConnectionStatus.Disconnected)
			{
				GD.PrintErr("[SERVER] ERROR cannot host the server");
				return;
			}
			GD.Print("Server successfully established !");
			Multiplayer.MultiplayerPeer = server;
			// Start instance
			Hide();
			_spawner.AddPlayer();
		}
	}
}
