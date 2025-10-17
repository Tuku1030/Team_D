using UnityEngine;
public class LookAt2D : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    void Update()
    {
        // �Ώە��ւ̃x�N�g�����Z�o
        Vector3 toDirection = target.transform.position - transform.position;
        // �Ώە��։�]����
        transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);
    }
}