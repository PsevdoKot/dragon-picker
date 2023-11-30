using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Animator anim;

    public float mana { get; set; }
    public float maxMana { get; private set; } = 100;
    public int HP { get; private set; }
    public int maxHP { get; private set; } = 100;

    private int shildAmount = 0;
    [SerializeField] private float manaRegenAmount = 0.001f;

    void Start()
    {
        Instance = this;
        anim = GetComponent<Animator>();

        mana = maxMana;
        HP = maxHP;
    }

    void Update()
    {
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
        mana += manaRegenAmount;
        ClampMana();
    }

    public void AddManaRegenAmount(float regenAmount)
    {
        manaRegenAmount += regenAmount;
    }

    public void AddShieldAmount(int amount)
    {
        shildAmount += amount;
    }

    public void PlaceTotem(float manaCost)
    {
        DecreaseMana(manaCost);
        var rand = Random.Range(0, 1);
        anim.SetInteger("totemPositioning", rand > 0.5 ? 1 : 0);
    }

    public void TakeDamage(int damageAmount)
    {
        if (shildAmount > 0)
        {
            shildAmount -= damageAmount;
        }
        if (shildAmount < 0)
        {
            (shildAmount, damageAmount) = (0, shildAmount == 0 ? damageAmount : -shildAmount);
            HP -= damageAmount;
            if (HP < 0)
            {
                Fight.Instance.PlayerLose();
            }
            else
            {
                anim.SetTrigger("hitted");
            }
        }
    }
}
