using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    //public static event Action OnPlayerDeath;

    [Header("Compnent")]
    public TextMeshProUGUI scoreText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    [Header("Limit")]
    public bool hasLimit;
    public float timerLimit;


    public bool updateTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        updateTimer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (updateTimer == true)
        {
            currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

            if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
            {
                currentTime = timerLimit;
                SetTimerText();
                scoreText.color = Color.red;
                enabled = false;
            }

            SetTimerText();
        }
    }

    private void SetTimerText()
    {
        scoreText.text = currentTime.ToString("000");
    }

}