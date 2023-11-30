using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTotem : Totem
{
    public readonly TotemType type = TotemType.Air;
    public override float manaCost { get; protected set; } = 20;
    protected override float timeBetweenActions { get; set; } = 30;

    [SerializeField] private float actionDuration = 30;

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
        // Перейти к выбору объекта для защиты
    }
}
