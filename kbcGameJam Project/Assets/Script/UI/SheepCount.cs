using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheepCount : MonoBehaviour
{
    aboutmove about;
    int count;
    public Text numText;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        SheepnumCount();
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sheep"))
        {
            Debug.Log("羊じゃい！");
            count++;
            SheepnumCount();
        }
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("犬じゃい!");
        }
    }
    void SheepnumCount()
    {
        numText.text = string.Format("捕まえた羊の数:{0}", count);
    }
}
