using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTotem : Totem
{
    static public int ManaCost { get; } = 40;

    [SerializeField] private GameObject fire;

    public override TotemType type { get; } = TotemType.Fire;
    public override int manaCost { get; protected set; } = ManaCost;
    protected override float timeBetweenActions { get; set; } = 5;

    [SerializeField] private Vector3 fireballStartPos;
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
        TargetSelection.Instance.StartTargetSelection(this);
    }

    public override void Action(GameObject target)
    {
        base.Action(target);

        fire.SetActive(false);

        var startPos = fireballStartPos + transform.position;
        var direction = target.transform.position - startPos;
        var fireballGO = Instantiate(firaballPrefab);
        fireballGO.transform.position = startPos;
        fireballGO.GetComponent<Fireball>().Init(target.transform.position, direction);

        Destroy(target);
    }
}
