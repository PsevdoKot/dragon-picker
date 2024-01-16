using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Totem : MonoBehaviour
{
    private Material material;
    private MeshRenderer render;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private ParticleSystem destroyEffect;

    public int placeId { get; set; }
    public bool isReady { get; private set; } = false;
    public int HP { get; protected set; }
    public int maxHP { get; protected set; } // дубликат для предоставления информации соответствующему UI
    public int manaCost { get; protected set; } // дубликат для предоставления информации TotemsRow
    public int shield { get; private set; } = 0;
    public int maxShield { get; private set; } = 0;
    public float actionTimer { get; protected set; }
    public abstract TotemType type { get; }

    private bool isActive = true;
    private bool isShieldDestructed = false;
    private Color standartColor = new Color(1f, 1f, 1f);
    [SerializeField] private Color withShieldBodyColor = new Color(0.41f, 0.97f, 0.81f);

    protected virtual void Start()
    {
        render = GetComponentInChildren<MeshRenderer>();
        material = render.materials[0];
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
        TotemButtons.Instance.ActiveButton(placeId);
    }

    public abstract void PrepareAction();

    public virtual void Action(GameObject target)
    {
        isReady = false;
        TotemButtons.Instance.HideButton(placeId);
    }

    public IEnumerator AddShieldAmount(int amount, float duration)
    {
        HandleShieldApperance(amount);
        yield return new WaitForSecondsRealtime(duration);

        HandleShieldDestruction();
    }

    private void HandleShieldApperance(int amount)
    {
        shield = maxShield = amount;
        material.SetColor("_Color", withShieldBodyColor);
    }

    private void HandleShieldDestruction()
    {
        shield = maxShield = 0;
        material.SetColor("_Color", standartColor);
    }

    public void TakeDamage(int damageAmount)
    {
        if (shield > 0)
        {
            shield -= damageAmount;
            if (shield >= 0)
            {
                return;
            }
            else
            {
                isShieldDestructed = true;
                damageAmount = -shield;
                HandleShieldDestruction();
                AudioManager.Instance.Play("shield-destruction");
            }
        }

        HP -= damageAmount;
        if (HP > 0)
        {
            hitEffect.Play();
        }
        else
        {
            DestroyTotem();
        }

        if (!isShieldDestructed)
        {
            StartCoroutine(PlayHittedSound());
        }
        isShieldDestructed = false;
    }

    private IEnumerator PlayHittedSound()
    {
        AudioManager.Instance.Play($"totem-hitted{Random.Range(1, 7)}");
        yield return new WaitForSecondsRealtime(1f);

        AudioManager.Instance.Play($"splinters{Random.Range(1, 3)}");
    }

    protected virtual void DestroyTotem()
    {
        isActive = false;
        hitEffect.Play();
        destroyEffect.Play();
        TotemsRow.Instance.DestroyTotem(placeId);
    }

    public static explicit operator Totem(GameObject v)
    {
        return v.GetComponent<Totem>();
    }
}
