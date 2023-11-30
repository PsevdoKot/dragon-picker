using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTotem : Totem
{
    public readonly TotemType type = TotemType.Air;

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
        // Обозначить, что можно выбрать объект для защиты
    }
}
