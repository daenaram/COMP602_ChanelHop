using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 4f;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogWarning("CanvasGroup missing on ObjectivePanel!");
            return;
        }
        StartCoroutine(FadeOut());
    }

     public IEnumerator FadeOut()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            yield return null;
        }

        // Only disable THIS panel, not its parent
        gameObject.SetActive(false);

    }
}

