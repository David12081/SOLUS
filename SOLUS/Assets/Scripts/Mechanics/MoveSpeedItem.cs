using UnityEngine;

public class MoveSpeedItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("Item");
            PlayerStats.movementSpeed += 0.6f;
            Destroy(this.gameObject);
        }
    }
}