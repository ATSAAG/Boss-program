using Godot;
using System;

public partial class boss_2 : GroundedEnemy
{	
	//[Export] public PackedScene bullet;
	
	public float DistanceOfDetction = 250.0f;
	public new bool isAttacking = false;
	public Vector2 PlayerPos;
	
		
	public override void _Ready()
	{
		GD.Print("Boss2 ready");
		
		Speed = 100.0f;
		_rayCasts = new RayCast2D[2];
		_rayCasts[0] = GetNode<RayCast2D>("RayCast2D_boss2");
		_rayCasts[1] = GetNode<RayCast2D>("RayCast2D2_boss2");
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D_boss2");
		Health = 10;
		PlayerPos.X=0;	
		PlayerPos.Y=0;
				
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		
		if (isAttacking) 
		{
			if (GlobalPosition.X>PlayerPos.X)
			{
				GD.Print("Boss a droite, Parent: " + GetParent().Name);		
				
				
				var bulletScene = (PackedScene)ResourceLoader.Load("res://Enemies/bullet.tscn");
				
				if (bulletScene != null)
				{
					GD.Print("chuis dans le milliii");
					bullet Bullet = (bullet)bulletScene.Instantiate();
					//bullet.GetNode("RayCast2D").SetCollisionMaskBit(0, false); // Désactiver la collision avec le boss
					//bullet.Position = this.GlobalPosition;
					
					GetTree().Root.AddChild(Bullet);
													
					GD.Print("nom bullet = " + Bullet.Name);
				}
				else
				{
					GD.Print("Erreur: Impossible de charger la scène du missile");
				}
				
				
				
				//GD.Print(GetParent().GetNode("Bullet").Name);		
			}
			else
			{
				GD.Print("Boss a gauche");
				
			}
		}
		
		
	}
	public override Godot.Vector2 Move()
	{
		Vector2 velocity = Velocity;
		velocity.X = Speed;
		return velocity;
	}
	public override void CheckRaycasts()
	{
		if (_rayCasts[0].IsColliding())
		{
			GD.Print("CheckRaycasts right");
			Speed = -Mathf.Abs(Speed);
			_sprite.FlipH = false;
		}
		else if (_rayCasts[1].IsColliding())
		{
			GD.Print("CheckRaycasts left");
			Speed = Mathf.Abs(Speed);
			_sprite.FlipH = true;
		}
		
	}

	public override void HandleAnimations()
	{
		//GD.Print("HandleAnimations");
		_sprite.FlipH = Speed < 0;
		if (isAttacking) 
		{
			_sprite.Play("attack");
		}
		else
		{
			_sprite.Play("walk");
		}
			
	}
	
	private void _on_hitboxe_body_entered(Node2D body)
	{
		GD.Print("Hit");
		if (body is player)
		{	
			((player)body).TakeHit(5);
			GD.Print("Health of the player = " + $"{((player)body).Health}");			
		}
		//Explode();
	}
}

