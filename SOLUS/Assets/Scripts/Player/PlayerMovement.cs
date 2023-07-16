using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    
    private float movementSpeed;
    Vector2 movement, normMovement;

    public Rigidbody2D rb;
    
    public Animator anim;

    public Transform attackAnchor;

    public SpriteRenderer melee;
    private Lifebar lifebar;

    Vector2 dashDir;
    float dashSpeed;
    
    [HideInInspector]
    public enum State
    {
        Normal,
        Rolling,
    }

    public State state;

    public Ghost ghost;

    public AudioSource source;

    public float knockbackPower;
    public float knockbackDuration;
    bool beingKnockedback = false;

    private void Awake()
    {
        instance = this;

        state = State.Normal;

        lifebar = GetComponent<Lifebar>();
    }

    private void Update()
    {
        switch (state)
        {
            case State.Normal:

                movementSpeed = PlayerStats.movementSpeed;

                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");

                normMovement = movement.normalized;

                anim.SetFloat("Horizontal", movement.x);
                anim.SetFloat("Vertical", movement.y);
                anim.SetFloat("Speed", movement.sqrMagnitude);

                if (movement.x != 0 || movement.y != 0)
                {
                    anim.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
                    anim.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
                }

                if(rb.velocity.magnitude > 1f)
                {
                    if(!source.isPlaying)
                    {
                        source.Play();
                    }
                }
                else
                {
                    source.Stop();
                }

                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    attackAnchor.localRotation = Quaternion.Euler(0, 0, 90);
                    melee.sortingOrder = 1;
                }
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    attackAnchor.localRotation = Quaternion.Euler(0, 0, -90);
                    melee.sortingOrder = 1;
                }
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    attackAnchor.localRotation = Quaternion.Euler(0, 0, 180);
                    melee.sortingOrder = 1;
                }
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    attackAnchor.localRotation = Quaternion.Euler(0, 0, 0);
                    melee.sortingOrder = 3;
                }

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    dashDir = normMovement;
                    dashSpeed = PlayerStats.movementSpeed * 10f;
                    state = State.Rolling;
                    FindObjectOfType<AudioManager>().Play("Dash");
                }
                break;

            case State.Rolling:

                lifebar.invincible = true;
                ghost.makeGhost = true;

                float dashSpeedDropMult = 5f;
                dashSpeed -= dashSpeed * dashSpeedDropMult * Time.deltaTime;

                float dashSpeedMinimum = 50f;
                if(dashSpeed < dashSpeedMinimum)
                {
                    state = State.Normal;
                    lifebar.invincible = false;
                    ghost.makeGhost = false;
                }
                break;
        }
    }

    private void FixedUpdate()
    {
        switch(state)
        {
            case State.Normal:
                rb.MovePosition(rb.position + movementSpeed * Time.fixedDeltaTime * normMovement);

                if (!beingKnockedback)
                {
                    rb.velocity = normMovement * movementSpeed;
                }
                break;

            case State.Rolling:
                rb.velocity = dashDir * dashSpeed;
                break;
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy") && lifebar.invincible == false)
        {
            //StartCoroutine(Knockback(knockbackDuration, knockbackPower, col.transform));
            Knockback(knockbackDuration, knockbackPower, col.transform);
        }
        else if(col.gameObject.CompareTag("Boss") && lifebar.invincible == false)
        {
            //StartCoroutine(Knockback(knockbackDuration, knockbackPower, col.transform));
            Knockback(knockbackDuration, knockbackPower, col.transform);
        }
        else if (col.gameObject.CompareTag("Boss2") && lifebar.invincible == false)
        {
            //StartCoroutine(Knockback(knockbackDuration, knockbackPower, col.transform));
            Knockback(knockbackDuration, knockbackPower, col.transform);
        }
        else if (col.gameObject.CompareTag("Boss3") && lifebar.invincible == false)
        {
            //StartCoroutine(Knockback(knockbackDuration, knockbackPower, col.transform));
            Knockback(knockbackDuration, knockbackPower, col.transform);
        }
        else if (col.gameObject.CompareTag("Boss4") && lifebar.invincible == false)
        {
            //StartCoroutine(Knockback(knockbackDuration, knockbackPower, col.transform));
            Knockback(knockbackDuration, knockbackPower, col.transform);
        }
    }

    public void Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        float timer = 0;
        beingKnockedback = true;

        while (timer < knockbackDuration)
        {
            timer += Time.fixedDeltaTime;

            Vector2 direction = obj.transform.position - this.transform.position;
            direction = direction.normalized * knockbackPower;
            rb.AddForce(5f * knockbackPower * -direction);
        }
        beingKnockedback = false;
    }
}