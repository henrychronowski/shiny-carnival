using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2D : PhysicsObject2D
{
	[SerializeField]
	private float radius;
	private new void Update()
	{
		base.Update();
	}

	public float getRadius()
	{
		return radius;
	}
}
