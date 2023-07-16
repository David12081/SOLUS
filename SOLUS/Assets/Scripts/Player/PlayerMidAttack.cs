using System.Collections;
using UnityEngine;

public class PlayerMidAttack : MonoBehaviour
{
    public Transform axolotl, attackPos;
    public Transform pUp, pRight, pDown, pLeft, pupa, pra, pda, pla;
    public float attackRange;
    private float attackDamage;
    public LayerMask whatIsEnemies;

    private float timeBtwAttacks;
    public float starTimeBtwAttacks;
    Vector2 direction;
    public Animator anim;
    public SpriteRenderer mid;

    private void Update()
    {
        attackDamage = PlayerStats.attackDamage + 1f;
        direction.x = Input.GetAxis("ShootHorizontal");
        direction.y = Input.GetAxis("ShootVertical");

        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
        anim.SetFloat("speed", direction.sqrMagnitude);

        if (direction.x != 0 || direction.y != 0)
        {
            anim.SetFloat("lastMoveX", Input.GetAxis("ShootHorizontal"));
            anim.SetFloat("lastMoveY", Input.GetAxis("ShootVertical"));
        }

        if (Input.GetAxis("ShootHorizontal") > 0)
        {
            axolotl.localPosition = pRight.localPosition;
            attackPos.localPosition = pra.localPosition;
            mid.sortingOrder = 3;
            if (timeBtwAttacks <= 0)
            {
                anim.SetTrigger("attackRight");
                timeBtwAttacks = starTimeBtwAttacks;
            }
            else
            {
                timeBtwAttacks -= Time.deltaTime;
            }
        }
        if (Input.GetAxis("ShootHorizontal") < 0)
        {
            axolotl.localPosition = pLeft.localPosition;
            attackPos.localPosition = pla.localPosition;
            mid.sortingOrder = 3;
            if (timeBtwAttacks <= 0)
            {
                anim.SetTrigger("attackLeft");
                timeBtwAttacks = starTimeBtwAttacks;
            }
            else
            {
                timeBtwAttacks -= Time.deltaTime;
            }
        }
        if (Input.GetAxis("ShootVertical") > 0)
        {
            axolotl.localPosition = pUp.localPosition;
            attackPos.localPosition = pupa.localPosition;
            mid.sortingOrder = 1;
            if (timeBtwAttacks <= 0)
            {
                anim.SetTrigger("attackUp");
                timeBtwAttacks = starTimeBtwAttacks;
            }
            else
            {
                timeBtwAttacks -= Time.deltaTime;
            }
        }
        if (Input.GetAxis("ShootVertical") < 0)
        {
            axolotl.localPosition = pDown.localPosition;
            attackPos.localPosition = pda.localPosition;
            mid.sortingOrder = 3;
            if (timeBtwAttacks <= 0)
            {
                anim.SetTrigger("attackDown");
                timeBtwAttacks = starTimeBtwAttacks;
            }
            else
            {
                timeBtwAttacks -= Time.deltaTime;
            }
        }
    }

    public void Attack()
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