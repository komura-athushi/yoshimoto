using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyonPyon : MonoBehaviour
{
    bool pyonpyon = false;
    bool jump = false;
    int count = 0;
    float time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (pyonpyon)
        {
            if (jump)
            {
                transform.position -= new Vector3(0f, 1f, 0f);
                if (time >= 0.1f)
                {
                    count++;
                    time = 0.0f;
                    jump = false;
                }
            }
            else
            {
                transform.position += new Vector3(0f, 1f, 0f);
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
                pyonpyon = true;
            }
        }
    }
}
