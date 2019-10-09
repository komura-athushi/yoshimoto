using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountUp : MonoBehaviour
{
    public Text scoreText;
    public bool flag;

    public AudioSource dararara;
    public AudioSource deden;
    // Use this for initialization
    void Start()
    {
        flag = false;
        StartCoroutine(ScoreAnimation(0f, SheepCount.SHEEPCOUNT, 2f));
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator ScoreAnimation(float startScore, float endScore, float duration)
    {
        // 開始時間
        float startTime = Time.time;

        // 終了時間
        float endTime = startTime + duration;
        dararara.Play();
        do
        {
            // 現在の時間の割合
            float timeRate = (Time.time - startTime) / duration;

            //float u = (float)((endScore - startScore) * timeRate + startScore);
            float threedigit = Random.Range(0, 9);
            float twodigit = Random.Range(0, 9);
            float onedigit = Random.Range(0, 9);
            // テキストの更新
            scoreText.text = threedigit.ToString() + twodigit.ToString() + onedigit.ToString();

            // 1フレーム待つ
            yield return null;

        } while (Time.time < endTime);
        dararara.Stop();
        deden.Play();
        // 最終的な着地のスコア
        scoreText.text = endScore.ToString();
        scoreText.fontSize = 24;
        //scoreText.color = new Color(255f, 209f, 0);
        flag = true;
    }
}