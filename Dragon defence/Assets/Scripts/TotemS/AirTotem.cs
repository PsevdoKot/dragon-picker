using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTotem : Totem
{
    static public int ManaCost { get; } = 20;

    private Material sphereMaterial;
    [SerializeField] private GameObject sphere;

    public int shieldAmount { get; protected set; } = 60;
    public override TotemType type { get; } = TotemType.Air;
    public override int manaCost { get; protected set; } = ManaCost;
    protected override float timeBetweenActions { get; set; } = 10;

    private string sphereTextureName;
    private Vector2 sphereTextureScale = new();
    private int sphereTextureXScale = 1;
    private int sphereTextureYScale = 1;
    [SerializeField] private float sphereMaxScale = 7;
    [SerializeField] private float sphereXScalingSpeed = 0.01f;
    [SerializeField] private float sphereYScalingSpeed = 0.015f;
    [SerializeField] private float actionDuration = 15;

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
            AnimateSphere();
        }
    }

    private void AnimateSphere()
    {
        if (Mathf.Abs(sphereTextureScale.x) > sphereMaxScale)
        {
            sphereTextureXScale = -sphereTextureXScale;
        }
        if (Mathf.Abs(sphereTextureScale.x) > sphereMaxScale)
        {
            sphereTextureYScale = -sphereTextureYScale;
        }

        sphereTextureScale = new Vector2(sphereTextureScale.x + sphereXScalingSpeed * sphereTextureXScale,
            sphereTextureScale.y + sphereYScalingSpeed * sphereTextureYScale);
        sphereMaterial.SetTextureScale(sphereTextureName, sphereTextureScale);
    }

    protected override void ShowReadiness()
    {
        base.ShowReadiness();
        sphere.SetActive(true);
        StartCoroutine(sphere.MoveObjectSmoothly(new Vector3(0, sphere.transform.localPosition.y + 0.3f, 0), 1f));
    }

    public override void PrepareAction()
    {
        TargetSelection.Instance.StartTargetSelection(this);
    }

    public override void Action(GameObject target)
    {
        base.Action(target);

        sphere.transform.localPosition = new Vector3(0, sphere.transform.localPosition.y - 0.3f, 0);
        sphere.SetActive(false);

        if (target.CompareTag("Totem"))
        {
            StartCoroutine(((Totem)target).AddShieldAmount(shieldAmount, actionDuration));
        }
        else if (target.CompareTag("Player"))
        {
            StartCoroutine(((Player)target).AddShieldAmount(shieldAmount, actionDuration));
        }

        AudioManager.Instance.Play("air-totem-action");
    }
}
