using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTeleport : MonoBehaviour
{
    private Transform playerTransform;
    private Transform spawnPoint;
    //private CameraFollow cameraFollow;
    private Transform squiddy;
    public Animator anim;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        spawnPoint = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Transform>();

        //cameraFollow = GameObject.Find("CameraHolder").GetComponent<CameraFollow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerTransform.position = spawnPoint.position;
            //cameraFollow.minPosition.x = -14;
            //cameraFollow.minPosition.y = -21;
            //cameraFollow.maxPosition.x = 14;
            //cameraFollow.maxPosition.y = 21;
            anim.SetTrigger("FadeOut");
            Invoke(nameof(FadeOut), 1f);

            if (PlayerStats.hasCreatureShooter == true)
            {
                squiddy = GameObject.Find("Squiddy(Clone)").GetComponent<Transform>();
                squiddy.position = playerTransform.position;
            }
        }
    }

    void FadeOut()
    {
        anim.SetTrigger("FadeIn");
    }
}