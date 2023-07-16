using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorChange : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(PlayerStats.instance.bossIndex != 2)
            {
                PlayerStats.instance.bossIndex++;
                anim.SetTrigger("FadeOut");
                Invoke(nameof(LoadScene), 1f);
            }
            else if (PlayerStats.instance.bossIndex == 2)
            {
                PlayerStats.instance.bossIndex++;
                anim.SetTrigger("FadeOut");
                Invoke(nameof(LoadScene2), 1f);
            }
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Room3");
    }

    void LoadScene2()
    {
        SceneManager.LoadScene("Room4");
    }
}