using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject pauseCanvas;
    public PlayerMovement playerMovement;
    bool isPaused;
    public Animator anim;

    private void Start()
    {
        isPaused = false;
        playerMovement.enabled = true;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            isPaused = true;
            if (isPaused == true)
            {
                pauseCanvas.SetActive(true);
                Time.timeScale = 0;
                playerMovement.enabled = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            Time.timeScale = 1;
            pauseCanvas.SetActive(false);
            isPaused = false;
            playerMovement.enabled = true;
        }

        if(Input.GetKey(KeyCode.Tab))
        {
            anim.SetBool("Pressed", true);
        }
        else
        {
            anim.SetBool("Pressed", false);
        }
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        isPaused = false;
        playerMovement.enabled = true;
    }
}