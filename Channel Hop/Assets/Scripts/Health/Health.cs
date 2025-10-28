using System.Runtime.InteropServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    public bool dead;
    public GameOverScript gameOver;

    [Header("Components to disable on death")]
    [SerializeField] private Behaviour[] components;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;

    private Rigidbody2D rb;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameOver = Object.FindAnyObjectByType<GameOverScript>();
    }

    // Update is called once per frame
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // player hurt
            anim.SetTrigger("hurt");
            SFXManager.instance.PlaySound(hurtSound);
            // iframes
        }
        else
        {
            if (!dead)
            {
                // player dead
                anim.SetTrigger("die");
                SFXManager.instance.PlaySound(deathSound);
                dead = true;
                Debug.Log($"Dead called for {gameObject.name}");


                if (GetComponent<PlayerMovement>() != null)
                    GetComponent<PlayerMovement>().enabled = false;

                if (GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;

                if (GetComponent<MeleeEnemy>() != null)
                    GetComponent<MeleeEnemy>().enabled = false;



                BoxCollider2D box = GetComponent<BoxCollider2D>();
                if (box != null)
                    box.enabled = false;

                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                    rb.bodyType = RigidbodyType2D.Kinematic; // stop physics from moving the player
                }


            }

        }
    }


    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    public void Respawn()
    {
        Debug.Log($"Health called for {gameObject.name}");
        dead = false;

        // restore health
        SetHealth(startingHealth);

        // reset animator so the "die" pose/sprite is cleared
        if (anim != null)
        {
            anim.ResetTrigger("die");
            anim.Play("Idle");
        }

        // StartCoroutine(Invunerability());// this is optional

        /*foreach (Behaviour component in components)
            component.enabled = true; NOT YET IMPLEMENTED*/
        if (GetComponent<PlayerMovement>() != null)
            GetComponent<PlayerMovement>().enabled = true;

        if (GetComponentInParent<EnemyPatrol>() != null)
            GetComponentInParent<EnemyPatrol>().enabled = true;


        if (GetComponent<MeleeEnemy>() != null)
            GetComponent<MeleeEnemy>().enabled = true;


        // Re-enable collider
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box != null)
            box.enabled = true;

        // Reset Rigidbody velocity (important for player respawn)
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.linearVelocity = Vector2.zero;
        }
    }
    public void SetHealth(float value)
    {
        currentHealth = Mathf.Clamp(value, 0, startingHealth);
    }

}