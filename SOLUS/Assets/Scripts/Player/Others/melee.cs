using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : MonoBehaviour
{
    void Update()
    {
        if(PlayerStats.hasCreatureMelee == true || PlayerStats.hasCreatureShooter == true || PlayerStats.hasCreatureMid == true || PlayerStats.hasCreatureGloves == true)
        {
            gameObject.SetActive(false);
        }
    }
}