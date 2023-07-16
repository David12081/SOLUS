using System.Collections;
using UnityEngine;

public class SlimeMiniboss : MonoBehaviour
{
    public float speed;
    private Transform target;
    public float chaseArea, attackArea;
    public float damage;
    public LayerMask whatIsEnemies, whatIsBoss;
    private float timeBtwAttacks;
    public float starTimeBtwAttacks;

    private Rigidbody2D rb;
    public Animator anim;
    public SlimeBossHealthBar healthBar;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (healthBar.inCriticalState == false)
        {
            if (SlimeBossHealthBar.actualLife > 0)
            {
                if (Vector2.Distance(transform.position, target.position) < chaseArea)
                {
                    ChasePlayer();
                }

                if (SlimeBossHealthBar.actualLife <= healthBar.halfHealth)
                {
                    speed = 8f;
                    anim.SetBool("attacking", true);
                }
            }

            if (Vector2.Distance(transform.position, target.position) < attackArea)
            {
                if (timeBtwAttacks <= 0)
                {
                    anim.SetTrigger("attack");
                    timeBtwAttacks = starTimeBtwAttacks;
                }
                else
                {
                    timeBtwAttacks -= Time.deltaTime;
                    StartCoroutine(ResetSpeed());
                }
            }
        }
        else
        {
            anim.SetBool("critical", true);
        }
    }

    public void Attack()
    {
        speed = 10f;

        if (SlimeBossHealthBar.actualLife <= healthBar.halfHealth)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }

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

    public void Opening()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public void ChasePlayer()
    {
        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        rb.MovePosition(temp);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" && collision.IsTouchingLayers(whatIsBoss))
        {
            SlimeBossHealthBar.actualLife -= PlayerStats.bulletDamage;
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

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(0.1f);
        speed = 1f;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackArea);
    }
}