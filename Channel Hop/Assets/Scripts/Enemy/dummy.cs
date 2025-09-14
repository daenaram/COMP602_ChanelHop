using UnityEngine;
using TMPro;

public class dummy : MonoBehaviour, IDamageable
{
    [Header("Dummy Settings")]
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float blinkDuration = 0.1f;
    [SerializeField] private int numberOfBlinks = 2;
    [SerializeField] private GameObject damageTextPrefab;
    [SerializeField] private Vector3 damageTextOffset = new Vector3(0, 1, 0);

    private int currentHealth;
    private SpriteRenderer spriteRenderer;
    private float blinkTimer;
    private int currentBlinks;
    private bool isBlinking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlinking)
        {
            HandleBlinking();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        StartBlinking();
        ShowDamageNumber(damage);
    }

    private void StartBlinking()
    {
        isBlinking = true;
        currentBlinks = 0;
        blinkTimer = blinkDuration;
    }

    private void HandleBlinking()
    {
        blinkTimer -= Time.deltaTime;

        if (blinkTimer <= 0)
        {
            blinkTimer = blinkDuration;
            spriteRenderer.enabled = !spriteRenderer.enabled;
            currentBlinks++;

            if (currentBlinks >= numberOfBlinks * 2) // * 2 because each blink is off and on
            {
                isBlinking = false;
                spriteRenderer.enabled = true;
            }
        }
    }

    private void ShowDamageNumber(int damage)
    {
        if (damageTextPrefab != null)
        {
            GameObject damageTextObj = Instantiate(damageTextPrefab, transform.position + damageTextOffset, Quaternion.identity);
            TextMeshPro damageText = damageTextObj.GetComponent<TextMeshPro>();
            if (damageText != null)
            {
                damageText.text = damage.ToString();
                Destroy(damageTextObj, 1f);
            }
        }
    }
}
