using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Lifebar : MonoBehaviour
{
    public Image lifeBar;
    private float actualLife;
    private float maxLife;
    
    [HideInInspector]
    public bool invincible;

    public Animator anim;
    public PlayerMovement playerMovement;
    public PlayerScript playerScript;
    public Rigidbody2D rb;
    public BoxCollider2D col;

    private void Start()
    {
        maxLife = PlayerStats.maxLife;
        actualLife = maxLife;

        invincible = false;
        rb.isKinematic = false;
        col.enabled = true; ;
    }

    public void Update()
    {
        actualLife = PlayerStats.actualLife;
        maxLife = PlayerStats.maxLife;
        
        lifeBar.fillAmount = actualLife / maxLife;

        if(actualLife <= 0)
        {
            anim.SetBool("death", true);
            playerMovement.enabled = false;
            playerScript.enabled = false;
            rb.isKinematic = true;
            col.enabled = false;
            Invoke(nameof(HandleDeath), 2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy" && !invincible)
        {
            PlayerStats.actualLife -= col.gameObject.GetComponent<EnemyFollow>().damage;
            StartCoroutine(Damage());
            FindObjectOfType<AudioManager>().Play("Damage");
        }
        else if (col.gameObject.tag == "Enemy2" && !invincible)
        {
            PlayerStats.actualLife -= col.gameObject.GetComponent<EnemyRetreat>().damage;
            StartCoroutine(Damage());
            FindObjectOfType<AudioManager>().Play("Damage");
        }
        else if (col.gameObject.tag == "Enemy3" && !invincible)
        {
            PlayerStats.actualLife -= col.gameObject.GetComponent<EnemyLong>().damage;
            StartCoroutine(Damage());
            FindObjectOfType<AudioManager>().Play("Damage");
        }
        else if(col.gameObject.tag == "Boss" && !invincible)
        {
            PlayerStats.actualLife -= col.gameObject.GetComponent<Miniboss>().damage;
            StartCoroutine(Damage());
            FindObjectOfType<AudioManager>().Play("Damage");
        }
        else if (col.gameObject.tag == "Boss2" && !invincible)
        {
            PlayerStats.actualLife -= col.gameObject.GetComponent<SlimeMiniboss>().damage;
            StartCoroutine(Damage());
            FindObjectOfType<AudioManager>().Play("Damage");
        }
        else if (col.gameObject.tag == "Boss3" && !invincible)
        {
            PlayerStats.actualLife -= col.gameObject.GetComponent<PanteraMiniboss>().damage;
            StartCoroutine(Damage());
            FindObjectOfType<AudioManager>().Play("Damage");
        }
        else if (col.gameObject.tag == "Boss4" && !invincible)
        {
            PlayerStats.actualLife -= col.gameObject.GetComponent<AmalgamBoss>().damage;
            StartCoroutine(Damage());
            FindObjectOfType<AudioManager>().Play("Damage");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !invincible)
        {
            PlayerStats.actualLife -= 4f;
            StartCoroutine(Damage());
            FindObjectOfType<AudioManager>().Play("Damage");
        }
    }

    public IEnumerator Damage()
    {
        invincible = true;
        for (int i = 0; i < 5; i++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        invincible = false;
    }

    void HandleDeath()
    {
        SceneManager.LoadScene("Game Over");
    }
}