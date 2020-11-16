using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private static GameManager instance = null;
	public static GameManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<GameManager>();
				if (instance == null)
				{
					GameObject go = new GameObject();
					go.name = "GameManager";
					instance = go.AddComponent<GameManager>();
					DontDestroyOnLoad(go);
				}
			}
			return instance;
		}
	}

	[Tooltip("Ensure Top Left has ample vertical room")]
	[SerializeField]
	public Vector2 topLeft, bottomRight;
	public Integrator mIntegrator;
	public ForceManager mForceManager;
    public List<PhysicsObject2D> mPhysicsObjects;
	public List<Particle2DLink> mParticleLinks;
	public WeaponType mCurrentWeaponType;
	public List<Particle2DContact> mContacts;
	public ContactResolver mResolver;
	public Particle2DManager mParticleManager;

	public enum WeaponType
    {
        SPRING,
        ROD,
        NUM_PROJECTILE_TYPES
    }

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
		mIntegrator = GetComponent<Integrator>();
		mForceManager = GetComponent<ForceManager>();
		mParticleManager = GetComponent<Particle2DManager>();
	}

	// Start is called before the first frame update
	void Start()
    {
		if(mIntegrator == null)
			mIntegrator = GetComponent<Integrator>();
		if(mForceManager == null)
			mForceManager = GetComponent<ForceManager>();
		if(mParticleManager == null)	
			mParticleManager = GetComponent<Particle2DManager>();
		mCurrentWeaponType = WeaponType.SPRING;

        mContacts = new List<Particle2DContact>();
        mContacts.Clear();

		mParticleLinks = new List<Particle2DLink>();
		mParticleLinks.Clear();

		mResolver = gameObject.AddComponent<ContactResolver>();
	}

    // Update is called once per frame
    void Update()
    {
		mContacts.Clear();
		foreach(var link in mParticleLinks)
        {
			link.CreateContacts(mContacts);
        }

		mResolver.SetIterations(10);
		mResolver.ResolveContacts(mContacts, Time.deltaTime);

		mForceManager.UpdateForceGenerators();
		mIntegrator.Integrate(Time.deltaTime);
		CheckBounds();
    }

	void CheckBounds()
	{
		// get root objects in scene
		List<GameObject> rootObjects = new List<GameObject>();
		Scene scene = SceneManager.GetActiveScene();
		scene.GetRootGameObjects(rootObjects);

		foreach ( GameObject element in rootObjects)
		{
			if (element.transform.position.x < topLeft.x || element.transform.position.x > bottomRight.x ||
				element.transform.position.y > topLeft.y || element.transform.position.y < bottomRight.y)
            {
				if(element.layer != 5)
					Destroy(element);
			}
		}
	}

	public void CreateProjectile(GameObject projectile, Transform spawnPoint, Transform playerPos)
    {
		float speed = 0.0f;
		Vector2 angle = new Vector2(spawnPoint.position.x - playerPos.position.x, spawnPoint.position.y - playerPos.position.y);
		Vector2 gravity;
		PhysicsObject2D proj;
		GameObject part;

        switch (mCurrentWeaponType)
        {
			case WeaponType.SPRING:
                {
					speed = 20.0f;
					float speed2 = 15.0f;
					gravity = new Vector2(0.0f, -6.0f);

					float mass1, mass2;
					mass1 = 1.0f;
					mass2 = 2.0f;

					part = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
					mParticleManager.AddParticle(part.GetComponent<Particle2D>());
					proj = part.GetComponent<PhysicsObject2D>();
					proj.SetVel(angle * speed);
					proj.SetAcc(gravity);
					proj.SetInverseMass(mass1);
					proj.SetDamping(0.99f);
					mPhysicsObjects.Add(proj);

					PhysicsObject2D proj2;
					part = Instantiate(projectile, spawnPoint.position - new Vector3(1.0f, 0.0f, 0.0f), spawnPoint.rotation);
					proj2 = part.GetComponent<PhysicsObject2D>();
					mParticleManager.AddParticle(part.GetComponent<Particle2D>());
					proj2.SetVel(angle * speed2);
					proj2.SetAcc(gravity);
					proj2.SetInverseMass(mass2);
					proj2.SetDamping(0.99f);
					mPhysicsObjects.Add(proj2);
					proj2.gameObject.AddComponent<DestroyOnExit>();
					
					SpringForceGenerator springForceGenerator = new SpringForceGenerator(proj, proj2, 1.0f, 50.0f, true);
					proj2.gameObject.GetComponent<DestroyOnExit>().mForceGenerators.Add(springForceGenerator);
					mForceManager.AddForceGenerator(springForceGenerator);
					BouyancyForceGenerator bGenerator = new BouyancyForceGenerator(true, proj, -6, 100, 0);
					mForceManager.AddForceGenerator(bGenerator);
					proj2.gameObject.GetComponent<DestroyOnExit>().mForceGenerators.Add(bGenerator);
					bGenerator = new BouyancyForceGenerator(true, proj2, -6, 100, 0);
					mForceManager.AddForceGenerator(bGenerator);
					proj2.gameObject.GetComponent<DestroyOnExit>().mForceGenerators.Add(bGenerator);
				}
				break;
			case WeaponType.ROD:
                {
					speed = 30.0f;
					gravity = new Vector2(0.0f, -6.0f);

					proj = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation).GetComponent<PhysicsObject2D>();
                    proj.SetVel(angle * speed);
                    proj.SetAcc(gravity);
                    proj.SetInverseMass(1.0f);
                    proj.SetDamping(0.99f);
                    mPhysicsObjects.Add(proj);

                    PhysicsObject2D proj2;
					proj2 = Instantiate(projectile, spawnPoint.position + new Vector3(2.5f, 0.0f), spawnPoint.rotation).GetComponent<PhysicsObject2D>();
					proj2.SetVel(angle * speed);
					proj2.SetAcc(gravity);
					proj2.SetInverseMass(1.0f);
					proj2.SetDamping(0.99f);
                    mPhysicsObjects.Add(proj2);
					proj2.gameObject.AddComponent<DestroyOnExit>();

					Particle2DLink link = new ParticleRod(proj, proj2, 5.0f);
					AddParticleLink(link);

					BouyancyForceGenerator bGenerator = new BouyancyForceGenerator(true, proj, -6, 100, 0);
					mForceManager.AddForceGenerator(bGenerator);
					proj2.gameObject.GetComponent<DestroyOnExit>().mForceGenerators.Add(bGenerator);
					bGenerator = new BouyancyForceGenerator(true, proj2, -6, 100, 0);
					mForceManager.AddForceGenerator(bGenerator);
					proj2.gameObject.GetComponent<DestroyOnExit>().mForceGenerators.Add(bGenerator);
				}
				break;
        }
    }

	public WeaponType GetWeaponType()
    {
		return mCurrentWeaponType;
    }

	public void AddParticleLink(Particle2DLink link)
    {
		mParticleLinks.Add(link);
    }

	public void DeleteParticleLink(Particle2DLink link)
    {
		mParticleLinks.Remove(link);
    }

	private void buoyancy()
	{

	}

}
