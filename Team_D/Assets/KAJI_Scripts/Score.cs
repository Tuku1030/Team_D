using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    private int _Score; 　  //得点の変数
    public Text ScoreText;  //得点の文字の変数

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Score = 0; //得点を０にする
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score:" + _Score.ToString();

        if (Input.GetKey(KeyCode.P)) //もし、スペースキーがおされたら、
        {
            _Score += 1000; //Scoreを1000ずつ変える
        }
    }
}
