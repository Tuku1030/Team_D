using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HorseMarckele : MonoBehaviour
{
    public GameObject player;  // �ړ��Ώ�
    public int speed = 3;      // �ړ��X�s�[�h
    Vector3 movePosition;      // �ړ��ڕW�ʒu

    [Header("���f�[�^�ݒ�")]
    public string fishName = "HorseMackerel";  // ���̎�ޖ��i��F�A�W�j
    public float addRate = 0.2f;               // ���̋�1�C������̔{�����Z�l

    private bool isCaptured = false; // �ߊl�ςݔ���

    void Start()
    {
        movePosition = moveRandomPosition();
    }

    void Update()
    {
        if (isCaptured) return; // �ߊl�ς݂Ȃ瓮�����Ȃ�

        // �����_���ړ�
        if (movePosition == player.transform.position)
        {
            movePosition = moveRandomPosition();
        }

        player.transform.position = Vector3.MoveTowards(player.transform.position, movePosition, speed * Time.deltaTime);

        // �������]
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (player.transform.position.x < movePosition.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (player.transform.position.x > movePosition.x)
        {
            spriteRenderer.flipX = false;
        }
    }

    // �Ԃɓ��������Ƃ��̏���
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCaptured) return;

        if (other.CompareTag("Net")) // �ԃI�u�W�F�N�g�̃^�O��"Net"�ɐݒ肵�Ă���
        {
            isCaptured = true;

            // �ߊl���ꂽ���Ƃ��X�R�A�Ǘ��֒ʒm
            NetScoreCalculator scoreCalculator = FindObjectOfType<NetScoreCalculator>();
            if (scoreCalculator != null)
            {
                scoreCalculator.AddCapturedFish(fishName, addRate);
            }

            // �ߊl���o�Ȃǂ���ꂽ���ꍇ�͂����ɃA�j���[�V��������ǉ�
            Destroy(gameObject); // �����폜
        }
    }

    private Vector3 moveRandomPosition()
    {
        return new Vector3(Random.Range(-7, 7), Random.Range(-4, 4), 3);
    }
}
