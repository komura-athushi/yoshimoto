using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyonPyon : MonoBehaviour
{
    bool pyonpyon;
    bool jump;
    int count;
    float time;
    AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        pyonpyon = false;
        jump = false;
        count = 0;
        time = 0.0f;
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (pyonpyon)
        {
            if (jump)
            {
                transform.position -= new Vector3(0f, 100f * Time.deltaTime, 0f);
                if (time >= 0.1f)
                {
                    count++;
                    time = 0.0f;
                    jump = false;
                }
            }
            else
            {
                transform.position += new Vector3(0f, 100f * Time.deltaTime, 0f);
                if (time >= 0.1f)
                {
                    time = 0.0f;
                    jump = true;
                }
            }
            if (count >= 2)
            {
                count = 0;
                pyonpyon = false;
            }
        }
        else
        {
            if (time >= Random.Range(1.0f, 10.0f))
            {
                time = 0.0f;
                sound.Play();
                pyonpyon = true;
            }
        }
    }
}
