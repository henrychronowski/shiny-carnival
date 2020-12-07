using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Transform cameraFocus;
    [SerializeField]
    private float minFOV = 30.0f;
    [SerializeField]
    private float maxFOV = 150.0f;
    [SerializeField]
    private float increment = 10.0f;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float minDistance;

    private float cameraDistance;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        startPos = mainCamera.transform.position;
        cameraDistance = (mainCamera.transform.position - cameraFocus.transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.LookAt(cameraFocus);
        Vector3 distance = mainCamera.transform.position - cameraFocus.transform.position;
        float threshold = distance.magnitude - cameraDistance;

        Debug.Log(threshold);

        if(distance.magnitude > maxDistance)
        {
            transform.position += mainCamera.transform.forward * (distance.magnitude - maxDistance);
        }
        else if(distance.magnitude < minDistance)
        {
            transform.position += mainCamera.transform.forward * (distance.magnitude - minDistance);
        }
        else if (threshold > 0.1)
        {
            transform.position -= mainCamera.transform.forward * threshold;
        }
        else if (threshold < -0.1)
        {
            transform.position -= mainCamera.transform.forward * threshold;
        }

        cameraDistance = (mainCamera.transform.position - cameraFocus.transform.position).magnitude;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Vector3 direction = mainCamera.transform.forward;
            mainCamera.transform.position += direction * increment;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Vector3 direction = mainCamera.transform.forward;
            mainCamera.transform.position -= direction * increment;
        }
    }

    public void FocusOnPlanet(GameObject planet)
    {
        //if planet == sun
        //mainCamera.transform.position = startPos
        //cameraFocus -> planet
    }
}
