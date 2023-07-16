using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    //public Vector2 cameraChange;
    public Vector3 playerChange;
    private Transform squiddy;
    //private CameraFollow cam;
    public Animator anim;

    //private void Start()
    //{
    //    cam = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraFollow>();
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //cam.minPosition += cameraChange;
            //cam.maxPosition += cameraChange;
            collision.transform.position += playerChange;

            if(PlayerStats.hasCreatureShooter == true)
            {
                squiddy = GameObject.Find("Squiddy(Clone)").GetComponent<Transform>();
                squiddy.position += playerChange;
            }
            anim.SetTrigger("FadeOut");
            Invoke(nameof(FadeOut), 0f);
        } 
    }

    void FadeOut()
    {
        anim.SetTrigger("FadeIn");
    }
}