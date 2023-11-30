using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject dragonFireball;

    public int HP { get; private set; }

    private bool isActive = true;
    private int moveDirection = 1;
    private float attackTimer;
    [SerializeField] private float speed = 0.2f;
    [SerializeField] private float maxLeftRightDistance = 10f;
    [SerializeField] private float timeToStartAttacking = 20f;
    [SerializeField] private float timeBetweenAttacks = 10f;

    void Start()
    {
        attackTimer = timeToStartAttacking;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isActive) return;

        if (attackTimer < 0)
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

    private void Move()
    {
        var movementVector = RandomPointOnUnitSphere(Time.time);
        CalculateMoveDirection(movementVector);
        transform.Translate(movementVector * speed * Time.deltaTime);

        // ограничить движения в определённой зоне

        UpdateAnimation();
    }

    private Vector3 RandomPointOnUnitSphere(float time)
    {
        float x = Mathf.PerlinNoise(time, 925714);
        float z = Mathf.PerlinNoise(time, 345318);

        Vector3 vector = new(x, 0, z);

        return Vector3.Normalize(vector);
    }

    private void CalculateMoveDirection(Vector3 movementVector)
    {
        if (movementVector.x > 0.2)
        {
            moveDirection = 1;
        }
        else if (movementVector.x < -0.2)
        {
            moveDirection = -1;
        }
        else
        {
            moveDirection = 0;
        }
    }

    private void UpdateAnimation()
    {
        anim.SetInteger("moveDirection", moveDirection);
        // if (moveDirection > 0)
        // {
        //     anim.SetBool("moovingLeft", true);
        //     anim.SetBool("moovingRight", false);
        // }
        // else if (moveDirection < 0)
        // {
        //     anim.SetBool("moovingRight", true);
        //     anim.SetBool("moovingLeft", false);
        // }
        // else
        // {
        //     anim.SetBool("moovingLeft", false);
        //     anim.SetBool("moovingRight", false);
        // }
    }

    private void Attack()
    {
        var fireball = Instantiate<GameObject>(dragonFireball);
        fireball.transform.position = transform.position;
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP < 0)
        {
            Die();
        }
        else
        {
            anim.SetTrigger("hitted");
        }
    }

    private void Die()
    {
        // приземлить дракона
        isActive = false;
        anim.SetTrigger("dying");
        StartCoroutine("AfterDeath", 3f);
    }

    private void AfterDeath()
    {
        Fight.Instance.PlayerWin();
    }
}
