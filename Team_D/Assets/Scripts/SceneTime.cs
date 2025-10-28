using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private float elapsedTime = 0.0f; // �o�ߎ��Ԃ��L�^
    public float timeLimit = 30.0f; // �������ԁi�b�j
    public string nextSceneName; // �J�ڐ�̃V�[����

    void Update()
    {
        elapsedTime += Time.deltaTime; // �t���[�����Ƃ̌o�ߎ��Ԃ����Z

        if (elapsedTime >= timeLimit) // �������Ԃ𒴂�����
        {
            SceneManager.LoadScene(nextSceneName); // �V�[�����ړ�
        }
    }
}
