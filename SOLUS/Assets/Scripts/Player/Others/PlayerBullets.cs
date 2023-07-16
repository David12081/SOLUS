using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class PlayerBullets : MonoBehaviour
{
    public float lifeTime;
    private float damage;

    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    private void Update()
    {
        damage = PlayerStats.bulletDamage;
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}