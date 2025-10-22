using UnityEngine;

public class HorseMarckeleClone : MonoBehaviour
{
    public GameObject player;   // 元のオブジェクトを参照する変数
    public Transform Parent;    // 指定する親オブジェクトを参照する変数
    float timeCount = 0f;
    float count = 0;

    void Start()
    {

    }
    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount > 7f)
        {


            if (count < 7)
            {
                timeCount = 0;
                // Prefab の生成
                Instantiate(Parent, new Vector3(0f, 0f, 0f), Quaternion.identity);
                player.SetActive(true); //オブジェクトを表示する
                count++;
            }
            else
            {
                timeCount = 0;
            }
        }
    }
}
