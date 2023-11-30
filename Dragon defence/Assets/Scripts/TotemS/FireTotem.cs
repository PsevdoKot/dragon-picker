using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTotem : Totem
{
    public readonly TotemType type = TotemType.Fire;
    public override float manaCost { get; protected set; } = 40;
    protected override float timeBetweenActions { get; set; } = 15;

    [SerializeField] private GameObject firaballPrefab;

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
        // Выстрелить fireball`ом по направлению курсора
    }
}
