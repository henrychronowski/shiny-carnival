using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DManager : MonoBehaviour
{
	public List<Particle2D> particles;
	private List<Particle2D> toBeDeleted;
	private void Awake()
	{
		particles = new List<Particle2D>();
		particles.Clear();
		toBeDeleted = new List<Particle2D>();
		toBeDeleted.Clear();
	}

	public void AddParticle(Particle2D particle)
	{
		particles.Add(particle);
	}

	public void DeleteParticle(Particle2D particle)
	{
		particles.Remove(particle);
	}

	public void Update()
	{
		foreach(var lhs in particles)
		{
			foreach (var rhs in particles)
			{
				if(lhs != rhs && (lhs != null && rhs != null))
				{
					if(CollisionDetector2D.DetectCollision(lhs, rhs))
					{
						if (!toBeDeleted.Contains(lhs))
							toBeDeleted.Add(lhs);
						if (!toBeDeleted.Contains(rhs))
							toBeDeleted.Add(rhs);
					}
				}
			}
		}

		foreach(var particle in toBeDeleted)
		{
			particles.Remove(particle);
			Destroy(particle.gameObject);
		}
		toBeDeleted.Clear();
	}
}
