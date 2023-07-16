using UnityEngine;

public class EnemyRetreat : MonoBehaviour
{
    public float speed;
    private Transform target;
    public float retreatArea, spawnArea;
    public float damage;

    private Rigidbody2D rb;

    public Animator anim;
    private EnemyHealth enemy;

    private float timeBtwAttacks;
    public float starTimeBtwAttacks;
    public GameObject sushi, miniSushi;
    public Transform spawn1, spawn2, spawn3;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb = this.gameObject.GetComponent<Rigidbody2D>();

        enemy = GetComponentInChildren<EnemyHealth>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < spawnArea)
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

    private void FixedUpdate()
    {
        if (enemy.health > 0)
        {
            if (Vector2.Distance(transform.position, target.position) < retreatArea)
            {
                ChasePlayer();
            }
        }

        if(enemy.health <= 0)
        {
            Destroy(gameObject, 0.6f);
        }
    }

    public void ChasePlayer()
    {
        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        rb.MovePosition(temp);
    }

    public void SpawnSushi()
    {
        Instantiate(sushi, spawn1.position, Quaternion.identity);
        Instantiate(miniSushi, spawn2.position, Quaternion.identity);
        Instantiate(miniSushi, spawn3.position, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnArea);
    }
}