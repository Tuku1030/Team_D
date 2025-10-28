using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] private int minute = 1; // �������ԁi���j
    [SerializeField] private float seconds = 0f; // �������ԁi�b�j
    private float totalTime; // ���v�������ԁi�b�j
    private float oldSeconds; // �O��̕b��
    private Text timerText; // �^�C�}�[�\���p�e�L�X�g

    void Start()
    {
        totalTime = minute * 180 + seconds; // ���v�b�����v�Z
        oldSeconds = totalTime;
        timerText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        if (totalTime <= 0f)
        {
            Debug.Log("�������ԏI��");
            EndGame(); // �Q�[���I������
            return;
        }

        totalTime -= Time.deltaTime; // ���Ԃ����炷
        int displayMinutes = (int)totalTime / 60;
        int displaySeconds = (int)totalTime % 60;

        if ((int)totalTime != (int)oldSeconds)
        {
            timerText.text = displayMinutes.ToString("00") + ":" + displaySeconds.ToString("00");
            oldSeconds = totalTime;
        }
    }

    void EndGame()
    {
        // �Q�[���I�������i��: �V�[���J�ڂ⃁�b�Z�[�W�\���j
        Debug.Log("�Q�[���I�[�o�[");
    }
}
