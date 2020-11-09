﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[Tooltip("Ensure Top Left has ample vertical room")]
	[SerializeField]
	private Vector2 topLeft, bottomRight;
	public Integrator mIntegrator;
	public ForceManager mForceManager;
    public List<PhysicsObject2D> mPhysicsObjects;
	public WeaponType mCurrentWeaponType;

	public enum WeaponType
    {
        SPRING,
        ROD,
        NUM_PROJECTILE_TYPES
    }

    // Start is called before the first frame update
    void Start()
    {
		mIntegrator = GetComponent<Integrator>();
		mForceManager = GetComponent<ForceManager>();
		mCurrentWeaponType = WeaponType.SPRING;
	}

    // Update is called once per frame
    void Update()
    {
		mIntegrator.Integrate(Time.deltaTime);
		mForceManager.UpdateForceGenerators();
		CheckBounds();
    }

	void CheckBounds()
	{
		// get root objects in scene
		List<GameObject> rootObjects = new List<GameObject>();
		Scene scene = SceneManager.GetActiveScene();
		scene.GetRootGameObjects(rootObjects);

		foreach ( GameObject element in rootObjects)
		{
			if (element.transform.position.x < topLeft.x || element.transform.position.x > bottomRight.x ||
				element.transform.position.y > topLeft.y || element.transform.position.y < bottomRight.y)
            {
				Destroy(element);
			}
		}
	}

	public void CreateProjectile(GameObject projectile, Transform spawnPoint, Transform playerPos)
    {
		float speed = 0.0f;
		Vector2 angle;
		Vector2 gravity;
		PhysicsObject2D proj;

        switch (mCurrentWeaponType)
        {
			case WeaponType.SPRING:
                {
					speed = 20.0f;
					float speed2 = 15.0f;
					angle = new Vector2(spawnPoint.position.x - playerPos.position.x, spawnPoint.position.y - playerPos.position.y);
					gravity = new Vector2(0.0f, -6.0f);

					float mass1, mass2;
					mass1 = 1.0f;
					mass2 = 2.0f;

					proj = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation).GetComponent<PhysicsObject2D>();
					proj.SetVel(angle * speed);
					proj.SetAcc(gravity);
					proj.SetInverseMass(mass1);
					proj.SetDamping(0.99f);
					mPhysicsObjects.Add(proj);

					PhysicsObject2D proj2;
					proj2 = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation).GetComponent<PhysicsObject2D>();
					proj2.SetVel(angle * speed2);
					proj2.SetAcc(gravity);
					proj2.SetInverseMass(mass2);
					proj2.SetDamping(0.99f);
					mPhysicsObjects.Add(proj2);
					proj2.gameObject.AddComponent<DestroyOnExit>();
					
					SpringForceGenerator springForceGenerator = new SpringForceGenerator(proj, proj2, 1.0f, 50.0f, true);
					proj2.gameObject.GetComponent<DestroyOnExit>().mForceGenerator = springForceGenerator;
					mForceManager.AddForceGenerator(springForceGenerator);
				}
				break;
        }
    }

	public WeaponType GetWeaponType()
    {
		return mCurrentWeaponType;
    }
}
