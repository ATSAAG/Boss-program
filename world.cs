using Godot;
using System;

public partial class main : Node2D
{
	private PackedScene missileScene;
	public player playerInstance;
	
	private double timeElapsed;
	private double missileInterval = 2.0; // Intervalle de temps en secondes entre les lancements de missiles


	public override void _Ready()
	{
		missileScene = (PackedScene)GD.Load("res://missile.tscn");
		player player = GetNode<player>("player");
		timeElapsed = 0.0;
	}

	public override void _PhysicsProcess(double delta)
	{
		timeElapsed += delta;

		if (timeElapsed >= missileInterval)
		{
			LaunchMissile();
			timeElapsed = 0.0; // Réinitialise le compteur de temps
		}
	}

	private void LaunchMissile()
	{
		if (playerInstance != null)
		{
			var projectile = (missile)missileScene.Instantiate();
			AddChild(projectile);
			projectile.Position = new Vector2((float)GD.RandRange(0, GetViewportRect().Size.X), 0);
			projectile.Launch(playerInstance);
		}
	}
}
