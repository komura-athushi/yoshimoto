using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    float alfa;
    float red, green, blue;
    float count = 0;
    bool start = false;
    bool se = false;
    AudioSource sound;

    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        //if (Input.GetKeyDown("joystick button 0"))
        if (Input.GetKeyDown(KeyCode.A))
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
        if (count > 3.0f)
        {
            SceneManager.LoadScene("Game Scene");
        }
    }
}
