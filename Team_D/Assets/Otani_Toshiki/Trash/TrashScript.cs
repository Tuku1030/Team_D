using UnityEngine;

public class TrashScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����������肪 "Net" �^�O�̃I�u�W�F�N�g�Ȃ�
        if (collision.CompareTag("Net"))
        {
            // ���̑܂�����
            Destroy(gameObject);
        }
    }
}
