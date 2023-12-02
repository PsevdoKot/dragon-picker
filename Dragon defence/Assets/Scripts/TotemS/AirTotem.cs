using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTotem : Totem
{
    private Material sphereMaterial;
    [SerializeField] private GameObject sphere;

    public override TotemType type { get; } = TotemType.Air;
    public override float manaCost { get; protected set; } = 20;
    protected override float timeBetweenActions { get; set; } = 20;

    private string sphereTextureName;
    private Vector2 sphereTextureScale = new();
    private int sphereTextureXScale = 1;
    private int sphereTextureYScale = 1;
    [SerializeField] private float sphereMaxScale = 7;
    [SerializeField] private float sphereXScalingSpeed = 0.01f;
    [SerializeField] private float sphereYScalingSpeed = 0.015f;
    [SerializeField] private float shieldAmount = 60;
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
        // Перейти к выбору объекта для защиты
    }

    protected override void Action()
    {
        base.Action();
        sphere.transform.localPosition = new Vector3(0, sphere.transform.localPosition.y - 0.3f, 0);
        sphere.SetActive(false);

        // StartCoroutine(.AddShieldAmount(shieldAmount, actionDuration));
    }
}
