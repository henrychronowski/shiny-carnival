using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public Integrator integrator;
    public List<PhysicsObject2D> physicsObjects;

    // Start is called before the first frame update
    void Start()
    {
		integrator = GetComponent<Integrator>();
	}

    // Update is called once per frame
    void Update()
    {
		integrator.Integrate(Time.deltaTime);
    }

	//void CheckBounds()
	//{
	//	// get root objects in scene
	//	List<GameObject> rootObjects = new List<GameObject>();
	//	Scene scene = SceneManager.GetActiveScene();
	//	scene.GetRootGameObjects(rootObjects);

	//	// iterate root objects and do something
	//	for (int i = 0; i < rootObjects.Count; ++i)
	//	{
	//		GameObject gameObject = rootObjects[i];
			
	//	}
	//}
}
