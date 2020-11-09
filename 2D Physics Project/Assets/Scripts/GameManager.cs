using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[Tooltip("Ensure Top Left has ample vertical room")]
	[SerializeField]
	private Vector2 topLeft, bottomRight;
	public Integrator mIntegrator;
    public List<PhysicsObject2D> mPhysicsObjects;

    // Start is called before the first frame update
    void Start()
    {
		mIntegrator = GetComponent<Integrator>();
	}

    // Update is called once per frame
    void Update()
    {
		mIntegrator.Integrate(Time.deltaTime);
		CheckBounds();
    }

	void CheckBounds()
	{
		// get root objects in scene
		List<GameObject> rootObjects = new List<GameObject>();
		Scene scene = SceneManager.GetActiveScene();
		scene.GetRootGameObjects(rootObjects);

		foreach ( GameObject elemnt in rootObjects)
		{
			if (elemnt.transform.position.x < topLeft.x || elemnt.transform.position.x > bottomRight.x ||
				elemnt.transform.position.y > topLeft.y || elemnt.transform.position.y < bottomRight.y)
				Destroy(elemnt);
		}
	}
}
