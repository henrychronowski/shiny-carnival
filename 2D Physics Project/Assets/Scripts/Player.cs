using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float mRotationRate = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameObject.transform.Rotate(0.0f, 0.0f, mRotationRate, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameObject.transform.Rotate(0.0f, 0.0f, -mRotationRate, Space.World);
        }
    }
}
