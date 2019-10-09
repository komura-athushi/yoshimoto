using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultFadeOut : MonoBehaviour
{
    float alfa;
    float red, green, blue;
    float count;
    bool start;
    bool se;
    AudioSource sound;
    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
        count = 0.0f;
        start = false;
        se = false;
        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        CountUp countUp = GameObject.FindObjectOfType<CountUp>();
        if (countUp.flag)
        {
            if (Input.GetKeyDown("joystick button 0"))
            {
                start = true;
            }
            if (start == true)
            {
                if (se == false)
                {
                    sound.Play();
                    se = true;
                }
                count += Time.deltaTime;
                GetComponent<Image>().color = new Color(red, green, blue, alfa);
                alfa += Time.deltaTime;
            }
            if (count > 2.3f)
            {
                SceneManager.LoadScene("Ranking Scene");
            }
        }
    }
}
