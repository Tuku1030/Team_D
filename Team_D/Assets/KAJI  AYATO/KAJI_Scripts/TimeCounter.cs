using System;
using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
   
   public Text TimerText;
    float LimitTime = 180; // ��������

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�J�E���g�_�E��
        LimitTime -= Time.deltaTime;

        if (LimitTime < 0)
        {
            LimitTime = 0;
        }

        TimerText.text = LimitTime.ToString("F0"); // �c�莞�Ԃ𐮐��ŕ\��
    }
}
