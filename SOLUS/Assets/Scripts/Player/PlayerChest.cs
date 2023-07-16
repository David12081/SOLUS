using UnityEngine;

[System.Serializable]

public struct PowerUpStruct
{
    public GameObject[] PowerUp;
}

public class PlayerChest : MonoBehaviour
{
    private GameObject chest;
    private bool InArea;

    private int powerUpIndex;
    private int powerUpSetIndex;
    private int chestPrice;

    public Animator anim;
    public SpriteRenderer ekey;

    public PowerUpStruct[] PowerUpSet = new PowerUpStruct[3];

    private void Start()
    {
        InArea = false;
        ekey.enabled = false;
    }

    void Update()
    {
        if(InArea == true)
        {
            ekey.enabled = true;
        }
        else
        {
            ekey.enabled = false;
        }

        if (InArea && Input.GetKeyDown(KeyCode.E) && (Score.ScoreNum >= chestPrice))
        {
            FindObjectOfType<AudioManager>().Play("Chest");
            InArea = false;
            PickRandomNumber();

            Instantiate(PowerUpSet[powerUpSetIndex].PowerUp[powerUpIndex], chest.transform.position, Quaternion.identity);

            PlayerStats.ScoreNum -= chestPrice;
            Destroy(chest.gameObject);
        }
        else if(InArea && Input.GetKeyDown(KeyCode.E) && (Score.ScoreNum < chestPrice))
        {
            FindObjectOfType<AudioManager>().Play("Locked");
            anim.SetTrigger("Locked");
        }
    }

    private void PickRandomNumber()
    {
        int randomNum = Random.Range(0, 2);

        powerUpIndex = randomNum;
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Chest")
        {
            InArea = true;
            chest = collider.gameObject;
            powerUpSetIndex = 0;
            chestPrice = 100;
        }

        if (collider.gameObject.tag == "Chest+")
        {
            InArea = true;
            chest = collider.gameObject;
            powerUpSetIndex = 1;
            chestPrice = 150;
        }

        if (collider.gameObject.tag == "Chest++")
        {
            InArea = true;
            chest = collider.gameObject;
            powerUpSetIndex = 2;
            chestPrice = 200;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InArea = false;
    }
}