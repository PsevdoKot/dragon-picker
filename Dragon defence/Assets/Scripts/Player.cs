using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Animator anim;
    private Material material;
    private SkinnedMeshRenderer render;

    public bool isActive { get; private set; } = true;
    public float mana { get; set; }
    public float maxMana { get; private set; } = 100;
    public int HP { get; private set; }
    public int maxHP { get; private set; } = 100;
    public int shield { get; private set; } = 0;
    public int maxShield { get; private set; } = 0;

    private bool isMale;
    private Color standartColor = new Color(1f, 1f, 1f);
    [SerializeField] private Color withShieldBodyColor = new Color(0.41f, 0.97f, 0.81f);
    [SerializeField] private float manaRegenSpeed = 0.005f;

    void Start()
    {
        Instance = this;
        anim = GetComponent<Animator>();
        render = GetComponentInChildren<SkinnedMeshRenderer>();
        material = render.materials[0];

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
            AudioManager.Instance.Play(isMale ? $"player-hitted{Random.Range(1, 3)}" : "f-player-hitted");
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
        AudioManager.Instance.Play(isMale ? $"player-dying{Random.Range(1, 3)}" : "f-player-hitted");
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
