using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour
{
    [SerializeField] GameObject objectivePanel;
    private CanvasGroup canvasGroup;
    public float objectiveTime = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(objectivePanel, objectiveTime);
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component not found on this GameObject.");
            return;
        }
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeOut()
    {
        float timer = 0f;
        while (timer < objectiveTime)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / objectiveTime);
            yield return null;
        }
        // Optionally, disable or destroy the UI element after fading out
        gameObject.SetActive(false);
    }
}
