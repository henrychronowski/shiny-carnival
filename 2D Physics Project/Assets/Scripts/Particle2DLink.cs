using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DLink
{
    protected PhysicsObject2D mObj1, mObj2;

    public Particle2DLink(PhysicsObject2D obj1, PhysicsObject2D obj2)
    {
        mObj1 = obj1;
        mObj2 = obj2;
    }

    public virtual void CreateContacts(List<Particle2DContact> contacts)
    {

    }

    protected float GetCurrentLength()
    {
        Vector2 temp = mObj1.transform.position - mObj2.transform.position;
        return temp.magnitude;
    }
}

public class ParticleRod : Particle2DLink
{
    float mLength;

    public ParticleRod(PhysicsObject2D obj1, PhysicsObject2D obj2, float length) : base(obj1, obj2)
    {
        mLength = length;
    }

    public override void CreateContacts(List<Particle2DContact> contacts)
    {
        if (mObj1 == null || mObj2 == null)
            return;

        float length = base.GetCurrentLength();
        if (length == mLength)
            return;

        Vector2 normal = mObj2.transform.position - mObj1.transform.position;
        normal = normal.normalized;

        float penetration;
        if(length < mLength)
        {
            penetration = (mLength - length) / 1000.0f;

            Particle2DContact contact = new Particle2DContact(mObj1, mObj2, 0, -normal, penetration, Vector2.zero, Vector2.zero);
            contacts.Add(contact);
        }
        else
        {
            penetration = (length - mLength) / 1000.0f;
            Particle2DContact contact = new Particle2DContact(mObj1, mObj2, 0, -normal, penetration, Vector2.zero, Vector2.zero);
            contacts.Add(contact);
        }
    }
}
