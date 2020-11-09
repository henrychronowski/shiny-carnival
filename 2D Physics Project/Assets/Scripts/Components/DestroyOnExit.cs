using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExit : MonoBehaviour
{
    public List<ForceGenerator2D> mForceGenerators;
    public GameManager mGameManager;

    private void Awake()
    {
		mForceGenerators = new List<ForceGenerator2D>();
		mForceGenerators.Clear();
        mGameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void OnDestroy()
    {
		foreach(var generator in mForceGenerators)
			mGameManager.mForceManager.DeleteForceGenerator(generator);
    }
}
