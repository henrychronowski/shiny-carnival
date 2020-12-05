using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemManager : MonoBehaviour
{
    public Integrator mIntegrator;
    public ForceManager mForceManager;
    public List<PhysicsObject3D> mPhysicsObjects;

    private GravityForceGenerator gravityForceGenerator;

    private void Awake()
    {
        mIntegrator = GetComponent<Integrator>();
        mForceManager = GetComponent<ForceManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gravityForceGenerator = new GravityForceGenerator(true, mPhysicsObjects);
    }

    // Update is called once per frame
    void Update()
    {
        //mForceManager.UpdateForceGenerators();
        gravityForceGenerator.UpdateForce();
        mIntegrator.Integrate(Time.deltaTime);
    }
}
