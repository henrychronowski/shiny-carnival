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

	private void resolveContacts()
	{
		foreach( Particle2DContact contact in contacts)
		{

		}
	}
}
