using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTrip : MonoBehaviour
{
    bool m_xPlus = true;  // x 軸プラス方向に移動中か？
    float time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (m_xPlus)
        {
            transform.position += new Vector3(6f * Time.deltaTime, 0f, 0f);
            if (time >= 3.0f)
            {
                time = 0.0f;
                m_xPlus = false;
            }
        }
        else
        {
            transform.position -= new Vector3(6f * Time.deltaTime, 0f, 0f);
            if (time >= 3)
            {
                time = 0.0f;
                m_xPlus = true;
            }
        }
    }
}
