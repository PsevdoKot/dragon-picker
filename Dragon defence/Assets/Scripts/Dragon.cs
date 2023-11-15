using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float leftRightDistance = 10f;
    [SerializeField] private float changeChanceDirection = 0.01f;
    [SerializeField] private float timeBetweenAttacks = 2f;
    [SerializeField] private GameObject dragonEgg;

    private float moveDirection = 1;

    void Start()
    {
        // Invoke("DropEgg", 2f);
    }

    void Update()
    {
        var pos = transform.position;
        pos.x += moveDirection * speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x < -leftRightDistance)
        {
            moveDirection = 1;
        }
        else if (pos.x > leftRightDistance)
        {
            moveDirection = -1;
        }
    }

    private void FixedUpdate()
    {
        if (Random.value < changeChanceDirection)
        {
            moveDirection = -moveDirection;
        }
    }

    private void DropEgg()
    {
        var egg = Instantiate<GameObject>(dragonEgg);
        egg.transform.position = transform.position + new Vector3(0f, 5f, 0f);
        Invoke("DropEgg", timeBetweenAttacks);
    }
}
