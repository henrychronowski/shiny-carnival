using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector2D : MonoBehaviour
{
	private static CollisionDetector2D instance = null;
	public static CollisionDetector2D Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<CollisionDetector2D>();
				if (instance == null)
				{
					GameObject go = new GameObject();
					go.name = "CollisionDetector2D";
					instance = go.AddComponent<CollisionDetector2D>();
					DontDestroyOnLoad(go);
				}
			}
			return instance;
		}
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	static public bool DetectCollision(Particle2D lhs, Particle2D rhs)
	{
		bool result = false;
		float distance = Vector2.Distance(new Vector2(lhs.transform.position.x, lhs.transform.position.y),
			new Vector2(rhs.gameObject.transform.position.x, rhs.gameObject.transform.position.y));
		if (distance < (lhs.getRadius() + rhs.getRadius()))
		{
			result = true;
		}
		return result;
	}
}
