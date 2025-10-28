using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
   
   public Text TimerText;   //�c�莞�Ԃ�\������e�L�X�g
    float LimitTime = 180f; // ��������
    private bool IsCounting = true; //�J�E���g�����ǂ����𔻒肷��

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�������Ԃ��O���傫���A�J�E���g���Ȃ�
        if(IsCounting && LimitTime  > 0)
        {
            //�o�ߎ��ԕ��A�������Ԃ����炷
            LimitTime -= Time.deltaTime;
            UpdateText();
        }

        //���Ԃ��O�ȉ��ɂȂ�����J�E���g���~�߂�
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
        //�c�莞�Ԃ𕪂ƕb�ɕϊ�
        int Minutes = Mathf.FloorToInt(LimitTime / 60f);
        int Seconds = Mathf.FloorToInt(LimitTime % 60f);
        TimerText.text = String.Format("{0:00}:{1:00}", Minutes, Seconds);
    }
}
