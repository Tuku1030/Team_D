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
        //�^�C��������
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
            //�����_���ȍ��W�̐ݒ�
            vecx = Random.Range(-5, 5);
            vecy = Random.Range(-5, 5);
            transform.position = new Vector3(vecx, vecy, 0);
            //�^�C�����Z�b�g
            time = 10000;
        }

    }
}


