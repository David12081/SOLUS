using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomReveal : MonoBehaviour
{
    public Animator anim;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("fade");
            Destroy(gameObject, 0.2f);
        }
    }
}