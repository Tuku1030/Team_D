using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//�V�[���؂�ւ��Ɏg����



public class o_SceneChanger : MonoBehaviour
{

    [SerializeField] private string _loadScene;//�V�[���̐؂�ւ����ǂݍ��ނ̂ɕK�v

    public void SceneChange()
    {
        SceneManager.LoadScene(_loadScene);

    }
}