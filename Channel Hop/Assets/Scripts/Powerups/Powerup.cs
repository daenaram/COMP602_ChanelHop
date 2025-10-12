using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupEffect powerupEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    { 

        // check if player is the collider with fruit
        if(collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            powerupEffect.Apply(collision.gameObject);
            Destroy(gameObject);
        }

    }
}