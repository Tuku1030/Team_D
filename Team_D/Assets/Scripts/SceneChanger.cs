using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン切り替えに使える



public class o_SceneChanger : MonoBehaviour
{

    [SerializeField] private string _loadScene;//シーンの切り替え先を読み込むのに必要

    public void SceneChange()
    {
        SceneManager.LoadScene(_loadScene);

    }
}