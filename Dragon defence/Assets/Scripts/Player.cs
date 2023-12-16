using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Animator anim;

    public bool isActive { get; private set; } = true;
    public float mana { get; set; }
    public float maxMana { get; private set; } = 100;
    public int HP { get; private set; }
    public int maxHP { get; private set; } = 100;
    public int shield { get; private set; } = 0;
    public int maxShield { get; private set; } = 0;

    [SerializeField] private float manaRegenSpeed = 0;

    void Start()
    {
        Instance = this;
        anim = GetComponent<Animator>();

        mana = maxMana;
        HP = maxHP;
    }

    void Update()
    {
        if (!isActive) return;
        RegenMana();
    }

    private void ClampMana()
    {
        mana = Mathf.Clamp(mana, 0, maxMana);
    }

    public void IncreaseMana(float amount)
    {
        mana += amount;
        ClampMana();
    }

    public void DecreaseMana(float amount)
    {
        mana -= amount;
        ClampMana();
    }

    private void RegenMana()
    {
        mana += manaRegenSpeed;
        ClampMana();
    }

    public void AddManaRegenSpeed(float regenSpeed)
    {
        manaRegenSpeed += regenSpeed;
    }

    public IEnumerator AddShieldAmount(int amount, float duration)
    {
        shield = maxShield = amount;
        yield return new WaitForSecondsRealtime(duration);
        shield = maxShield = 0;
    }

    public void PlaceTotem(int manaCost)
    {
        DecreaseMana(manaCost);
        var rand = Random.Range(0, 2);
        anim.SetTrigger("totemPositioning");
        anim.SetInteger("positioningType", rand);
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
                (shield, damageAmount) = (0, -shield);
            }
        }

        HP -= damageAmount;
        if (HP > 0)
        {
            anim.SetTrigger("hitted");
            // добавить звук
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        isActive = false;
        anim.SetTrigger("dying");
        // добавить звук
        StartCoroutine(Fight.Instance.PlayerDefeat());
    }

    public void HandlePlayerWin()
    {

    }

    public static explicit operator Player(GameObject v)
    {
        return v.GetComponent<Player>();
    }
}
