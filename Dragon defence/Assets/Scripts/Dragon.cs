using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject dragonFireball;

    public int HP { get; private set; }

    private bool isActive = true;
    private float moveDirection = 1;
    private float attackTimer;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float maxLeftRightDistance = 10f;
    [SerializeField] private float changeChanceDirection = 0.01f;
    [SerializeField] private float timeBetweenAttacks = 5f;

    void Start()
    {
        attackTimer = timeBetweenAttacks;
        anim = GetComponent<Animator>();
        // Invoke("Attack", 2f);
    }

    void Update()
    {
        if (!isActive) return;

        if (HP < 0)
        {
            Die();
        }

        if (attackTimer > 0)
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

    private void FixedUpdate()
    {
        if (Random.value < changeChanceDirection)
        {
            moveDirection = -moveDirection;
        }
    }

    private void Move()
    {
        var pos = transform.position;
        pos.x += moveDirection * speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x < -maxLeftRightDistance)
        {
            moveDirection = 1;
        }
        else if (pos.x > maxLeftRightDistance)
        {
            moveDirection = -1;
        } // добавить отсутствие движения md=0

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (moveDirection > 0)
        {
            anim.SetBool("moovingLeft", true);
            anim.SetBool("moovingRight", false);
        }
        else if (moveDirection < 0)
        {
            anim.SetBool("moovingRight", true);
            anim.SetBool("moovingLeft", false);
        }
        else
        {
            anim.SetBool("moovingLeft", false);
            anim.SetBool("moovingRight", false);
        }
    }

    private void Attack()
    {
        var fireball = Instantiate<GameObject>(dragonFireball);
        fireball.transform.position = transform.position;
        Invoke("Attack", timeBetweenAttacks);
    }

    private void TakeDamage()
    {
        anim.SetTrigger("hitted");
    }

    private void Die()
    {
        anim.SetTrigger("dying");
    }
}
