using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
   
   public Text TimerText;   //残り時間を表示するテキスト
    float LimitTime = 180f; // 制限時間
    private bool IsCounting = true; //カウント中かどうかを判定する

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //制限時間が０より大きく、カウント中なら
        if(IsCounting && LimitTime  > 0)
        {
            //経過時間分、制限時間を減らす
            LimitTime -= Time.deltaTime;
            UpdateText();
        }

        //時間が０以下になったらカウントを止める
        else if(LimitTime <= 0)
        {
            
            LimitTime = 0; 
            IsCounting = false;
            UpdateText ();
        }

        ////カウントダウン
        //LimitTime -= Time.deltaTime;

        //if (LimitTime < 0)
        //{
        //    LimitTime = 0;
        //}

        //TimerText.text = LimitTime.ToString("F0"); // 残り時間を整数で表示
    }

    void UpdateText()
    {
        //残り時間を分と秒に変換
        int Minutes = Mathf.FloorToInt(LimitTime / 60f);
        int Seconds = Mathf.FloorToInt(LimitTime % 60f);
        TimerText.text = String.Format("{0:00}:{1:00}", Minutes, Seconds);
    }
}
