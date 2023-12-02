using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EarthTotem : Totem
{
    [SerializeField] private GameObject spherePrefab;

    public override TotemType type { get; } = TotemType.Earth;
    public override float manaCost { get; protected set; } = 20;
    protected override float timeBetweenActions { get; set; } = 20;

    private float sphereAppearanceTimer;
    private List<GameObject> spheres = new();
    private Dictionary<GameObject, Material> materialBySphere = new();
    [SerializeField] private float sphereMaxScale = 0.3f;
    [SerializeField] private float sphereMinScale = 0.02f;
    [SerializeField] private Vector3 spherePos = new(0, 0.85f, 0);
    [SerializeField] private float timeBetweenSpheresAppearance = 1;
    [SerializeField] private float sphereSqueezingSpeed = 1.01f;
    [SerializeField] private float actionDuration = 8;
    [SerializeField] private float slowDownStrenght = 2;

    protected override void Start()
    {
        base.Start();
        sphereAppearanceTimer = 0;
    }

    protected override void Update()
    {
        base.Update();

        if (isReady)
        {
            SqueezeSpheres();

            if (sphereAppearanceTimer <= 0)
            {
                CreateSphere();
                sphereAppearanceTimer = timeBetweenSpheresAppearance;
            }
            else
            {
                sphereAppearanceTimer -= Time.deltaTime;
            } // исправить ошибку при постройке этого тотема
        }
    }

    private void CreateSphere()
    {
        var sphere = Instantiate(spherePrefab, transform);
        sphere.transform.localPosition = spherePos;
        sphere.transform.localScale = new Vector3(sphereMaxScale, sphereMaxScale, sphereMaxScale);
        spheres.Add(sphere);

        var material = sphere.GetComponent<MeshRenderer>().materials[0];
        material.SetColor("_TintColor", new Color(0.52f, 0.32f, 0.1f, 0));
        materialBySphere.Add(sphere, material);
    }

    private void SqueezeSpheres()
    {
        for (var i = 0; i < spheres.Count; i++)
        {
            var sphere = spheres[i];
            if (sphere.IsUnityNull()) return;

            var currentScale = sphere.transform.localScale.x;
            if (currentScale > sphereMinScale)
            {
                var newSphereScale = currentScale / sphereSqueezingSpeed;
                sphere.transform.localScale = new Vector3(newSphereScale, newSphereScale, newSphereScale);
                Debug.Log(1 - (newSphereScale / sphereMaxScale));
                materialBySphere[sphere].SetColor("_TintColor", new Color(0.52f, 0.32f, 0.1f, 1 - (newSphereScale / sphereMaxScale)));
            }
            else
            {
                spheres.Remove(sphere);
                materialBySphere.Remove(sphere);
                Destroy(sphere);
            }
        }
    }

    public override void PrepareAction()
    {

    }

    protected override void Action()
    {
        base.Action();
        spheres.Clear();
        materialBySphere.Clear();
        // Замедлить указанного врага
    }
}
