using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;

    private void OnEnable()
    {
        Invoke("Destroy", 5f);
    }

    void Start()
    {
        moveSpeed = 8f;
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Invoke("Destroy", 0.001f);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}