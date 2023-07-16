using UnityEngine;

public class HealthRecovItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStats.actualLife += 20f;
            FindObjectOfType<AudioManager>().Play("Item");
            if (PlayerStats.actualLife >= PlayerStats.maxLife)
            {
                PlayerStats.actualLife = PlayerStats.maxLife;
                Destroy(this.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}