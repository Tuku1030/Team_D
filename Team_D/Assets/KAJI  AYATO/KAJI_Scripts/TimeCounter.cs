using System;
using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
   
   public Text TimerText;
    float LimitTime = 180; // 制限時間

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //カウントダウン
        LimitTime -= Time.deltaTime;

        if (LimitTime < 0)
        {
            LimitTime = 0;
        }

        TimerText.text = LimitTime.ToString("F0"); // 残り時間を整数で表示
    }
}
