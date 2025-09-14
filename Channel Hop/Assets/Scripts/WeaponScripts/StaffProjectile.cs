using UnityEngine;

public class StaffProjectile : MonoBehaviour
{
    private float radius;
    private int damage;
    private float staffProjectileSpeed = 10f; // You can adjust this value as needed

    public void Initialize(float radius, int damage)
    {
        this.radius = radius;
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    IDamageable enemy = hitCollider.GetComponent<IDamageable>();
                    enemy?.TakeDamage(damage);
                }
            }
            Destroy(gameObject);
        }
    }

    public void SetDirection(float direction)
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * direction * staffProjectileSpeed;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.flipX = direction < 0;
        }
    }
}
