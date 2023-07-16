using System.Collections;
using UnityEngine;

public class Miniboss : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject projectile;
    private float timeBtwShots;
    public float starTimeBtwShots;

    public Animator anim;
    public float attackDuration;
    private float timeBtwAttacks;
    public float starTimeBtwAttacks;
    public LayerMask whatIsBoss;
    public float damage;

    public BossHealthBar bossHealthBar;

    private void Update()
    {
        if(bossHealthBar.inCriticalState == false)
        {
            if (timeBtwShots <= 0)
            {
                Instantiate(projectile, shootPoint.transform.position, Quaternion.identity);
                FindObjectOfType<AudioManager>().Play("Bullet");
                timeBtwShots = starTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }

            if (timeBtwAttacks <= 0)
            {
                StartCoroutine(Attack());
                timeBtwAttacks = starTimeBtwAttacks;
            }
            else
            {
                timeBtwAttacks -= Time.deltaTime;
            }
        }
        else
        {
            anim.SetBool("inCriticalState", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" && collision.IsTouchingLayers(whatIsBoss))
        {
            BossHealthBar.actualLife -= PlayerStats.bulletDamage;
            Destroy(collision.gameObject);
            StartCoroutine(Damage());
            FindObjectOfType<AudioManager>().Play("EnemyDamage");
        }
    }

    IEnumerator Damage()
    {
        for (int i = 0; i < 3; i++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.1f);
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator Attack()
    {
        float timer = 0f;
        while (timer < attackDuration)
        {
            timer += Time.deltaTime;
            anim.SetBool("IsAttacking", true);

            yield return null;
        }
        anim.SetBool("IsAttacking", false);
    }
}