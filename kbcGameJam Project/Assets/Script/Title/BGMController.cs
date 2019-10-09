using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    //シングルトン
    private static GameObject m_instance = null;
  
    public static GameObject Instance
    {
        get { return m_instance; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Awake()
    {
        if (m_instance != null && m_instance != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
