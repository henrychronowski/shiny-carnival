using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject3D : MonoBehaviour
{
    [SerializeField]
    protected Vector3 mPos;
    [SerializeField]
    protected Vector3 mVel;
    [SerializeField]
    protected Vector3 mAcc;
    protected Vector3 mAccumulatedForces;
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

    private void Awake()
    {
        mPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.SetPositionAndRotation(mPos, Quaternion.Euler(0.0f, 0.0f, 0.0f));
    }

    void ClearAccumulatedForces()
    {
        mAccumulatedForces = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void Integrate(double dt)
    {
        mPos += mVel * (float)dt;

        Vector3 resultingAcc = mAcc;

        if (!mShouldIgnoreForces)
        {
            resultingAcc += mAccumulatedForces * mInverseMass;
        }

        mVel += resultingAcc * (float)dt;
        float damping = Mathf.Pow(mDampingConstant, (float)dt);
        mVel *= damping;

        ClearAccumulatedForces();
    }

	public void AddForce(Vector3 force)
	{
		mAccumulatedForces += force;
	}

	public double GetMass()
	{
		return 1 / (double)mInverseMass;
	}
}
