using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTotem : Totem
{
    static public int ManaCost { get; } = 30;

    private Material sphereMaterial;
    [SerializeField] private GameObject sphere;

    public override TotemType type { get; } = TotemType.Water;
    public override int manaCost { get; protected set; } = ManaCost;
    protected override float timeBetweenActions { get; set; } = 7;

    private string sphereTextureName;
    private Vector2 sphereTextureOffset = new();
    [SerializeField] private float sphereRotationSpeed = 0.01f;
    [SerializeField] private float manaIncresingAmount = 10;

    protected override void Start()
    {
        base.Start();

        sphereMaterial = sphere.GetComponent<MeshRenderer>().material;
        sphereTextureName = sphereMaterial.GetTexturePropertyNames()[0];
    }

    protected override void Update()
    {
        base.Update();
        if (isReady)
        {
            RotateSphere();
        }
    }

    private void RotateSphere()
    {
        sphereTextureOffset.x += sphereRotationSpeed;
        sphereMaterial.SetTextureOffset(sphereTextureName, sphereTextureOffset);
    }

    protected override void ShowReadiness()
    {
        base.ShowReadiness();
        sphere.SetActive(true);
        StartCoroutine(sphere.MoveObjectSmoothly(new Vector3(0, sphere.transform.localPosition.y + 0.3f, 0), 1f));
    }

    public override void PrepareAction()
    {
        Action(null);
    }

    public override void Action(GameObject target)
    {
        base.Action(target);
        Player.Instance.IncreaseMana(manaIncresingAmount);
        sphere.transform.localPosition = new Vector3(0, sphere.transform.localPosition.y - 0.3f, 0);
        sphere.SetActive(false);

        // AudioManager.Instance.Play("water-totem-action");
    }
}
