using UnityEngine;
using UnityEngine.UI;

public class SlimeBossHealthBar : MonoBehaviour
{
    public Image lifeBar, blackLifeBar;
    public static float actualLife;
    private float maxLife;

    public GameObject boss;
    private bool inArea;

    static System.Random ran = new System.Random();
    public GameObject lootDrop;
    public GameObject floorChange;
    public int maxCoins;
    public int minCoins;

    [HideInInspector]
    public bool inCriticalState = false;

    private float criticalHealth;

    [HideInInspector]
    public float halfHealth;
    public Animator anim;
    private GameObject floor;

    private void Start()
    {
        maxLife = 50f;
        actualLife = maxLife;

        inCriticalState = false;

        criticalHealth = actualLife * 0.15f;
        halfHealth = actualLife / 2f;

        lifeBar.color = Color.red;
    }

    void Update()
    {
        lifeBar.fillAmount = actualLife / maxLife;

        if (inArea == true)
        {
            lifeBar.enabled = true;
            blackLifeBar.enabled = true;
        }
        else
        {
            lifeBar.enabled = false;
            blackLifeBar.enabled = false;
        }

        if (actualLife <= 0)
        {
            anim.SetTrigger("death");
            Instantiate(floorChange, GameObject.FindGameObjectWithTag("BossRoom").transform.position, Quaternion.identity);
            for (int i = 0; i < GenerateRnd(); i++)
            {
                Instantiate(lootDrop, transform.position, Quaternion.identity);
            }
            this.enabled = false;
            Destroy(boss, 1.7f);
        }

        if (actualLife <= criticalHealth)
        {
            inCriticalState = true;
        }

        if (inCriticalState == true)
        {
            lifeBar.color = Color.yellow;
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

    public int GenerateRnd()
    {
        return ran.Next(minCoins, maxCoins);
    }
}