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
		
	}


	public void SetIterations(int numIterations)
	{
		this.numIterations = numIterations;
	}

	public void ResolveContacts(List<Particle2DContact> contacts, float dt)
	{
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
