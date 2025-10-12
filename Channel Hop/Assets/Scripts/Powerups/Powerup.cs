// attach this script to game object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupEffect powerupEffect;

    // when player collides with fruit speed power up 


    private void OnTriggerEnter2D(Collider2D collision)
    {

        // check here for player/enemy
        // if block to apply speed buff

        Destroy(gameObject);
        powerupEffect.Apply(collision.gameObject);
    }
}