using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTotem : Totem
{
    public static int ManaCost { get; set; } = 30;
    public static int MaxHP { get; set; } = 30;
    public static float TimeBetweenActions { get; set; } = 7f;
    public static float ManaRegenAmount { get; set; } = 10;

    public override TotemType type { get; } = TotemType.Water;

    private Material sphereMaterial;
    [SerializeField] private GameObject sphere;

    private string sphereTextureName;
    private Vector2 sphereTextureOffset = new();
    [SerializeField] private float sphereRotationSpeed = 0.01f;

    protected override void Start()
    {
        base.Start();
        HP = maxHP = MaxHP;
        manaCost = ManaCost;
        actionTimer = TimeBetweenActions;

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
        actionTimer = TimeBetweenActions;

        Player.Instance.IncreaseMana(ManaRegenAmount);
        sphere.transform.localPosition = new Vector3(0, sphere.transform.localPosition.y - 0.3f, 0);
        sphere.SetActive(false);

        AudioManager.Instance.Play("water-totem-action");
    }
}
