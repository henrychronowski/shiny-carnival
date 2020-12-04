using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGenerator3D
{
    protected bool mShouldEffectAll = true;

    public ForceGenerator3D(bool effectAll)
    {
        mShouldEffectAll = effectAll;
    }

    public virtual void UpdateForce()
    {

    }

	public bool EffectAll()
    {
        return mShouldEffectAll;
    }
}

public class GravityForceGenerator : ForceGenerator3D
{
	double G;
	List<PhysicsObject3D> PhysicsObject3Ds;

	GravityForceGenerator(bool effectAll, in List<PhysicsObject3D> PhysicsObject3Ds, double G = 0.000000000066743) : base(effectAll)
	{
		this.PhysicsObject3Ds = PhysicsObject3Ds;
		this.G = G;
	}

	public override void UpdateForce()
	{
		int i, j;
		PhysicsObject3D lhs, rhs;

		for(i = 0; i < PhysicsObject3Ds.Count; i++)
		{
			lhs = PhysicsObject3Ds[i];
			for(j = i+1; j < PhysicsObject3Ds.Count; j++)
			{
				rhs = PhysicsObject3Ds[j];

				if (lhs == null || rhs == null || lhs == rhs)
					break;

				Vector3 pos1 = lhs.transform.position;
				Vector3 pos2 = rhs.transform.position;

				Vector3 diff = pos1 - pos2;
				float dist = diff.magnitude;

				float magnitude = (float)(G * lhs.GetMass() * rhs.GetMass()) / (dist * dist);

				diff.Normalize();
				diff *= magnitude;
				lhs.AddForce(diff);
				rhs.AddForce(-diff);
			}
		}
	}
}