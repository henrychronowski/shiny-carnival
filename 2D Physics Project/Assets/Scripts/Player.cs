﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float mRotationRate = 5.0f;
    public Transform mProjectileSpawnLoc;
    public GameObject mProjectilePrefab;
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
        mCurrentWeaponType = WeaponType.SPRING;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
		float speed = 5;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.Rotate(0.0f, 0.0f, mRotationRate, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.Rotate(0.0f, 0.0f, -mRotationRate, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
			Vector2 angle = new Vector2(mProjectileSpawnLoc.position.x - transform.position.x, mProjectileSpawnLoc.position.y - transform.position.y);
            PhysicsObject2D proj = Instantiate(mProjectilePrefab, mProjectileSpawnLoc.position, mProjectileSpawnLoc.rotation).GetComponent<PhysicsObject2D>();
			proj.SetVel(angle * speed);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            int current = (int)mCurrentWeaponType;
            current++;
            current %= (int)WeaponType.NUM_PROJECTILE_TYPES;

            mCurrentWeaponType = (WeaponType)current;
        }
    }
}
