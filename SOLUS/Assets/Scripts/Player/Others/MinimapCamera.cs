using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    private Transform playerTransform;

    private Vector3 followVelocity = Vector3.zero;
    public float followSpeed = 0.1f;
    private float timeBtwAttacks;
    public float starTimeBtwAttacks;

    public SpriteRenderer square;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        square.enabled = false;

        timeBtwAttacks = starTimeBtwAttacks;
    }

    private void Update()
    {
        FollowPlayer();

        if (timeBtwAttacks <= 0)
        {
            square.enabled = true;
        }
        else
        {
            timeBtwAttacks -= Time.deltaTime;
        }
    }

    public void FollowPlayer()
    {
        Vector3 targetPos = Vector3.SmoothDamp(transform.position, playerTransform.position, ref followVelocity, followSpeed);

        transform.position = targetPos;
    }
}