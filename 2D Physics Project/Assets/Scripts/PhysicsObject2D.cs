using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject2D : MonoBehaviour
{
	protected Vector2 mPos;
	protected Vector2 mVel;
	protected Vector2 mAcc;
	protected Vector2 mAccumulatedForces;
	protected float mFacing;
	protected float mRotVel;
	protected float mRotAcc;
	protected float mDampingConstant;
	protected float mInverseMass;
	protected bool mShouldIgnoreForces;

    void Start()
    {
		// Get singleton integrator
    }

    // Update is called once per frame
    void Update()
    {
        
		// Call integrator/pass data (maybe a data struct would actually be usefull)
		// Or actually having an integrate function here that the integrator calls, conversely
    }

	void clearAccumulatedForces()
	{
		mAccumulatedForces = new Vector2(0.0f, 0.0f);
	}

	void Integrate( double dt )
	{
		mPos += mVel * (float)dt;

		Vector2 resultingAcc = mAcc;

		if(!mShouldIgnoreForces)
		{
			resultingAcc += mAccumulatedForces * mInverseMass;
		}

		mVel += resultingAcc * (float)dt;
		float damping = Mathf.Pow(mDampingConstant, (float)dt);
		mVel *= damping;
	}
}
