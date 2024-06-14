using Godot;
using System;

public partial class missile : CharacterBody2D
{
	private const float MOVE_SPEED = 600;
	private const float STEER_FORCE = 1800;
	private const float ARC_HEIGHT = 100;
	private const int DAMAGE = 2;

	private Vector2 velocity = Vector2.Zero;
	private Vector2 acceleration = Vector2.Zero;
	private Node2D target;
	private Vector2? midPosition;

	public override void _PhysicsProcess(double delta)
	{
		acceleration = Seek();
		velocity += acceleration * (float)delta;
		velocity = velocity.LimitLength(MOVE_SPEED);
		Rotation = velocity.Angle();
		Position += velocity * (float)delta;

		if (midPosition.HasValue && Position.DistanceTo(midPosition.Value) < 5.0f)
		{
			midPosition = null;
		}
	}

	public void Launch(Node2D target)
	{
		this.target = target;
		if (target != null)
		{
			midPosition = (Position + target.Position) / 2;
			midPosition += Vector2.Down * ARC_HEIGHT;
		}
	}

	public void _OnProjectileHomingBodyEntered(Node2D body)
	{
		if (body is player)
		{
			((player)body).QueueFree();
		}

		QueueFree();
	}

	private Vector2 Seek()
	{
		Vector2 steer = Vector2.Up;
		if (midPosition.HasValue)
		{
			Vector2 desired = (midPosition.Value - Position).Normalized() * MOVE_SPEED;
			steer = (desired - velocity).Normalized() * STEER_FORCE;
		}
		else if (target != null)
		{
			Vector2 desired = (target.Position - Position).Normalized() * MOVE_SPEED;
			steer = (desired - velocity).Normalized() * STEER_FORCE;
		}
		return steer;
	}
}


