using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Integrator : MonoBehaviour
{
    public GameManager gameManager;
    public SolarSystemManager solarSystemManager;

    [SerializeField]
    private double scale = 1.0;

    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        solarSystemManager = GameObject.FindGameObjectWithTag("SolarSystemManager").GetComponent<SolarSystemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Integrate(double dt)
    {
        if(gameManager != null)
        {
            foreach (PhysicsObject2D obj in gameManager.mPhysicsObjects)
            {
                obj.Integrate(dt);
            }
        }
        
        if(solarSystemManager != null)
        {
            foreach(PhysicsObject3D obj in solarSystemManager.mPhysicsObjects)
            {
                obj.Integrate(dt * scale);
            }
        }
    }
}
