using System.Runtime.InteropServices;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    public bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if(currentHealth > 0)
        {
            // player hurt
            anim.SetTrigger("hurt");
            // iframes
        } 
        else
        {
            if(!dead)
            {
                // player dead
                anim.SetTrigger("die");

                if(GetComponent<PlayerMovement>() != null)
                    GetComponent<PlayerMovement>().enabled = false;
                
                if(GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;

                if(GetComponent<MeleeEnemy>() != null)
                    GetComponent<MeleeEnemy>().enabled = false;

                dead = true;
            }
         
        }
    }

    // Add health (reference to HealthCollectible script)
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    //public void Respawn()
    //{
    //    Debug.Log("respawning health.cs");
    //    dead = false;
    //    AddHealth(startingHealth);
    //    anim.ResetTrigger("die");
    //    anim.Play("Idle");
    //    //StartCoroutine(Invunerability());// this is optional

    //    /*foreach (Behaviour component in components)
    //        component.enabled = true; NOT YET IMPLEMENTED*/
    //    if (GetComponent<PlayerMovement>() != null)
    //        GetComponent<PlayerMovement>().enabled = true;

    //    if (GetComponentInParent<EnemyPatrol>() != null)
    //        GetComponentInParent<EnemyPatrol>().enabled = true;

    //    if (GetComponent<MeleeEnemy>() != null)
    //        GetComponent<MeleeEnemy>().enabled = true;
    //}

    public bool isDead() { return dead; }
}
