using UnityEngine;

public class HorseMarckeleClone : MonoBehaviour
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

        if (timeCount > 7f)
        {


            if (count < 7)
            {
                timeCount = 0;
                // Prefab �̐���
                Instantiate(Parent, new Vector3(0f, 0f, 0f), Quaternion.identity);
                player.SetActive(true); //�I�u�W�F�N�g��\������
                count++;
            }
            else
            {
                timeCount = 0;
            }
        }
    }
}
