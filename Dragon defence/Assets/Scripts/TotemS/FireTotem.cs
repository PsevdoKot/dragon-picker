using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTotem : Totem
{
    public readonly TotemType type = TotemType.Fire;

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
        // Обозначить, что fireball готов и можно выбрать направление атаки
    }
}
