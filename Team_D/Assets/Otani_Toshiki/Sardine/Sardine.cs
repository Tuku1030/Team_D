
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sardine : MonoBehaviour
{
    public GameObject player;  //�@�����������I�u�W�F�N�g���C���X�y�N�^�[��������B
    public int speed = 1;  //�I�u�W�F�N�g�������œ����X�s�[�h����
    Vector3 movePosition;  //�A�I�u�W�F�N�g�̖ړI�n��ۑ�
    private Action _onDisable;  // ��A�N�e�B�u�����邽�߂̃R�[���o�b�N
    private float _elapsedTime;  // ����������Ă���̌o�ߎ���


        void Start()
    {
        movePosition = moveRandomPosition();  //�A���s���A�I�u�W�F�N�g�̖ړI�n��ݒ�
    }
    void Update()
    {
        if (movePosition == player.transform.position)  //�Aplayer�I�u�W�F�N�g���ړI�n�ɓ��B����ƁA
        {
            movePosition = moveRandomPosition();  //�A�ړI�n���Đݒ�
        }
        this.player.transform.position = Vector3.MoveTowards(player.transform.position, movePosition, speed * Time.deltaTime);  //�@�Aplayer�I�u�W�F�N�g��, �ړI�n�Ɉړ�, �ړ����x
        // SpriteRenderer�R���|�[�l���g���擾
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (player.transform.position.x < movePosition.x)
        {
            if (spriteRenderer.flipX == false)
            {
                // X���ɔ��]��K�p
                spriteRenderer.flipX = true;
            }
        }


        if (player.transform.position.x > movePosition.x)
        {
            if (spriteRenderer.flipX == true)
            {
                // X���ɔ��]��K�p
                spriteRenderer.flipX = false;
            }
        }
    }

    void DelayMethod()
    {
        
    }

    private Vector3 moveRandomPosition()  // �ړI�n�𐶐��Ax��y�̃|�W�V�����������_���ɒl���擾 
    {
        Vector3 randomPosi = new Vector3(Random.Range(-7, 7), Random.Range(-4, 4), 1);
        return randomPosi;
    }
}