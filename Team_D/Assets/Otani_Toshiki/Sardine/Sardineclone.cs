using UnityEngine;

public class SardineClone : MonoBehaviour
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

        if (timeCount > 3f)
        {
           

            if (count < 10)
            {
                timeCount = 0;
                // Prefab の生成
                player.SetActive(true); //オブジェクトを表示する
                Instantiate(Parent,new Vector3(0f, 0f, 0f), Quaternion.identity);
                count++;
            }
            else
            {
                timeCount = 0;
            }
        }
    }
}
