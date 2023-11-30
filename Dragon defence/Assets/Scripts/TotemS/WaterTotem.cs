using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTotem : Totem
{
    public readonly TotemType type = TotemType.Water;
    public override float manaCost { get; protected set; } = 30;
    protected override float timeBetweenActions { get; set; } = 10;

    [SerializeField] private float manaIncresingAmount;

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
        Player.Instance.IncreaseMana(manaIncresingAmount);
    }
}
