using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreRanking : MonoBehaviour
{
    public static int[] score = { 0, 0, 0 };
    public int tmp;
    public int num;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        num = SheepCount.SHEEPCOUNT;
        if (num > score[0])
        {
            score[0] = num;
        }
        else if (num > score[1])
        {
            score[1] = num;
        }
        else if (num > score[2])
        {
            score[2] = num;
        }
        BubbleSort(score);
        GameObject score1 = GameObject.Find("Ranking1");
        Text score01 = score1.GetComponent<Text>();
        score01.text = score[0].ToString();
        GameObject score2 = GameObject.Find("Ranking2");
        Text score02=score2.GetComponent<Text>();
        score02.text = score[1].ToString();
        GameObject score3 = GameObject.Find("Ranking3");
        Text score03 = score3.GetComponent<Text>();
        score03.text = score[2].ToString();
    }

    void BubbleSort(int[] a)
    {

        for (int i = 0; i < a.Length - 1; i++)
        {
            if (a[i] < a[i + 1])
            {
                int tmp;
                tmp = a[i];
                a[i] = a[i + 1];
                a[i + 1] = tmp;
            }
        }
    }
}
