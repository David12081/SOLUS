using UnityEngine;

public class TameArea : MonoBehaviour
{
    private bool inArea;
    public BoxCollider2D col;
    public GameObject boss;

    private PlayerMovement movement;
    public GameObject choicePanel;
    public GameObject squidItem;
    public GameObject floorChange;

    public BossHealthBar bossHealthBar;
    private SpriteRenderer ekey;

    private void Start()
    {
        col.enabled = false;
        choicePanel.SetActive(false);
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        movement.enabled = true;

        ekey = GameObject.FindGameObjectWithTag("Ekey").GetComponent<SpriteRenderer>();
        ekey.enabled = false;
    }

    public void Update()
    {
        if(inArea == true)
        {
            ekey.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                movement.enabled = false;
                choicePanel.SetActive(true);
            }
        }
        else
        {
            ekey.enabled = false;
        }

        if(bossHealthBar.inCriticalState == true)
        {
            col.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inArea = false;
        }
    }

    public void TameBoss()
    {
        movement.enabled = true;
        Instantiate(floorChange, GameObject.FindGameObjectWithTag("BossRoom").transform.position, Quaternion.identity);
        Instantiate(squidItem, transform.position, Quaternion.identity);
        Destroy(boss);
        choicePanel.SetActive(false);
    }

    public void KillBoss()
    {
        choicePanel.SetActive(false);
        movement.enabled = true;
    }
}