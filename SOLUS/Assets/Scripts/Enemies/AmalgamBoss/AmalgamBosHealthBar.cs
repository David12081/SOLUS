using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AmalgamBosHealthBar : MonoBehaviour
{
    public Image lifeBar, blackLifeBar;
    public static float actualLife;
    private float maxLife;

    public GameObject boss;
    private bool inArea;

    private void Start()
    {
        maxLife = 50f;
        actualLife = maxLife;

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
            SceneManager.LoadScene("Ending");
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
}