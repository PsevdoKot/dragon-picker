using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Totem : MonoBehaviour
{
    public static Totem[] Instances;

    public int placeId { get; set; }
    public bool isReady { get; private set; } = false;
    public int HP { get; private set; } = 100;
    [SerializeField] abstract public float manaCost { get; protected set; }
    [SerializeField] abstract protected float timeBetweenActions { get; set; }

    private bool isActive = true;
    private float actionTimer;

    public void Init(int placeId)
    {
        Instances ??= new Totem[6];
        Instances[placeId] = this;
        // добавить ui
    }

    protected virtual void Start()
    {
        actionTimer = timeBetweenActions;
    }

    protected virtual void Update()
    {
        if (!isActive) return;

        if (HP < 0)
        {
            DestroyTotem();
        }

        if (!isReady)
        {
            if (actionTimer < 0)
            {
                ShowReadiness();
                actionTimer = timeBetweenActions;
            }
            else
            {
                actionTimer -= Time.deltaTime;
            }
        }
    }

    private void ShowReadiness()
    {
        isReady = true;
    }

    protected virtual void Action()
    {
        isReady = false;
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
    }

    protected virtual void DestroyTotem()
    {
        isActive = false;
        Instances[placeId] = null;
        // убрать ui
        Destroy(gameObject);
    }
}
