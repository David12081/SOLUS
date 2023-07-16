using UnityEngine;
using System.Collections;

public class Projectiles : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);

        StartCoroutine(DestroyBullet());
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);    

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}