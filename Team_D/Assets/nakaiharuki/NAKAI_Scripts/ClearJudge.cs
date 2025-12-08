using UnityEngine;

public class ClearJudge : MonoBehaviour
{
    public int clearScore = 2000;
    public float clearTime = 60f;

    float timer = 0f;
    public StageClearManager clearManager;

    void Update()
    {
        if (GameOverManager.isGameOver) return;
        if (StageClearManager.isClear) return;

        timer += Time.deltaTime;

        if (timer >= clearTime &&
            TotalScoreManager.Instance.GetTotalScore() >= clearScore)
        {
            clearManager.StageClear();
        }
    }
}