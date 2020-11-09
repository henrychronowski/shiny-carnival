using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject2D : MonoBehaviour
{
	[SerializeField]
	protected Vector2 mPos;
	[SerializeField]
	protected Vector2 mVel;
	[SerializeField]
	protected Vector2 mAcc;
	protected Vector2 mAccumulatedForces;
	[SerializeField]
	protected float mFacing;
	[SerializeField]
	protected float mRotVel;
	[SerializeField]
	protected float mRotAcc;
	[SerializeField]
	protected float mDampingConstant;
	[SerializeField]
	protected float mInverseMass;
	[SerializeField]
	protected bool mShouldIgnoreForces;

    void Start()
    {
		mPos = transform.position;
    }

    // Update is called once per frame
    protected void Update()
    {

		// Call integrator/pass data (maybe a data struct would actually be usefull)
		// Or actually having an integrate function here that the integrator calls, conversely

		//Integrate(Time.deltaTime);
		transform.SetPositionAndRotation(mPos, Quaternion.Euler(0.0f, 0.0f, 0.0f));
	}

	void ClearAccumulatedForces()
	{
		mAccumulatedForces = new Vector2(0.0f, 0.0f);
	}

	public void Integrate( double dt )
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

		ClearAccumulatedForces();
	}

	public void AddForce(Vector2 force)
    {
		mAccumulatedForces += force;
    }
	
	public void SetPos(Vector2 pos)
	{
		mPos = pos;
	}
	
	public void SetVel(Vector2 vel)
	{
		mVel = vel;
	}

	public void SetAcc(Vector2 acc)
    {
		mAcc = acc;
    }

	public void SetInverseMass(float mass)
    {
		mInverseMass = 1.0f / mass;
    }

	public void SetDamping(float damping)
    {
		mDampingConstant = damping;
    }

	public Vector2 GetVel()
	{
		return mVel;
	}

	public Vector2 GetAcc()
	{
		return mAcc;
	}

	public float GetIMass()
	{
		return mInverseMass;
	}

	public Vector2 GetPos()
	{
		return mPos;
	}

}
