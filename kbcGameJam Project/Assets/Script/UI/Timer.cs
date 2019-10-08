using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;

    public float totalTime;
    int minute;
    int seconds;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        totalTime -= Time.deltaTime;
        minute = (int)totalTime / 60;
        seconds = (int)totalTime - (minute * 60);
        timerText.text = "あと"+ minute.ToString()+"分" + seconds.ToString() + "秒";
        if(minute == 0&&seconds == 0)
        {
            timerText.text = "終了！";
            totalTime = 0;
        }
    }
}
