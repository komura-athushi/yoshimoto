using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheepCount : MonoBehaviour
{
    public static int SHEEPCOUNT = 0;
    GameObject[] m_sheepList;
    public Text numText;
    public float DISTANCE;
    public Vector3 POSITION;
    // Start is called before the first frame update
    void Start()
    {
        SHEEPCOUNT = 0;
        SheepnumCount();
        m_sheepList = GameObject.FindGameObjectsWithTag("Sheep");
    }

    //Update is called once per frame
    void Update()
    {
        SheepnumCount();
        CaptureSheep();
    }
    void CaptureSheep()
    {
        foreach (GameObject sheep in m_sheepList)
        {
            SheepController sheepController = sheep.GetComponent<SheepController>();
            if(!sheepController.GetisCapture())
            {
                Vector3 distance = sheep.transform.position - POSITION;
                if(distance.sqrMagnitude <= DISTANCE * DISTANCE)
                {
                    SHEEPCOUNT += 1;
                    sheepController.SetisCapture();
                }
            }
        }
    }
    /*void OnTriggerEnter(Collider other)
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
    }*/
    void SheepnumCount()
    {
        numText.text = string.Format("捕まえた羊の数:{0}", SHEEPCOUNT);
    }
}
