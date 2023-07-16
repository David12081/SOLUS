using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    static System.Random ran = new System.Random();

    public GameObject lootDrop;
    public int maxCoins;
    public int minCoins;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(HandleDeath());
        }
    }
     
    public int GenerateRnd()
    {
        return ran.Next(minCoins, maxCoins);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            health -= PlayerStats.bulletDamage;
            Destroy(collision.gameObject);
            StartCoroutine(Damage());
            if(health > 0)
            {
                FindObjectOfType<AudioManager>().Play("EnemyDamage");
            }
            else if(health <= 0)
            {
                FindObjectOfType<AudioManager>().Play("Explosion");
            }
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

    IEnumerator HandleDeath()
    {
        anim.SetBool("death", true);
        yield return new WaitForSeconds(0.6f);

        Destroy(this.gameObject);
        for (int i = 0; i < GenerateRnd(); i++)
        {
            Instantiate(lootDrop, transform.position, Quaternion.identity);
        }
    }
}