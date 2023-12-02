using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Animator anim;

    public bool isActive { get; private set; } = true;
    public float mana { get; set; }
    public float maxMana { get; private set; } = 100;
    public int HP { get; private set; }
    public int maxHP { get; private set; } = 100;

    private int shildAmount = 0;
    [SerializeField] private float timeToDie = 3f;
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
        shildAmount += amount;
        yield return new WaitForSecondsRealtime(duration);
        shildAmount = 0;
    }

    public void PlaceTotem(float manaCost)
    {
        DecreaseMana(manaCost);
        var rand = Random.Range(0, 2);
        anim.SetTrigger("totemPositioning");
        anim.SetInteger("positioningType", rand);
        if (rand == 0) StartCoroutine("CompensateAnimationTranslate");
    }

    private IEnumerator CompensateAnimationTranslate()
    {
        var vector = new Vector3(0, 0.7f, 0);
        transform.position += vector;
        yield return new WaitForSecondsRealtime(2.7f);
        transform.position -= vector;
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
            anim.SetTrigger("hitted");
            // добавить звук
        }
        else
        {
            StartCoroutine("Die");
        }
    }

    private IEnumerator Die()
    {
        anim.SetTrigger("dying");
        isActive = false;
        yield return new WaitForSecondsRealtime(timeToDie);
        Fight.Instance.PlayerLose();
    }
}
