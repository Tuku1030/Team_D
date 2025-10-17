using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HorseMackerel : MonoBehaviour
{
    public GameObject target;

    float speed;
    float time;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.003f);
        if (transform.position == target.transform.position)
        {
            time = 0.0f;
        }
    }
}