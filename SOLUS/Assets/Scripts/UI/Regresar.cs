using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Regresar : MonoBehaviour
{
    public void Scene1()
    {
        PlayerStats.instance.bossIndex = 0;
        PlayerStats.movementSpeed = 8f;
        PlayerStats.attackDamage = 4.1f;
        PlayerStats.bulletDamage = 4.1f;
        PlayerStats.fireDelay = 1f;
        PlayerStats.maxLife = 100f;
        PlayerStats.actualLife = PlayerStats.maxLife;
        PlayerStats.hasCreatureShooter = false;
        PlayerStats.hasCreatureMelee = false;
        PlayerStats.hasCreatureMid = false;
        PlayerStats.hasCreatureGloves = false;
        PlayerStats.ScoreNum = 0;

        SceneManager.LoadScene("Room3");
        FindObjectOfType<AudioManager>().Play("Button");
    }
}