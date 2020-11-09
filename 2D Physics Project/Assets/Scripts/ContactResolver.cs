using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactResolver : MonoBehaviour
{
	private static ContactResolver instance = null;
	public static ContactResolver Instance
	{
		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType<ContactResolver>();
				if(instance == null)
				{
					GameObject go = new GameObject();
					go.name = "ContactResolver";
					instance = go.AddComponent<ContactResolver>();
					DontDestroyOnLoad(go);
				}
			}
			return instance;
		}
	}

	[SerializeField]
	private int numIterations = 4;
	private int iterationsUsed = 0;

	private List<Particle2DContact> contacts;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		resolveContacts(Time.deltaTime);
	}

	public void addContact(PhysicsObject2D lhs, PhysicsObject2D rhs, float restitutionCoefficient, Vector2 contactNormal, float penetration)
	{
		contacts.Add(new Particle2DContact(lhs, rhs, restitutionCoefficient, contactNormal, penetration, Vector2.zero, Vector2.zero));
	}

	public void setIterations(int numIterations)
	{
		this.numIterations = numIterations;
	}

	private void resolveContacts(float dt)
	{
		foreach( Particle2DContact contact in contacts)
		{

		}

		if (contacts.Count == 0)
			return;

		int i;
		iterationsUsed = 0;
		while(iterationsUsed < numIterations)
		{
			float max = float.MaxValue;
			int numContacts = contacts.Count;
			int maxIndex = numContacts;
			i = 0;
			foreach(Particle2DContact contact in contacts)
			{
				float sepVel = contact.calculateSeparatingVelocity();
				if(sepVel < max && (sepVel < 0.0f || contact.penetration > 0.0f))
				{
					max = sepVel;
					maxIndex = i;
				}
				++i;
			}
			if (maxIndex == numContacts)
				break;

			contacts[maxIndex].resolve(dt);

			foreach(Particle2DContact contact in contacts)
			{
				if (contact.lhs == contacts[maxIndex].lhs)
					contact.penetration -= Vector2.Dot(contacts[maxIndex].move1, contact.contactNormal);
				else if(contact.lhs == contacts[maxIndex].rhs)
					contact.penetration -= Vector2.Dot(contacts[maxIndex].move2, contact.contactNormal);

				if (contact.rhs == contacts[maxIndex].lhs)
					contact.penetration -= Vector2.Dot(contacts[maxIndex].move1, contact.contactNormal);
				else if (contact.rhs == contacts[maxIndex].rhs)
					contact.penetration -= Vector2.Dot(contacts[maxIndex].move2, contact.contactNormal);
			}
			iterationsUsed++;
		}
		contacts.Clear();
	}
}
