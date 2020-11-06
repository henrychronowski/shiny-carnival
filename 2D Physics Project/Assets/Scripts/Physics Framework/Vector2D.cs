using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector2D
{
	public float x { get; set; }
	public float y { get; set; }

	public static Vector2D Zero()
	{
		return new Vector2D(0.0f, 0.0f);
	}
	public static Vector2D Down()
	{
		return new Vector2D(0.0f, -1.0f);
	}

	public static Vector2D Left()
	{
		return new Vector2D(-1.0f, 0.0f);
	}

	public static Vector2D Right()
	{
		return new Vector2D(1.0f, 0.0f);
	}

	public static Vector2D Up()
	{
		return new Vector2D(0.0f, 1.0f);
	}

	public Vector2D(float x, float y)
	{
		this.x = x;
		this.y = y;
	}

	public Vector2D(float x)
	{
		this.x = x;
		this.y = x;
	}
	public Vector2D(double x)
	{
		this.x = (float)x;
		this.y = (float)x;
	}

	public Vector2D(double x, double y)
	{
		this.x = (float)x;
		this.y = (float)y;
	}

	public Vector2D(float x, double y)
	{
		this.x = x;
		this.y = (float)y;
	}

	public Vector2D(double x, float y)
	{
		this.x = (float)x;
		this.y = y;
	}

	public float getMagnitude()
	{
		return Mathf.Sqrt(getSqrMagnitude());
	}

	public float getSqrMagnitude()
	{
		return (x * x + y * y);
	}

	public Vector2D getNormalized()
	{
		return new Vector2D(x / getMagnitude(), y / getMagnitude());
	}

	public bool Equals(Vector2D rhs)
	{
		return (this.x == rhs.x && this.y == rhs.x);
	}

	public void Normalize()
	{
		x /= getMagnitude();
		y /= getMagnitude();
	}

	public void Set(float x, float y)
	{
		this.x = x;
		this.y = y;
	}

	public override string ToString()
	{
		return x.ToString() + " " + y.ToString();
	}

	public static Vector2D operator +(Vector2D lhs, Vector2D rhs)
	{
		return new Vector2D(lhs.x + rhs.x, lhs.y + rhs.y);
	}

	public static Vector2D operator -(Vector2D lhs)
	{
		return new Vector2D(-lhs.x, -lhs.y);
	}

	public static Vector2D operator -(Vector2D lhs, Vector2D rhs)
	{
		return new Vector2D(lhs.x - rhs.x, lhs.y - rhs.y);
	}

	public static Vector2D operator *(float lhs, Vector2D rhs)
	{
		return new Vector2D(lhs * rhs.x, lhs * rhs.y);
	}

	public static Vector2D operator *(Vector2D lhs, float rhs)
	{
		return new Vector2D(lhs.x * rhs, lhs.y * rhs);
	}

	// Dot
	public static Vector2D operator *(Vector2D lhs, Vector2D rhs)
	{
		return new Vector2D(lhs.x * rhs.x, lhs.y * rhs.y);
	}

	public static Vector2D operator /(Vector2D lhs, float rhs)
	{
		return new Vector2D(lhs.x / rhs, lhs.y / rhs);
	}

	//public static Vector2D operator /(Vector2D lhs, Vector2D rhs);
	public static bool operator ==(Vector2D lhs, Vector2D rhs)
	{
		return (lhs.x == rhs.x && lhs.y == rhs.x);
	}

	public static bool operator !=(Vector2D lhs, Vector2D rhs)
	{
		return !(lhs.x == rhs.x && lhs.y == rhs.x);
	}

	public static implicit operator Vector2(Vector2D v)
	{
		return new Vector2(v.x, v.y);
	}

	public static implicit operator Vector2D(Vector2 v)
	{
		return new Vector2D(v.x, v.y);
	}
}