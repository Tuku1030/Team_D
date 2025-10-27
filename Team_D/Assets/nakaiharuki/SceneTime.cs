using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private float elapsedTime = 0.0f; // 経過時間を記録
    public float timeLimit = 30.0f; // 制限時間（秒）
    public string nextSceneName; // 遷移先のシーン名

    void Update()
    {
        elapsedTime += Time.deltaTime; // フレームごとの経過時間を加算

        if (elapsedTime >= timeLimit) // 制限時間を超えたら
        {
            SceneManager.LoadScene(nextSceneName); // シーンを移動
        }
    }
}
