using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    public Text timerText;
    public Text startText;
    public Text endText;
    public float totalTime;
    public Image fade; 

    int minute;
    int seconds;
    bool m_isStart = false;
    bool m_isEnd = false;
 
    float m_startMinute = 4.0f;
    int m_textScale = 0;
    float m_countScale = 0.0f;

    float m_endMinute = 3.0f;
    int m_textScale2 = 0;
    float m_countScale2 = 0.0f;

    bool m_isFade = false;
    float m_alpha = 0.0f;
    //始まった?
    public bool GetisStart()
    {
        return m_isStart;
    }
    //終わった?
    public bool GetisEnd()
    {
        return m_isEnd;
    }
    // Use this for initialization
    void Start()
    {
        m_textScale = startText.fontSize;
        m_countScale = (float)m_textScale;

        m_textScale2 = endText.fontSize;
        m_countScale2 = (float)m_textScale2;
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_isStart)
        {
            First();
        }
        else
        {
            if (!m_isEnd)
            {
                Second();
            }
            else
            {
                if (!m_isFade)
                {
                    Third();
                }
                else
                {
                    Fourth();
                }
            }
        }
    }

    void First()
    {
        timerText.enabled = false;
        startText.enabled = true;
        endText.enabled = false;

        int minute1 = (int)m_startMinute;
        m_startMinute -= Time.deltaTime;
        int minute2 = (int)m_startMinute;
        if (minute2 < 0)
        {
            m_isStart = true;
            return;
        }
        if (minute1 != minute2)
        {
            startText.fontSize = m_textScale;
            m_countScale = m_textScale;
        }
        else
        {
            m_countScale += Time.deltaTime * 50.0f;
            startText.fontSize = (int)m_countScale;
        }
        startText.text = minute2.ToString();
      
    }

    void Second()
    {
        timerText.enabled = true;
        startText.enabled = false;
        endText.enabled = false;

        totalTime -= Time.deltaTime;
        minute = (int)totalTime / 60;
        seconds = (int)totalTime - (minute * 60);
        timerText.text = "あと" + minute.ToString() + "分" + seconds.ToString() + "秒";
        if (minute == 0 && seconds == 0)
        {
            m_isEnd = true;
        }
    }
    void Third()
    {
        endText.enabled = true;

        m_endMinute -= Time.deltaTime;
        int minute = (int)m_endMinute;
     
        if(minute < 1)
        {
            if(minute < 0)
            {
                m_isFade = true;
            }
        }
        else
        {
            m_countScale2 += Time.deltaTime * 25.0f;
            endText.fontSize = (int)m_countScale2;
        }
    }

    void Fourth()
    {
        m_alpha += Time.deltaTime;
        fade.color = new Color(fade.color.r,fade.color.g,fade.color.b, m_alpha);
       
        if (fade.color.a >= 1.2f)
        {
           
            SceneManager.LoadScene("Title Scene");
        }
    }
}
