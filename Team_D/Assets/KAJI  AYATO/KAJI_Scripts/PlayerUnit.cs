using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public GameObject Bullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//���N���b�N�Œe�𔭎�
        {
            //�e�̐���
            Instantiate(Bullet, transform.position, Quaternion.identity);
        }
    }
}
