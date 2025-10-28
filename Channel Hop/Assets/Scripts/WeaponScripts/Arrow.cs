using UnityEngine;

public class Arrow : MonoBehaviour
{
    private int damage;
    private float arrowSpeed = 10f;

    public void Initialize(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Try getting Health component instead of IDamageable
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log($"Arrow hit enemy for {damage} damage");
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(float direction)
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * direction * arrowSpeed;
        
        // Flip sprite based on direction
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.flipX = direction < 0;
        }
    }
}
