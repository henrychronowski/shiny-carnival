using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CelestialBody : PhysicsObject3D
{
    [SerializeField]
    private float radius = 1;
    [SerializeField]
    private float cameraZoomMin = 0;
    [SerializeField]
    private float cameraZoomMax = 0;

    [SerializeField]
    private Billboard billboard;

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

    public void SetActiveUI(bool active)
    {
        billboard.gameObject.SetActive(active);
        billboard.isActive = active;
        billboard.planetName.gameObject.SetActive(active);
        billboard.planetName.text = gameObject.name;
    }
}
