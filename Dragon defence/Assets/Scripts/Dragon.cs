using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public static Dragon Instance { get; private set; }

    private Animator anim;
    [SerializeField] private GameObject dragonFireball;

    [SerializeField] public int maxHP { get; private set; } = 100;
    [SerializeField] public int HP { get; private set; }

    private bool isActive = false;
    private int moveDirection = 1;
    private float attackTimer;
    [SerializeField] private float timeToSleep = 4f;
    [SerializeField] private float timeToWakeUp = 2f;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float xSpeed = 12f;
    [SerializeField] private float ySpeed = 6f;
    [SerializeField] private float maxLeftDistance = 18f;
    [SerializeField] private float maxRightDistance = -18f;
    [SerializeField] private float maxTopDistance = -2f;
    [SerializeField] private float maxBottomDistance = -12f;
    [SerializeField] private float timeBetweenAttacks = 5f;

    void Start()
    {
        Instance = this;

        HP = -1;
        anim = GetComponent<Animator>();

        anim.SetBool("sleeping", true);
        StartCoroutine("WakeUp");
    }

    void Update()
    {
        if (!isActive) return;

        if (attackTimer <= 0)
        {
            Attack();
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
        yield return new WaitForSecondsRealtime(timeToWakeUp);
        HP = maxHP;
        isActive = true;
        attackTimer = timeBetweenAttacks;
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

    private void Attack()
    {
        anim.SetTrigger("attacking");
        // var fireball = Instantiate<GameObject>(dragonFireball);
        // fireball.transform.position = transform.position;
        // добавить звук
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP > 0)
        {
            anim.SetTrigger("hitted");
            // добавить звук
        }
        else
        {
            Die();
            // добавить звук
        }
    }

    private void Die()
    {
        // приземлить дракона
        isActive = false;
        anim.SetTrigger("dying");
        StartCoroutine(gameObject.MoveObjectSmoothly(new Vector3(transform.position.x, -10.5f, transform.position.z), 1f));
        Invoke("AfterDeath", 3f);
    }

    private void AfterDeath()
    {
        Fight.Instance.PlayerWin();
    }
}
