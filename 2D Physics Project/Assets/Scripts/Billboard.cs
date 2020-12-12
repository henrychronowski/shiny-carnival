using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{
    public Transform cameraTransform;
    public bool isActive = false;

    private Quaternion originalRotation;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
        image = GetComponent<Image>();
        image.gameObject.SetActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
            transform.rotation = cameraTransform.rotation * originalRotation;
    }
}
