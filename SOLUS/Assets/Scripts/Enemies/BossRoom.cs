using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class BossRoom : MonoBehaviour
{
    private bool inArea = false;
    public GameObject[] bossPrefab;
    public Image image;
    public Image[] images;
    public GameObject canvas;
    private GameObject cutscene;
    private PlayerMovement player;
    private PlayerScript playerScript;
    public Animator anim;
    private Canvas cutsceneCanvas;
    private PlayableDirector director;
    private float volume;

    private void Start()
    {
        canvas.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        volume = AudioManager.instance.music.volume;
    }

    private void Update()
    {
        if(inArea == true)
        {
            StartCoroutine(ShowIllustration());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inArea = true;
            FindObjectOfType<AudioManager>().Play("EnterBoss");
        }
    }

    IEnumerator ShowIllustration()
    {
        if(PlayerStats.instance.bossIndex == 0)
        {
            //anim.SetTrigger("fade");
            PlayerMovement.instance.source.volume = 0f;
            AudioManager.instance.music.volume = 0f;
            player.enabled = false;
            playerScript.enabled = false;
            image.sprite = images[0].sprite;
            canvas.SetActive(true);
            yield return new WaitForSeconds(3.5f);
            PlayerMovement.instance.source.volume = 1f;
            AudioManager.instance.music.volume = volume;
            player.enabled = true;
            playerScript.enabled = true;
            canvas.SetActive(false);
            Instantiate(bossPrefab[PlayerStats.instance.bossIndex], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (PlayerStats.instance.bossIndex == 1)
        {
            //anim.SetTrigger("fade");
            PlayerMovement.instance.source.volume = 0f;
            AudioManager.instance.music.volume = 0f;
            player.enabled = false;
            playerScript.enabled = false;
            image.sprite = images[1].sprite;
            canvas.SetActive(true);
            yield return new WaitForSeconds(3.5f);
            PlayerMovement.instance.source.volume = 1f;
            AudioManager.instance.music.volume = volume;
            playerScript.enabled = true;
            player.enabled = true;
            canvas.SetActive(false);
            Instantiate(bossPrefab[PlayerStats.instance.bossIndex], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (PlayerStats.instance.bossIndex == 2)
        {
            //anim.SetTrigger("fade");
            PlayerMovement.instance.source.volume = 0f;
            AudioManager.instance.music.volume = 0f;
            player.enabled = false;
            playerScript.enabled = false;
            image.sprite = images[2].sprite;
            canvas.SetActive(true);
            yield return new WaitForSeconds(3.5f);
            AudioManager.instance.music.volume = volume;
            PlayerMovement.instance.source.volume = 1f;
            playerScript.enabled = true;
            player.enabled = true;
            canvas.SetActive(false);
            Instantiate(bossPrefab[PlayerStats.instance.bossIndex], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if(PlayerStats.instance.bossIndex == 3)
        {
            cutsceneCanvas = GameObject.FindGameObjectWithTag("Cutscene").GetComponent<Canvas>();
            cutsceneCanvas.enabled = true;
            director = cutsceneCanvas.GetComponent<PlayableDirector>();
            director.Play();
            PlayerMovement.instance.source.volume = 0f;
            AudioManager.instance.music.volume = 0f;
            player.enabled = false;
            playerScript.enabled = false;
            yield return new WaitForSeconds(21f);
            cutsceneCanvas.enabled = false;
            director.Stop();
            PlayerMovement.instance.source.volume = 1f;
            AudioManager.instance.music.volume = volume;
            playerScript.enabled = true;
            player.enabled = true;
            Instantiate(bossPrefab[PlayerStats.instance.bossIndex], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}