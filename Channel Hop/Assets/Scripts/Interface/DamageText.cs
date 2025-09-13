using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float fadeSpeed = 1f;

    private TextMeshPro text;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        // Float upward
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        // Fade out
        Color color = text.color;
        color.a -= fadeSpeed * Time.deltaTime;
        text.color = color;

        if (color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
