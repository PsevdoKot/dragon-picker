using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public static int DragonFireballMinDamage { get; set; } = 15;
    public static int DragonFireballMaxDamage { get; set; } = 25;
    public static float DragonFireballSpeed { get; set; } = 0.5f;
    public static int TotemFireballMinDamage { get; set; } = 15;
    public static int TotemFireballMaxDamage { get; set; } = 15;
    public static float TotemFireballSpeed { get; set; } = 0.5f;

    [SerializeField] private GameObject fireEffectGO;
    [SerializeField] private GameObject trailEffectGO;
    [SerializeField] private GameObject explosionEffectGO;

    private bool isActive = true;
    private bool isTargetMissed = false;
    private float speed;
    private int minDamage;
    private int maxDamage;
    private Vector3 targetPos;
    private Vector3 direction;
    [SerializeField] private FireballType type;
    [SerializeField] private float timeAfterMiss;
    [SerializeField] private float timeForExplosion;
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

        SetParamsDependingOnType();

        StartCoroutine(PlaySound());
    }

    private void SetParamsDependingOnType()
    {
        (speed, minDamage, maxDamage) = type switch
        {
            FireballType.Dragon => (DragonFireballSpeed, DragonFireballMinDamage, DragonFireballMaxDamage),
            FireballType.Totem => (TotemFireballSpeed, TotemFireballMinDamage, TotemFireballMaxDamage),
            _ => throw new System.Exception("The new fireball type has not been processed"),
        };
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
                    coll.gameObject.GetComponent<Totem>().TakeDamage(CalculateRandomDamage());
                }
                else if (coll.CompareTag("Dragon"))
                {
                    Dragon.Instance.TakeDamage(CalculateRandomDamage());
                }
                else if (coll.CompareTag("Player"))
                {
                    Player.Instance.TakeDamage(CalculateRandomDamage());
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

    private IEnumerator PlaySound()
    {
        AudioManager.Instance.Play($"fireball-shoot{Random.Range(1, 6)}");
        yield return new WaitForSecondsRealtime(0.8f);

        AudioManager.Instance.Play("fireball-burning");
    }

    private IEnumerator HandleTargetMiss()
    {
        isTargetMissed = true;
        yield return new WaitForSecondsRealtime(timeAfterMiss);
        if (isActive)
        {
            Destroy(gameObject);
        }
    }

    private int CalculateRandomDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }

    private IEnumerator Explode()
    {
        isActive = false;
        fireEffectGO.SetActive(false);
        trailEffectGO.SetActive(false);
        explosionEffectGO.SetActive(true);
        AudioManager.Instance.Stop("fireball-burning");
        AudioManager.Instance.Play($"fireball-explosion{Random.Range(1, 7)}");
        yield return new WaitForSecondsRealtime(timeForExplosion);

        Destroy(gameObject);
    }
}
