using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireEffect;
    [SerializeField] private ParticleSystem trailEffect;
    [SerializeField] private GameObject explosionEffectGO;

    private bool isActive = true;
    private bool isTargetMissed = false;
    private Vector3 targetPos;
    private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private float timeAfterMiss;
    [SerializeField] private float timeForExplosion;
    [SerializeField] private int damageAmount;
    [SerializeField] private float damageRadius;
    [SerializeField] private LayerMask masksAbleToDamage;

    void Start()
    {
        explosionEffectGO.SetActive(false);
    }

    public void Init(Vector3 targetPos, Vector3 direction)
    {
        this.targetPos = targetPos;
        this.direction = direction;
        transform.LookAt(transform.position + direction);
        // вызвать звук
    }

    void Update()
    {
        if (isActive)
        {
            var colliders = Physics.OverlapSphere(transform.position, damageRadius, masksAbleToDamage);
            if (colliders.Length > 0)
            {
                var coll = colliders[0];
                if (coll.CompareTag("Totem"))
                {
                    coll.gameObject.GetComponent<Totem>().TakeDamage(damageAmount);
                }
                else if (coll.CompareTag("Dragon"))
                {
                    Dragon.Instance.TakeDamage(damageAmount);
                }
                else if (coll.CompareTag("Player"))
                {
                    Player.Instance.TakeDamage(damageAmount);
                }

                StartCoroutine(Explode());
                return;
            }

            if (!isTargetMissed && (targetPos - transform.position).magnitude < 0.5f)
            {
                StartCoroutine(HandleTargetMiss());
            }

            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private IEnumerator HandleTargetMiss()
    {
        isTargetMissed = true;
        yield return new WaitForSecondsRealtime(timeAfterMiss);
        if (isActive)
        {
            Destroy(gameObject);
        }
        // StartCoroutine(Explode()); ??
    }

    private IEnumerator Explode()
    {
        isActive = false;
        fireEffect.Pause();
        trailEffect.Pause();
        explosionEffectGO.SetActive(true);
        // вызвать звук
        yield return new WaitForSecondsRealtime(timeForExplosion);
        Destroy(gameObject);
    }
}