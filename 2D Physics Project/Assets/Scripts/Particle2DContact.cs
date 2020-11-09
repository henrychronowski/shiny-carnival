using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DContact
{
	public PhysicsObject2D lhs { get; private set; }
	public PhysicsObject2D rhs { get; private set; }
	public float restitutionCoefficient { get; private set; }
	public float penetration { get; set; }
	public Vector2 contactNormal { get; private set; }
	public Vector2 move1 { get; private set; }
	public Vector2 move2 { get; private set; }

	public Particle2DContact(PhysicsObject2D lhs, PhysicsObject2D rhs, float restitutionCoefficient, Vector2 contactNormal, float penetration, Vector2 move1, Vector2 move2)
	{
		this.lhs = lhs;
		this.rhs = rhs;
		this.restitutionCoefficient = restitutionCoefficient;
		this.contactNormal = contactNormal;
		this.penetration = penetration;
		this.move1 = move1;
		this.move2 = move2;
	}

	public void resolve(float dt)
	{
		resolveVelocity(dt);
		resolveInterpenetration(dt);
	}

	public float calculateSeparatingVelocity()
	{
		Vector2 relativeVel = lhs.GetVel();
			if(rhs)
				relativeVel -= rhs.GetVel();
		return Vector2.Dot(relativeVel, contactNormal);
	}

	void resolveVelocity(float dt)
	{
		float separatingVel = calculateSeparatingVelocity();
		if (separatingVel > 0.0f)
			return;

		float newSepVel = -separatingVel * restitutionCoefficient;

		Vector2 velFromAcc = lhs.GetAcc();
		if (rhs)
			velFromAcc -= rhs.GetAcc();
		float accCausedSepVelocity = Vector2.Dot(velFromAcc, contactNormal) * dt;

		if(accCausedSepVelocity < 0.0f)
		{
			newSepVel += restitutionCoefficient * accCausedSepVelocity;
			if (newSepVel < 0.0f)
				newSepVel = 0.0f;
		}

		float deltaVel = newSepVel - separatingVel;

		float totalInverseMass = lhs.GetIMass();
		if (rhs)
			totalInverseMass += rhs.GetIMass();
		if (totalInverseMass <= 0)
			return;

		float impulse = deltaVel / totalInverseMass;
		Vector2 impulsePerIMass = contactNormal * impulse;

		Vector2 newVelocity = lhs.GetVel() + impulsePerIMass * lhs.GetIMass();
		lhs.SetVel(newVelocity);
		if(rhs)
		{
			newVelocity = rhs.GetVel() + impulsePerIMass * rhs.GetIMass();
			rhs.SetVel(newVelocity);
		}
	}

	void resolveInterpenetration(float dt)
	{
		if (penetration <= 0.0f)
			return;

		float totalInverseMass = lhs.GetIMass();
		if (rhs)
			totalInverseMass += rhs.GetIMass();
		if (totalInverseMass <= 0)
			return;

		Vector2 movePerIMass = contactNormal * (penetration / totalInverseMass);
		move1 = movePerIMass * lhs.GetIMass();
		if (rhs)
			move2 = movePerIMass * (-rhs.GetIMass());
		else
			move2 = Vector2.zero;

		lhs.SetPos(lhs.GetPos() + move1);
		if (rhs)
			rhs.SetPos(rhs.GetPos() + move2);
	}
}
