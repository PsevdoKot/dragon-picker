using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTotem : Totem
{
    public static int ManaCost { get; set; } = 40;
    public static int MaxHP { get; set; } = 30;
    public static float TimeBetweenActions { get; set; } = 5f;

    public override TotemType type { get; } = TotemType.Fire;

    [SerializeField] private GameObject fire;

    [SerializeField] private Vector3 fireballStartPos;
    [SerializeField] private GameObject firaballPrefab;

    protected override void Start()
    {
        base.Start();
        HP = maxHP = MaxHP;
        manaCost = ManaCost;
        actionTimer = TimeBetweenActions;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void ShowReadiness()
    {
        base.ShowReadiness();

        fire.SetActive(true);
        StartCoroutine(PlaySound());
    }

    private IEnumerator PlaySound()
    {
        AudioManager.Instance.Play("fire-lighting");
        yield return new WaitForSecondsRealtime(2f);

        AudioManager.Instance.Play("fire-burning");
    }

    public override void PrepareAction()
    {
        TargetSelection.Instance.StartTargetSelection(this);
    }

    public override void Action(GameObject target)
    {
        base.Action(target);
        actionTimer = TimeBetweenActions;

        fire.SetActive(false);
        AudioManager.Instance.Stop("fire-burning");
        AudioManager.Instance.Play("fire-extinguishing");

        var startPos = fireballStartPos + transform.position;
        var direction = target.transform.position - startPos;
        var fireballGO = Instantiate(firaballPrefab);
        fireballGO.transform.position = startPos;
        fireballGO.GetComponent<Fireball>().Init(target.transform.position, direction);

        Destroy(target);

        AudioManager.Instance.Play($"fire-totem-action{Random.Range(1, 3)}");
    }
}
