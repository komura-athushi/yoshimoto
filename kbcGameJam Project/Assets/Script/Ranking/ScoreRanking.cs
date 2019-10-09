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
        num = SheepCount.SHEEPCOUNT;
        for(int i = 0; i < score.Length; i++)
        {
            if(score[i] < num)
            {
                BubbleSort(i);
                score[i] = num;
                //Debug.Log("A");
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
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

    void BubbleSort(int number)
    {
        int memory = score[number];
        for(int i = number + 1; i < score.Length; i++)
        {
            int hoge = score[i];
            score[i] = memory;
            memory = hoge;
        }
    }
}
