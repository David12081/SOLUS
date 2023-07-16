using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float ghostDelay;
    private float ghostDelaySeconds;
    public GameObject ghost;
    public bool makeGhost = false;

    private void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    private void Update()
    {
        if(makeGhost == true)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                GameObject currentGhost = Instantiate(ghost, transform.position, Quaternion.identity);
                Sprite currenSprite = GetComponent<SpriteRenderer>().sprite;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currenSprite;
                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, 0.6f);
            }
        }
    }
}