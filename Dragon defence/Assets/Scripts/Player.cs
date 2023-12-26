using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public static int MaxMana { get; set; } = 100;
    public static int MaxHP { get; set; } = 100;
    public static float ManaRegenSpeed { get; set; } = 0.005f;

    public bool isActive { get; private set; } = true;
    public float mana { get; set; }
    public int HP { get; private set; }
    public int shield { get; private set; } = 0;
    public int maxShield { get; private set; } = 0;

    private Animator anim;
    private Material material;
    private SkinnedMeshRenderer render;

    private Color standartColor = new Color(1f, 1f, 1f);
    [SerializeField] private Color withShieldBodyColor = new Color(0.41f, 0.97f, 0.81f);

    void Start()
    {
        Instance = this;

        anim = GetComponent<Animator>();
        render = GetComponentInChildren<SkinnedMeshRenderer>();
        material = render.materials[0];

        mana = MaxMana;
        HP = MaxHP;
    }

    void Update()
    {
        if (!isActive) return;

        RegenMana();
    }

    private void ClampMana()
    {
        mana = Mathf.Clamp(mana, 0, MaxMana);
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
        mana += ManaRegenSpeed;
        ClampMana();
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

    public IEnumerator PlaceTotem(int manaCost)
    {
        DecreaseMana(manaCost);
        var rand = Random.Range(0, 2);
        anim.SetTrigger("totemPositioning");
        anim.SetInteger("positioningType", rand);
        yield return new WaitForSecondsRealtime(0.3f);

        AudioManager.Instance.Play("player-totem-placement");
    }

    public void IncreaseHP(int amount)
    {
        HP += amount;
        Mathf.Clamp(HP, 0, MaxHP);
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
                damageAmount = -shield;
                HandleShieldDestruction();
                AudioManager.Instance.Play("shield-destruction");
            }
        }

        HP -= damageAmount;
        if (HP > 0)
        {
            anim.SetTrigger("hitted");
            AudioManager.Instance.Play($"player-hitted{Random.Range(1, 3)}");
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
        AudioManager.Instance.Play($"player-dying{Random.Range(1, 3)}");
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
