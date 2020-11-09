using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TargetScript : MonoBehaviour
{
	[SerializeField]
	private float radius = 1;
	[SerializeField]
	private ScoreManager scoreManager;
	[SerializeField]
	private int xBoundOffset = 1, yBoundOffset = 1;
	private Vector2Int topLeft;
	private Vector2Int bottomRight;

	private System.Random r = new System.Random();

	private void Start()
	{
		topLeft = new Vector2Int((int)Mathf.Floor(GameManager.Instance.topLeft.x + (xBoundOffset + 2)), (int)Mathf.Floor(GameManager.Instance.topLeft.y - (yBoundOffset + 5)));
		bottomRight = new Vector2Int((int)Mathf.Floor(GameManager.Instance.bottomRight.x - xBoundOffset), (int)Mathf.Floor(GameManager.Instance.bottomRight.y + (yBoundOffset + 1)));

		setRandomPosition();
	}

	void Update()
    {
		if (checkCollision(GameManager.Instance.mPhysicsObjects))
		{
			scoreManager.addPoints();
			setRandomPosition();
		}
    }

	bool checkCollision(List<PhysicsObject2D> physObjects)
	{
		bool result = false;
		foreach(PhysicsObject2D projectile in physObjects)
		{
			if(projectile.gameObject.CompareTag("Projectile"))
			{
				float distance = Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
					new Vector2(projectile.gameObject.transform.position.x, projectile.gameObject.transform.position.y));
				if(distance < radius)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	void setRandomPosition()
	{
		gameObject.transform.SetPositionAndRotation(new Vector3(r.Next(topLeft.x, bottomRight.x),
																r.Next(bottomRight.y, topLeft.y),
																0.0f),
													gameObject.transform.rotation);
	}
}
