using UnityEngine;

public class BillFishClone : MonoBehaviour
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

        if (timeCount > 30f)
        {
            if (count < 3)
            {
                timeCount = 0;
                // Prefab の生成
                Instantiate(Parent, new Vector3(Random.Range(-7, 7), Random.Range(-4, 4)), Quaternion.identity);
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
