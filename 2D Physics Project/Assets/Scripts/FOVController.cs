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

    private float maxDistance;
    private float minDistance;
    private float cameraDistance;

    private Vector3 startPos;
    private Vector3 startRotation;
    private SolarSystemManager systemManager;

    [SerializeField]
    private int focusIndex;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        startPos = transform.position;
        startRotation = mainCamera.transform.localRotation.eulerAngles;
        cameraDistance = (mainCamera.transform.position - cameraFocus.transform.position).magnitude;
        systemManager = GameObject.FindGameObjectWithTag("SolarSystemManager").GetComponent<SolarSystemManager>();

        focusIndex = 0;
        minDistance = systemManager.mPhysicsObjects[focusIndex].minCameraZoom;
        maxDistance = systemManager.mPhysicsObjects[focusIndex].maxCameraZoom;

        FocusOnPlanet(systemManager.mPhysicsObjects[focusIndex].gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraFocus.gameObject.tag != "Sun")
            mainCamera.transform.LookAt(cameraFocus);
        Vector3 distance = mainCamera.transform.position - cameraFocus.transform.position;
        float threshold = distance.magnitude - cameraDistance;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject planet = systemManager.mPhysicsObjects[focusIndex].gameObject;
            planet.GetComponent<CelestialBody>().SetActiveUI(false);

            focusIndex++;
            if (focusIndex > systemManager.mPhysicsObjects.Count - 1)
                focusIndex = 0;
            planet = systemManager.mPhysicsObjects[focusIndex].gameObject;
            FocusOnPlanet(planet);
        }
    }

    public void FocusOnPlanet(GameObject planet)
    {
        if(planet.tag == "Sun")
        {
            mainCamera.transform.position = startPos;
            transform.localEulerAngles = startRotation;
        }
        cameraFocus = planet.transform;
        mainCamera.transform.LookAt(cameraFocus);

        minDistance = systemManager.mPhysicsObjects[focusIndex].minCameraZoom;
        maxDistance = systemManager.mPhysicsObjects[focusIndex].maxCameraZoom;
        planet.GetComponent<CelestialBody>().SetActiveUI(true);
    }
}
