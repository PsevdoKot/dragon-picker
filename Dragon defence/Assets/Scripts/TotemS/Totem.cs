using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Totem : MonoBehaviour
{
    public int placeId { get; set; }
    public bool isReady { get; private set; } = false;
    public int HP { get; private set; }
    public int maxHP { get; private set; } = 30;
    public int shildAmount { get; private set; } = 0;
    public int maxShildAmount { get; private set; } = 0;
    public float actionTimer { get; private set; }
    abstract public TotemType type { get; }
    abstract public float manaCost { get; protected set; }
    abstract protected float timeBetweenActions { get; set; }

    private bool isActive = true;

    protected virtual void Start()
    {
        HP = maxHP;
        actionTimer = timeBetweenActions;
    }

    protected virtual void Update()
    {
        if (!isActive) return;

        if (!isReady)
        {
            if (actionTimer < 0)
            {
                ShowReadiness();
            }
            else
            {
                actionTimer -= Time.deltaTime;
            }
        }
    }

    protected virtual void ShowReadiness()
    {
        isReady = true;
    }

    public abstract void PrepareAction();

    protected virtual void Action()
    {
        isReady = false;
        actionTimer = timeBetweenActions;
    }

    public IEnumerator AddShieldAmount(int amount, float duration)
    {
        shildAmount = maxShildAmount = amount;
        yield return new WaitForSecondsRealtime(duration);
        shildAmount = maxShildAmount = 0;
    }

    public void TakeDamage(int damageAmount)
    {
        if (shildAmount > 0)
        {
            shildAmount -= damageAmount;
            if (shildAmount >= 0)
            {
                return;
            }
            else
            {
                (shildAmount, damageAmount) = (0, -shildAmount);
            }
        }

        HP -= damageAmount;
        if (HP > 0)
        {
            // добавить импакт, звук
        }
        else
        {
            DestroyTotem();
        }
    }

    protected virtual void DestroyTotem()
    {
        isActive = false;
        // добавить импакт, звук
        TotemsRow.Instance.DestroyTotem(placeId);
    }
}
