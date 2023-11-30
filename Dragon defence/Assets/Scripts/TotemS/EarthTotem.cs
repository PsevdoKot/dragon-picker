using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTotem : Totem
{
    public readonly TotemType type = TotemType.Earth;
    public override float manaCost { get; protected set; } = 20;
    protected override float timeBetweenActions { get; set; } = 20;

    [SerializeField] private float slowDownAmount;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Action()
    {
        base.Action();
        // Замедлить указанного врага
    }
}
