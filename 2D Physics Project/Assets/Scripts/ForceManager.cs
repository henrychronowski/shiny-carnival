using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
    public List<ForceGenerator2D> mForceGenerators;
    public List<ForceGenerator3D> mForceGenerator3Ds;

    private void Awake()
    {
        mForceGenerators = new List<ForceGenerator2D>();
        mForceGenerators.Clear();
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

    public void UpdateForceGenerators()
    {
        foreach(ForceGenerator2D generator in mForceGenerators)
        {
            if (generator.EffectAll())
            {
                generator.UpdateForce();
            }
        }
        foreach (ForceGenerator3D generator in mForceGenerator3Ds)
        {
            if (generator.EffectAll())
            {
                generator.UpdateForce();
            }
        }
    }
}
