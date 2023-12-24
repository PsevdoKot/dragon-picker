using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public static Dragon Instance { get; private set; }

    // public static GameObject DragonPrefab { get; set; };
    public static int MaxHP { get; set; } = 100;
    public static float Speed { get; set; } = 4f;
    public static float XSpeed { get; set; } = 12f;
    public static float YSpeed { get; set; } = 6f;
    public static float AttackSpeed { get; set; } = 5f;

    [SerializeField] public DragonType type { get { return __type; } }
    [SerializeField] public int HP { get; private set; }

    private Animator anim;
    private Material material;
    private SkinnedMeshRenderer render;
    [SerializeField] private GameObject dragonFireballPrefab;

    private bool isActive = false;
    private bool isAttacking = false;
    private float attackTimer;
    private Color standartColor = new Color(1f, 1f, 1f);
    private Vector3 movementVector;
    [SerializeField] private DragonType __type;
    [SerializeField] private float timeToSleep = 4f;
    [SerializeField] private float timeToWakeUp = 3f;
    [SerializeField] private float maxLeftDistance = 18f;
    [SerializeField] private float maxRightDistance = -18f;
    [SerializeField] private float maxTopDistance = -2f;
    [SerializeField] private float maxBottomDistance = -12f;
    [SerializeField] private Color slowDownBodyColor = new Color(1f, 0.73f, 0.35f);
    [SerializeField] private Vector3 fireballStartPos = new(0, 7f, 6f);
    [SerializeField] private Material[] materials;

    void Start()
    {
        Instance = this;

        anim = GetComponent<Animator>();
        render = GetComponentInChildren<SkinnedMeshRenderer>();
        material = materials[Random.Range(0, 4)];
        render.material = material;

        HP = -1; // ставим ниже 0, чтобы не показывать hp bar, пока дракон не проснётся

        anim.SetBool("sleeping", true);
        StartCoroutine("WakeUp");
    }

    void Update()
    {
        if (!isActive) return;

        if (attackTimer <= 0)
        {
            StartCoroutine(Attack());
            attackTimer = AttackSpeed;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }

        if (type == DragonType.Nightmare && isAttacking) return;
        Move();
    }

    private IEnumerator WakeUp()
    {
        yield return new WaitForSecondsRealtime(timeToSleep);

        anim.SetBool("sleeping", false);
        AudioManager.Instance.Play($"dragon-awakening{Random.Range(1, 3)}");
        // Invoke("PlayWingbeatSound", 3f);
        yield return new WaitForSecondsRealtime(timeToWakeUp);

        HP = MaxHP;
        isActive = true;
        attackTimer = AttackSpeed;
    }

    private void PlayWingbeatSound()
    {
        AudioManager.Instance.Play("dragon-flying");
        Invoke("PlayWingbeatSound", 1f);
    }

    private void Move()
    {
        movementVector = RandomMovementVector();
        if (type == DragonType.Nightmare) movementVector.y = 0;
        transform.position = transform.position + (movementVector * Speed * Time.deltaTime);

        RestrictMovementInArea();
        UpdateAnimation();
    }

    private Vector3 RandomMovementVector()
    {
        float x = Mathf.PerlinNoise(Time.time, 925714);
        float y = Mathf.PerlinNoise(Time.time, 345318);

        return new Vector3((x - 0.465f) * XSpeed, (y - 0.465f) * YSpeed, 0);
    }

    private void RestrictMovementInArea()
    {
        if (transform.position.x > maxLeftDistance)
        {
            transform.position = new Vector3(maxLeftDistance, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < maxRightDistance)
        {
            transform.position = new Vector3(maxRightDistance, transform.position.y, transform.position.z);
        }

        if (transform.position.y > maxTopDistance)
        {
            transform.position = new Vector3(transform.position.x, maxTopDistance, transform.position.z);
        }
        else if (transform.position.y < maxBottomDistance)
        {
            transform.position = new Vector3(transform.position.x, maxBottomDistance, transform.position.z);
        }
    }

    private void UpdateAnimation()
    {
        anim.SetInteger("moveDirection", movementVector.x > 0.05 ? 1 : movementVector.x < -0.05 ? -1 : 0);
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        anim.SetTrigger("attacking");
        yield return new WaitForSecondsRealtime(type == DragonType.SoulEater ? 0.3f : 1.10f);

        var totemsId = TotemsRow.Instance.GetExistingTotemsId();
        var rand = Random.Range(0, totemsId.Count + 1);

        Vector3 targetPos;
        if (rand == totemsId.Count)
        {
            targetPos = Player.Instance.transform.position;
        }
        else
        {
            targetPos = TotemsRow.Totems[totemsId[rand]].transform.position;
        }
        targetPos += new Vector3(0, 0.7f, 0);

        var startPos = fireballStartPos + transform.position;
        var direction = targetPos - startPos;
        var fireballGO = Instantiate(dragonFireballPrefab);
        fireballGO.transform.position = startPos;
        fireballGO.GetComponent<Fireball>().Init(targetPos, direction);

        yield return new WaitForSecondsRealtime(0.3f);
        isAttacking = false;
    }

    public IEnumerator SlowDown(float strength, float duration)
    {
        Speed /= strength;
        material.SetColor("_Color", slowDownBodyColor);
        yield return new WaitForSecondsRealtime(duration);

        Speed *= strength;
        material.SetColor("_Color", standartColor);
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP > 0)
        {
            anim.SetTrigger("hitted");
            AudioManager.Instance.Play($"dragon-hitted{Random.Range(1, 3)}");
        }
        else
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        isActive = false;
        anim.SetTrigger("dying");
        StartCoroutine(gameObject.MoveObjectSmoothly(new Vector3(transform.position.x, -10.5f, transform.position.z), 1f));
        AudioManager.Instance.Play($"dragon-dying{Random.Range(1, 3)}");
        yield return new WaitForSecondsRealtime(2f);

        StartCoroutine(Fight.Instance.PlayerWin());
    }

    public void HandlePlayerDefeat()
    {
        isActive = false;
    }

    public static explicit operator Dragon(GameObject v)
    {
        return v.GetComponent<Dragon>();
    }
}
