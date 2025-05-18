using System;
using UnityEngine;
using TMPro;

public class SnakeTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime;

    private void Start()
    {
        Destroy(gameObject, remainingTime);
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            remainingTime = Mathf.Max(remainingTime, 0); 
        }

        int seconds = Mathf.FloorToInt(remainingTime);
        timerText.text = string.Format("{0:00}", seconds);
    }
}