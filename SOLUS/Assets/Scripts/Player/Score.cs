using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score;
    public static int ScoreNum;

    void Start()
    {
        ScoreNum = PlayerStats.ScoreNum;
        score.text = ScoreNum.ToString();
    }

    private void Update()
    {
        ScoreNum = PlayerStats.ScoreNum;
        score.text = ScoreNum.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            PlayerStats.ScoreNum++;
            FindObjectOfType<AudioManager>().Play("Coin");
            Destroy(collision.gameObject);
        }
    }
}