using UnityEngine;

public class StaffProjectile : MonoBehaviour
{
    private float radius;
    private int damage;

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
}
