using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
   
   public Text TimerText;
    float LimitTime = 180f; // ��������
    private bool IsCounting = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(IsCounting && LimitTime  > 0)
        {
            LimitTime -= Time.deltaTime;
            UpdateText();
        }

        else if(LimitTime <= 0)
        {
            LimitTime = 0; 
            IsCounting = false;
            UpdateText ();
        }

        ////�J�E���g�_�E��
        //LimitTime -= Time.deltaTime;

        //if (LimitTime < 0)
        //{
        //    LimitTime = 0;
        //}

        //TimerText.text = LimitTime.ToString("F0"); // �c�莞�Ԃ𐮐��ŕ\��
    }

    void UpdateText()
    {
        int Minutes = Mathf.FloorToInt(LimitTime / 60f);
        int Seconds = Mathf.FloorToInt(LimitTime % 60f);
        TimerText.text = String.Format("{0:00}:{1:00}", Minutes, Seconds);
    }
}
