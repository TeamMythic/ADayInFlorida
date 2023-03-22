using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
[CreateAssetMenu(fileName = "Gun", menuName = "Mythics Guns/Gun", order = 0)]
public class GunScriptableObject : ScriptableObject
{
    public GunType Type;
    public string Name;
    public GameObject ModelPrefab;
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;

    public ShootConfigurationScriptableObject ShootConfig;
    public TrailConfigurationScriptableObject TrailConfig;

    private MonoBehaviour ActiveMonoBehaviour;
    private GameObject Model;
    private float LastShootTime;
    [HideInInspector] public ParticleSystem ShootSystem;
    //private ObjectPool<TrailRenderer> TrailPool; cannot use in 2019 rip :/
    //[was introduced in unity 2021.1] well the method for setting = new Object Pool that is.
    public void Spawn(Transform Parent, MonoBehaviour ActiveMonoBehavior)
    {
        this.ActiveMonoBehaviour = ActiveMonoBehavior;
        LastShootTime = 0;//in the editor this will not be properly reset, in build it's fine
        //TrailPool = new ObjectPool<TrailRenderer>(CreateTrail); Cannot use because it was introduced in 2021 :/ rip!
        Model = Instantiate(ModelPrefab);
        Model.transform.SetParent(Parent, false);
        Model.transform.localPosition = SpawnPoint;
        Model.transform.localRotation = Quaternion.Euler(SpawnRotation);

        ShootSystem = Model.GetComponentInChildren<ParticleSystem>();
    }
    public void Shoot()
    {
        if(Time.time > ShootConfig.FireRate + LastShootTime)
        {
            LastShootTime = Time.time;
            ShootSystem.Play();
            //The spread and direction:
            Vector3 shootDirection = ShootSystem.transform.forward + new Vector3(Random.Range(-ShootConfig.Spread.x, ShootConfig.Spread.x),
                                                                                 Random.Range(-ShootConfig.Spread.y, ShootConfig.Spread.y),
                                                                                 Random.Range(-ShootConfig.Spread.z, ShootConfig.Spread.z));
            shootDirection.Normalize();//Gives us a shoot direction of length 1 so it's actually a direction vector:
            if(Physics.Raycast(ShootSystem.transform.position, shootDirection, out RaycastHit hit, ShootConfig.rayHitDistance, ShootConfig.Hitmask))
            {//Did hit something:
                ActiveMonoBehaviour.StartCoroutine(PlayTrail(ShootSystem.transform.position, hit.point, hit));
                if(hit.collider.gameObject.TryGetComponent(out InteractableInterface interactable))
                {
                    interactable.ShotByCrazyBill();
                }
            }
            else
			{//Still play a effect to sell the elusion that we shot.
                ActiveMonoBehaviour.StartCoroutine(PlayTrail(ShootSystem.transform.position, ShootSystem.transform.position + (shootDirection * TrailConfig.MissDistance),
                                                   new RaycastHit()));
			}
        }
    }
    private IEnumerator PlayTrail(Vector3 StartPoint, Vector3 EndPoint, RaycastHit hit)
    {
        //TrailRenderer instance = TrailPool.Get();//get instance of trail renderer: Cannot use in 2019 introduced in 2021 rip.
        TrailRenderer createdTrail = CreateTrail();
		createdTrail.transform.gameObject.SetActive(true);
        createdTrail.transform.position = StartPoint;
        yield return null;//avoid residual from last point where the trail rendere was used at (weird bug fix)
		createdTrail.emitting = true;
        float distance = Vector3.Distance(StartPoint, EndPoint);
        float remainingDistance = distance;
        while(remainingDistance > 0)
        {
			createdTrail.transform.position = Vector3.Lerp(StartPoint, EndPoint, Mathf.Clamp01(1 - (remainingDistance / distance)));
            remainingDistance -= TrailConfig.SimilationSpeed * Time.deltaTime;
            yield return null;
        }
		createdTrail.transform.position = EndPoint;

        yield return new WaitForSeconds(TrailConfig.Duration);
        yield return null;
		createdTrail.emitting = false;
		createdTrail.gameObject.SetActive(false);
        //TrailPool.Release(instance); cannot use in 2019 :/ rip.
        Destroy(createdTrail);
    }

    private TrailRenderer CreateTrail()
    {//When We need to create a new trail if there is no more in the pool:
        GameObject instance = new GameObject("Bullet Trail");
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();
        trail.colorGradient = TrailConfig.Color;
        trail.material = TrailConfig.Material;
        trail.widthCurve = TrailConfig.WidthCurve;
        trail.time = TrailConfig.Duration;
        trail.minVertexDistance = TrailConfig.MinVertexDistance;

        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        return trail;
    }
}