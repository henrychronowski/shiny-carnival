using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemManager : MonoBehaviour
{
    public Integrator mIntegrator;
    public ForceManager mForceManager;
    public List<PhysicsObject3D> mPhysicsObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mForceManager.UpdateForceGenerators();
        mIntegrator.Integrate(Time.deltaTime);
    }
}
