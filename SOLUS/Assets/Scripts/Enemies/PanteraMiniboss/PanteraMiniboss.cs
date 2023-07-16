using System.Collections;
using UnityEngine;

public class PanteraMiniboss : MonoBehaviour
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
    public PanteraBossHealthBar healthBar;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(healthBar.inCriticalState == false)
        {
            if (PanteraBossHealthBar.actualLife > 0)
            {
                if (Vector2.Distance(transform.position, target.position) < chaseArea)
                {
                    ChasePlayer();
                }

                if (PanteraBossHealthBar.actualLife <= healthBar.halfHealth)
                {
                    speed = 7f;
                    anim.SetBool("running", true);
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

    public void ChasePlayer()
    {
        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        rb.MovePosition(temp);
        ChangeAnim(temp - transform.position);
    }

    public void ChangeAnim(Vector2 direction)
    {
        if (direction.x > 0)
        {
            anim.SetFloat("moveX", 1f);
        }
        else if (direction.x < 0)
        {
            anim.SetFloat("moveX", -1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" && collision.IsTouchingLayers(whatIsBoss))
        {
            PanteraBossHealthBar.actualLife -= PlayerStats.bulletDamage;
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackArea);
    }
}