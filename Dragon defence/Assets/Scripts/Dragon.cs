using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public static Dragon Instance { get; private set; }

    private Animator anim;
    private Material material;
    private SkinnedMeshRenderer render;
    [SerializeField] private GameObject dragonFireballPrefab;

    [SerializeField] public int HP { get; private set; }
    [SerializeField] public int maxHP { get; private set; } = 100;

    private bool isActive = false;
    private int moveDirection = 1;
    private float attackTimer;
    private Color standartColor = new Color(1f, 1f, 1f);
    [SerializeField] private Color slowDownBodyColor = new Color(1f, 0.73f, 0.35f);
    [SerializeField] private float timeToSleep = 4f;
    [SerializeField] private float timeToWakeUp = 3f;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float xSpeed = 12f;
    [SerializeField] private float ySpeed = 6f;
    [SerializeField] private float maxLeftDistance = 18f;
    [SerializeField] private float maxRightDistance = -18f;
    [SerializeField] private float maxTopDistance = -2f;
    [SerializeField] private float maxBottomDistance = -12f;
    [SerializeField] private float timeBetweenAttacks = 5f;
    [SerializeField] private Vector3 fireballStartPos = new(0, 7f, 6f);

    void Start()
    {
        Instance = this;

        HP = -1;
        anim = GetComponent<Animator>();
        render = GetComponentInChildren<SkinnedMeshRenderer>();
        material = render.materials[0];

        anim.SetBool("sleeping", true);
        StartCoroutine("WakeUp");

        // скрипт
        // tag
        // layer
        // коллайдер
    }

    void Update()
    {
        if (!isActive) return;

        if (attackTimer <= 0)
        {
            StartCoroutine(Attack());
            attackTimer = timeBetweenAttacks;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }

        Move();
    }

    private IEnumerator WakeUp()
    {
        yield return new WaitForSecondsRealtime(timeToSleep);

        anim.SetBool("sleeping", false);
        AudioManager.Instance.Play($"dragon-awakening{Random.Range(1, 3)}");
        // Invoke("PlayWingbeatSound", 3f);
        yield return new WaitForSecondsRealtime(timeToWakeUp);

        HP = maxHP;
        isActive = true;
        attackTimer = timeBetweenAttacks;
    }

    private void PlayWingbeatSound()
    {
        AudioManager.Instance.Play("dragon-flying");
        Invoke("PlayWingbeatSound", 1f);
    }

    private void Move()
    {
        var movementVector = RandomMovementVector();
        transform.position = transform.position + (movementVector * speed * Time.deltaTime);

        RestrictMovementInArea();

        CalculateMoveDirection(movementVector);
        UpdateAnimation();
    }

    private Vector3 RandomMovementVector()
    {
        float x = Mathf.PerlinNoise(Time.time, 925714);
        float y = Mathf.PerlinNoise(Time.time, 345318);

        return new Vector3((x - 0.465f) * xSpeed, (y - 0.465f) * ySpeed, 0);
    }

    private void CalculateMoveDirection(Vector3 movementVector)
    {
        if (movementVector.x > 0.1)
        {
            moveDirection = 1;
        }
        else if (movementVector.x < -0.1)
        {
            moveDirection = -1;
        }
        else
        {
            moveDirection = 0;
        }
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
        anim.SetInteger("moveDirection", moveDirection);
    }

    private IEnumerator Attack()
    {
        anim.SetTrigger("attacking");
        yield return new WaitForSecondsRealtime(1.10f);

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
    }

    public IEnumerator SlowDown(float strength, float duration)
    {
        speed /= strength;
        material.SetColor("_Color", slowDownBodyColor);
        yield return new WaitForSecondsRealtime(duration);

        speed *= strength;
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
        yield return new WaitForSecondsRealtime(1f);

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
