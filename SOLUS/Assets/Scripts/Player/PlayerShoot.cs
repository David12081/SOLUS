using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    private float fireDelay;

    private Transform anchorObj;
    private Transform shootPoint;

    private Transform target;
    public float stoppingDistance;
    public float maxDistance;
    private float speed;
    private float vel;

    private Animator anim;
    private Rigidbody2D rb;

    private void Start()
    {
        fireDelay = PlayerStats.fireDelay;

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        anim = GameObject.Find("Squiddy(Clone)").GetComponent<Animator>();

        rb = GameObject.Find("Squiddy(Clone)").GetComponent<Rigidbody2D>();

        speed = PlayerStats.movementSpeed - 2f;
    }

    private void FixedUpdate()
    {
        fireDelay = PlayerStats.fireDelay;

        anchorObj = GameObject.Find("Squiddy(Clone)").GetComponent<Transform>();
        shootPoint = GameObject.Find("ShootPoint").GetComponent<Transform>();

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVer = Input.GetAxis("ShootVertical");

        anim.SetFloat("moveX", shootHor);
        anim.SetFloat("moveY", shootVer);

        if((shootHor !=0 || shootVer != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHor, shootVer);
            lastFire = Time.time;

            FindObjectOfType<AudioManager>().Play("Bullet");

            if (shootHor != 0 && shootVer != 0)
            {
                anim.SetFloat("moveX", shootHor);
                anim.SetFloat("moveY", shootVer);
            }
        }

        if (anchorObj.transform.position == target.transform.position)
        {
            anchorObj.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
        }
        else
        {
            anchorObj.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
        }

        if (Vector2.Distance(anchorObj.transform.position, target.position) < stoppingDistance)
        {
            if(Vector2.Distance(anchorObj.transform.position, target.position) > maxDistance)
                {
                while(Vector2.Distance(anchorObj.transform.position, target.position) > maxDistance){
                    vel = speed;
                    vel = vel + 1f;

                    Vector3 temp = Vector3.MoveTowards(anchorObj.transform.position, target.position, vel * Time.deltaTime);
                    rb.MovePosition(temp);

                    if(shootHor == 0 && shootVer == 0)
                    {
                        ChangeAnim(transform.position - temp);
                    }
                }
            }
            else
            {
                Vector3 temp = Vector3.MoveTowards(anchorObj.transform.position, target.position, speed * Time.deltaTime);
                rb.MovePosition(temp);

                if (shootHor == 0 && shootVer == 0)
                {
                    ChangeAnim(transform.position - temp);
                }
            }
        }

        void Shoot(float x, float y)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
            bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
                (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
                0
                );
        }
    }
    
    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    private void ChangeAnim(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if(direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }
}