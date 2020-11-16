using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomSpawner2D : MonoBehaviour
{
	[SerializeField]
	private int xBoundOffset = 1, yBoundOffset = 1;
	[SerializeField]
	private GameObject projectilePrefab;
	[Tooltip("Milliseconds")]
	[SerializeField]
	private float wait = 5.0f;

	private Vector2Int topLeft;
	private Vector2Int bottomRight;
	private System.Random r = new System.Random();

    private void Start()
    {
		topLeft = new Vector2Int((int)Mathf.Floor(GameManager.Instance.topLeft.x + (xBoundOffset + 2)), (int)Mathf.Floor(GameManager.Instance.topLeft.y - (yBoundOffset + 5)));
		bottomRight = new Vector2Int((int)Mathf.Floor(GameManager.Instance.bottomRight.x - xBoundOffset), (int)Mathf.Floor(GameManager.Instance.bottomRight.y + (yBoundOffset + 1)));
		StartCoroutine("SpawnRandom");
	}

    void Update()
    {
        
    }

	IEnumerator SpawnRandom()
	{
		PhysicsObject2D proj;
		GameObject part;

		while (true)
		{
			part = Instantiate(projectilePrefab, GetRandomPosition(), Quaternion.identity);
			proj = part.GetComponent<PhysicsObject2D>();
			proj.SetVel(new Vector2(0.0f, -1.0f));
			proj.SetAcc(new Vector2(0.0f, -6.0f));
			proj.SetInverseMass(1.0f);
			proj.SetDamping(0.99f);
			GameManager.Instance.mPhysicsObjects.Add(proj);
			GameManager.Instance.mParticleManager.AddParticle(part.GetComponent<Particle2D>());
			proj.gameObject.AddComponent<DestroyOnExit>();
			yield return new WaitForSeconds(wait);
		}
	}

	private Vector3 GetRandomPosition()
	{
		return new Vector3(r.Next(topLeft.x, bottomRight.x) - 0.5f,
						   r.Next(bottomRight.y, topLeft.y) + 0.5f,
						   0.0f);
	}
}
