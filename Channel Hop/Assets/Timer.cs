using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    private bool isRunning = true;
    void Update()
    {
        elapsedTime += Time.deltaTime;
       
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("Elapsed Time: {0:00}:{1:00}", minutes, seconds);
    }
    public void StopTimer()
    {
        isRunning = false;
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
