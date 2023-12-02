using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTotem : Totem
{
    [SerializeField] private GameObject fire;

    public override TotemType type { get; } = TotemType.Fire;
    public override float manaCost { get; protected set; } = 40;
    protected override float timeBetweenActions { get; set; } = 5;

    [SerializeField] private GameObject firaballPrefab;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void ShowReadiness()
    {
        base.ShowReadiness();
        fire.SetActive(true);
    }

    public override void PrepareAction()
    {

    }

    protected override void Action()
    {
        base.Action();
        fire.SetActive(false);
        // Выстрелить fireball`ом по направлению курсора
        // Instantiate(firaballPrefab);
    }
}
