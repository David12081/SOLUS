using UnityEngine;

public class EnemyLong : MonoBehaviour
{
    private float timeBtwShots;
    public float starTimeBtwShots;

    public GameObject projectile;
    private Transform player;

    public Animator anim;
    public Transform shootPoint;
    float distance;
    public float shootArea;
    public float damage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = starTimeBtwShots;
    }

    public void Update()
    {
        distance = Vector2.Distance(transform.position, player.position);

        if (distance < shootArea)
        {
            if (timeBtwShots <= 0)
            {
                anim.SetTrigger("attack");
                timeBtwShots = starTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    public void Shoot()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shootPoint.position, shootArea);
    }
}