﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CelestialBody : PhysicsObject3D
{
    [SerializeField]
    private float radius = 1;
    [SerializeField]
    private float cameraZoomMin = 0;
    [SerializeField]
    private float cameraZoomMax = 0;

    public float minCameraZoom
    {
        get { return cameraZoomMin; }
        set { cameraZoomMin = value; }
    }

    public float maxCameraZoom
    {
        get { return cameraZoomMax; }
        set { cameraZoomMax = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(radius, radius, radius);
    }

    // Update is called once per frame
    private new void Update()
    {
        transform.localScale = new Vector3(radius, radius, radius);
        base.Update();
    }

    public float getRadius()
    {
        return radius;
    }

    //Add getters and setters for max/min camera distance
}
