using UnityEngine;

public class SardineClone : MonoBehaviour
{
    public GameObject player;   // ���̃I�u�W�F�N�g���Q�Ƃ���ϐ�
    public Transform Parent;    // �w�肷��e�I�u�W�F�N�g���Q�Ƃ���ϐ�
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
                // Prefab �̐���
                player.SetActive(true); //�I�u�W�F�N�g��\������
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
