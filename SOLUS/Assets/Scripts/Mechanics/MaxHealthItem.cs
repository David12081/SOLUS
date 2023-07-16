using UnityEngine;
public class MaxHealthItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("Item");
            PlayerStats.maxLife += 25f;
            Destroy(this.gameObject);
        }
    }
}