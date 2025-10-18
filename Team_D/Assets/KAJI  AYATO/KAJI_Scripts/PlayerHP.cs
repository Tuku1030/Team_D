using UnityEngine;
using UnityEngine.UI;


public class PlayerHP : MonoBehaviour
{
   
    public GameObject HPIcon; //HP�A�C�R���̃v���n�u

    private PlayerContller Player; //�v���C���[�R���g���[���[�̃X�N���v�g���擾
    private int BeforeHP;          //�O���HP���L�^


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = FindObjectOfType<PlayerContller>();
        BeforeHP = Player.GetHP();
        CreateHPIcon();
    }

    private void CreateHPIcon()
    {
        //�v���C���[��HP�̐������A�C�R���𐶐�
        for(int i = 0; i<Player.GetHP(); i++)
        {
            GameObject PlayerHPObj = Instantiate(HPIcon);
            PlayerHPObj.transform.SetParent(transform);  //�e�iHP�j�I�u�W�F�N�g�ɐݒ�
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShowHPIcon(); //HP�̕ω����`�F�b�N
    }

    private void ShowHPIcon()
    {
        //HP���ς���Ă��Ȃ���Ή������Ȃ�
        if (BeforeHP == Player.GetHP()) return;

        //���ׂĂ�HP�A�C�R�����擾
        Image[] Icons = transform.GetComponentsInChildren<Image>();

        //���݂�HP�ȉ��̃A�C�R���̂ݕ\���A����ȊO�͔�\��
        for(int i = 0; i<Icons.Length; i++)
        {
            Icons[i].gameObject.SetActive(i < Player.GetHP());
        }

        BeforeHP = Player.GetHP();
    }
}
