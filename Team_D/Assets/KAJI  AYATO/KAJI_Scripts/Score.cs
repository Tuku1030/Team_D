using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    private int _Score; �@  //���_�̕ϐ�
    public Text ScoreText;  //���_�̕����̕ϐ�

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Score = 0; //���_���O�ɂ���
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score:" + _Score.ToString();

        if (Input.GetKey(KeyCode.P)) //�����A�X�y�[�X�L�[�������ꂽ��A
        {
            _Score += 1000; //Score��1000���ς���
        }
    }
}
