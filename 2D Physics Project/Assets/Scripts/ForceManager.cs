using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
    public List<ForceGenerator2D> mForceGenerators;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddForceGenerator(ForceGenerator2D forceGenerator)
    {
        mForceGenerators.Add(forceGenerator);
    }

    public void DeleteForceGenerator(ForceGenerator2D forceGenerator)
    {
        mForceGenerators.Remove(forceGenerator);
    }

    public void UpdateForceGenerators(double dt)
    {
        foreach(ForceGenerator2D generator in mForceGenerators)
        {
            generator.UpdateForce(dt);
        }
    }
}
