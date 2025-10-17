using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMovingIwashi : MonoBehaviour
{
    private float time;
    private float vecx;
    private float vecy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //タイム初期化
        time = 0;
    }
    public GameObject target;


    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (transform.position == target.transform.position)
        {
            time = 0;
            //ランダムな座標の設定
            vecx = Random.Range(-5, 5);
            vecy = Random.Range(-5, 5);
            transform.position = new Vector3(vecx, vecy, 0);
            //タイムリセット
            time = 10000;
        }

    }
}


