using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player1" || collision.tag == "Player2")
        {
            collision.GetComponentInParent<Health>()?.TakeDamage(damage);
        }
    }
}
