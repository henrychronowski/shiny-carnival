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

	public virtual void UpdateForce(PhysicsObject3D lhs, PhysicsObject3D rhs)
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

	GravityForceGenerator(bool effectAll, double G = 0.000000000066743) : base(effectAll)
	{
		this.G = G;
	}

	public override void UpdateForce(PhysicsObject3D lhs, PhysicsObject3D rhs)
	{
		if (lhs == rhs)
			return;

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

public class SpringForceGenerator : ForceGenerator2D
{
    PhysicsObject2D mObj1, mObj2;
    float mSpringConstant;
    float mRestLength;

    public SpringForceGenerator(PhysicsObject2D obj1, PhysicsObject2D obj2, float springConstant, float restLength, bool effectAll) : base(effectAll)
    {
        mObj1 = obj1;
        mObj2 = obj2;
        mSpringConstant = springConstant;
        mRestLength = restLength;
    }

    public override void UpdateForce()
    {
        if (mObj1 == null || mObj2 == null)
            return;

        Vector2 pos1 = mObj1.transform.position;
        Vector2 pos2 = mObj2.transform.position;

        Vector2 diff = pos1 - pos2;
        float dist = diff.magnitude;

        float magnitude = dist - mRestLength;
        magnitude *= mSpringConstant;

        diff = diff.normalized;
        diff *= magnitude;

        mObj1.AddForce(diff);
        mObj2.AddForce(-diff);
    }
}

public class BouyancyForceGenerator : ForceGenerator2D
{
    PhysicsObject2D mObj;
    float mMaxDepth;
    float mVolume;
    float mWaterHeight;
    float mDensity;

    public BouyancyForceGenerator(bool effectAll, PhysicsObject2D obj, float depth, float volume, float waterHeight, float density = 0.5f) : base(effectAll)
    {
        mObj = obj;
        mMaxDepth = depth;
        mVolume = volume;
        mWaterHeight = waterHeight;
        mDensity = density;
    }

    public override void UpdateForce()
    {
		if (mObj == null)
			return;

        float currentDepth = mObj.transform.position.y;
        Vector2 bouyancyForce = Vector2.zero;

        if(currentDepth <= mWaterHeight)
        {
            if(currentDepth <= mMaxDepth)
            {
                bouyancyForce.y = mDensity * mVolume;
                mObj.AddForce(bouyancyForce);
            }
            else
            {
                bouyancyForce.y = mDensity * mVolume * (-(currentDepth - mMaxDepth - mWaterHeight)) / (2 * mMaxDepth);
                mObj.AddForce(bouyancyForce);
            }
        }
    }
}
