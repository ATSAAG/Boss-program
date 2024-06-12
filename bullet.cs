using Godot;
using System;

public partial class bullet : GroundedEnemy
{
	private RayCast2D[] _rayCasts;
	private float speed = 300.0f;
	public int Direction = -1; // aller a gauche
	private bool launched = false;
	
	// player
	// public Camera2D camera =  new Camera2D();
	// AddChild(camera);
	
	// multijoueur
	// var scene = ResourceLoader.Load<PackedScene>("res://world.tscn").Instantiate<Node2D>();
	// GetTree().Root.AddChild(scene);
	
	public override void _Ready()
	{
		GD.Print("Missile creee, parent name: " +GetParent().GetNode("World").Name);
		_rayCasts = new RayCast2D[1];
		_rayCasts[0] = GetNode<RayCast2D>("RayCast2D");
		//SetAsToplevel(true);
		boss_2 boss_testee = GetParent().GetNode("World").GetNode<boss_2>("boss_2");
		GD.Print("boss testé" + boss_testee.Name);
		GlobalPosition = new Vector2(boss_testee.GlobalPosition.X, boss_testee.GlobalPosition.Y);
		
	}
	
	public override void _PhysicsProcess(double delta)
	{
		GD.Print("Missile process");
	}


	private void _body_entered(CharacterBody2D body)
	{
		if (body is player)
		{
			//player body = (player) body.Health -= 10.0f;
			//body.Health -= 10.0f;
			player bodyUn = (player) _rayCasts[0].GetCollider();
			bodyUn.TakeHit(5);
			QueueFree();
		}
	}
	 public override Vector2 Move()
	{
		Vector2 velocity = Velocity;
		velocity.X = Speed * Direction;
		
		return velocity;
	}
	
	private void _on_visible_on_screen_enabler_2d_screen_exited()
	{
		QueueFree();
	}
	
	public override void CheckRaycasts()
	{
		if (_rayCasts[0].IsColliding())
		{
			QueueFree();
		}
	}
	
	public override void HandleAnimations()
	{
		
		_sprite.Play("default");
		
	}
	
	public void ChangePos(float X, float Y)
	{
		if (!launched)
		{
			this.Visible = true;
			launched = true;
			//this.GlobalPosition.X = X;
			//this.GlobalPosition.Y = Y;
		}
	}
}
