using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountUp : MonoBehaviour
{
    public Text scoreText;
    public GameObject Button;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(ScoreAnimation(0f, 10f, 2f));
    }

    // スコアをアニメーションさせる
    private IEnumerator ScoreAnimation(float startScore, float endScore, float duration)
    {
        // 開始時間
        float startTime = Time.time;

        // 終了時間
        float endTime = startTime + duration;

        do
        {
            // 現在の時間の割合
            float timeRate = (Time.time - startTime) / duration;

            // 数値を更新
            float updateValue = (float)((endScore - startScore) * timeRate + startScore);

            // テキストの更新
            scoreText.text = updateValue.ToString("f0"); // （"f0" の "0" は、小数点以下の桁数指定）

            // 1フレーム待つ
            yield return null;

        } while (Time.time < endTime);

        // 最終的な着地のスコア
        scoreText.text = endScore.ToString();
        scoreText.fontSize = 30;
        scoreText.color = new Color(255f, 209f, 0);
        Instantiate(Button,transform.position,transform.rotation);
    }
}
