using System.Collections;
using UnityEngine;

public class PlayerGloves : MonoBehaviour
{
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    private float attackDamage;

    private float timeBtwAttacks;
    public float starTimeBtwAttacks;

    public Animator anim;

    void Update()
    {
        attackDamage = PlayerStats.attackDamage + 2f;

        if (timeBtwAttacks <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("Attack");
                timeBtwAttacks = starTimeBtwAttacks;
            }
        }
        else
        {
            timeBtwAttacks -= Time.deltaTime;
        }
    }

    public void AttackGloves()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            if (enemy.gameObject.tag == "Enemy" || enemy.gameObject.tag == "Enemy2" || enemy.gameObject.tag == "Enemy3")
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
                StartCoroutine(Damage(enemy));
                if (enemy.GetComponent<EnemyHealth>().health > 0)
                {
                    FindObjectOfType<AudioManager>().Play("EnemyDamage");
                }
                else if (enemy.GetComponent<EnemyHealth>().health <= 0)
                {
                    FindObjectOfType<AudioManager>().Play("Explosion");
                }
            }
            else if (enemy.gameObject.tag == "Boss" || enemy.gameObject.tag == "Boss2" || enemy.gameObject.tag == "Boss3" || enemy.gameObject.tag == "Boss4")
            {
                BossHealthBar.actualLife -= PlayerStats.attackDamage;
                PanteraBossHealthBar.actualLife -= PlayerStats.attackDamage;
                SlimeBossHealthBar.actualLife -= PlayerStats.attackDamage;
                AmalgamBosHealthBar.actualLife -= PlayerStats.attackDamage;
                StartCoroutine(Damage(enemy));
                FindObjectOfType<AudioManager>().Play("EnemyDamage");
            }
            else
            {
                return;
            }
        }
    }

    IEnumerator Damage(Collider2D enemy)
    {
        if (enemy != null)
        {
            for (int i = 0; i < 3; i++)
            {
                enemy.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                yield return new WaitForSeconds(0.1f);
                if (enemy != null)
                {
                    enemy.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    yield return new WaitForSeconds(0.1f);
                }
                else { yield break; }
            }
        }
        else { yield break; }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}