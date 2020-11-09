using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExit : MonoBehaviour
{
    public ForceGenerator2D mForceGenerator;
    public GameManager mGameManager;

    private void Awake()
    {
        mGameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void OnDestroy()
    {
        mGameManager.mForceManager.DeleteForceGenerator(mForceGenerator);
    }
}
