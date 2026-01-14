using UnityEngine;
using UnityEngine.SceneManagement;

public class STAGEOverManager : MonoBehaviour
{
    public float clearTime = 30f;        // 何秒後に判定するか
    public int needScore = 2000;         // クリアに必要なスコア

    private float timer = 0f;
    private bool judged = false;         //  多重判定防止

    void Start()
    {
        timer = 0f;
        judged = false;
    }


    void Update()
    {
        //  ゲームオーバー中なら何もしない
        if (GameOverManager.isGameOver) return;

        if (judged) return;

        timer += Time.deltaTime;

        if (timer >= clearTime)
        {
            judged = true; //  1回だけ判定

            int score = TotalScoreManager.Instance.GetTotalScore();

            //  スコア判定が最優先
            if (score >= needScore)
            {
                Debug.Log("クリア！");
                FindFirstObjectByType<StageClearManager>().StageClear();
            }
            else
            {
                Debug.Log(" スコア不足 → ゲームオーバー");
                GameOverManager.GameOver();
            }
        }
    }
}