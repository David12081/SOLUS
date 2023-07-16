using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmalgamBoss : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject projectile;
    private float timeBtwShots;
    public float starTimeBtwShots;

    public Animator anim;
    public float attackDuration;
    private float timeBtwAttacks;
    public float starTimeBtwAttacks;

    private float timeBtwStomps;
    public float starTimeBtwStomps;
    public float attackArea;
    private Transform target;

    public LayerMask whatIsBoss, whatIsEnemies;
    public float damage;

    public AmalgamBosHealthBar bossHealthBar;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (timeBtwAttacks <= 0)
        {
            StartCoroutine(Attack());
            timeBtwAttacks = starTimeBtwAttacks;
        }
        else
        {
            timeBtwAttacks -= Time.deltaTime;
        }

        if (Vector2.Distance(transform.position, target.position) < attackArea && (anim.GetBool("attacking") == false))
        {
            if (timeBtwStomps <= 0)
            {
                anim.SetTrigger("stomp");
                timeBtwStomps = starTimeBtwStomps;
            }
            else
            {
                timeBtwStomps -= Time.deltaTime;
            }
        }
    }

    public void Stomp()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, attackArea, whatIsEnemies);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            if (enemy.gameObject.tag == "Player")
            {
                PlayerStats.actualLife -= damage;
                StartCoroutine(target.GetComponent<Lifebar>().Damage());
                //StartCoroutine(target.GetComponent<PlayerMovement>().Knockback(0.2f, 5f, this.transform));
                FindObjectOfType<AudioManager>().Play("Damage");
            }
            else
            {
                return;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" && collision.IsTouchingLayers(whatIsBoss))
        {
            AmalgamBosHealthBar.actualLife -= PlayerStats.bulletDamage;
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
            anim.SetBool("attacking", true);

            yield return null;
        }
        anim.SetBool("attacking", false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackArea);
    }
}