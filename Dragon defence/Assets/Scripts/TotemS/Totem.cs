using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Totem : MonoBehaviour
{
    public int HP { get; private set; } = 100;

    private bool isActive = true;
    private float actionTimer;
    [SerializeField] private float timeBetweenActions;

    protected virtual void Start()
    {
        actionTimer = timeBetweenActions;
    }

    protected virtual void Update()
    {
        if (!isActive) return;

        if (HP < 0)
        {
            Destroy();
        }

        if (actionTimer > 0)
        {
            Action();
            actionTimer = timeBetweenActions;
        }
        else
        {
            actionTimer -= Time.deltaTime;
        }
    }

    protected abstract void Action();

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
    }

    protected void Destroy()
    {
        isActive = false;
        // gameobject.Destroy();
    }
}
