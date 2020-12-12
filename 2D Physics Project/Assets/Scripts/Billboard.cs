using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{
    public Transform cameraTransform;
    public bool isActive = false;
    public Text planetName;

    private Quaternion originalRotation;
    private Quaternion textOriginalRotation;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
        textOriginalRotation = planetName.transform.rotation;
        image = GetComponent<Image>();
        image.gameObject.SetActive(isActive);
        planetName.gameObject.SetActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.rotation = cameraTransform.rotation * originalRotation;
            planetName.transform.rotation = cameraTransform.rotation * textOriginalRotation;
        }
    }
}
