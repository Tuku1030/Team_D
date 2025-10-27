using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] private int minute = 1; // 制限時間（分）
    [SerializeField] private float seconds = 0f; // 制限時間（秒）
    private float totalTime; // 合計制限時間（秒）
    private float oldSeconds; // 前回の秒数
    private Text timerText; // タイマー表示用テキスト

    void Start()
    {
        totalTime = minute * 180 + seconds; // 合計秒数を計算
        oldSeconds = totalTime;
        timerText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        if (totalTime <= 0f)
        {
            Debug.Log("制限時間終了");
            EndGame(); // ゲーム終了処理
            return;
        }

        totalTime -= Time.deltaTime; // 時間を減らす
        int displayMinutes = (int)totalTime / 60;
        int displaySeconds = (int)totalTime % 60;

        if ((int)totalTime != (int)oldSeconds)
        {
            timerText.text = displayMinutes.ToString("00") + ":" + displaySeconds.ToString("00");
            oldSeconds = totalTime;
        }
    }

    void EndGame()
    {
        // ゲーム終了処理（例: シーン遷移やメッセージ表示）
        Debug.Log("ゲームオーバー");
    }
}
