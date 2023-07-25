using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndingSceneMng : MonoBehaviour
{

    public Button ExitBtn;  // ó������


    // Start is called before the first frame update
    void Start()
    {
        ExitBtn.onClick.AddListener(ClickExit);

    }



    public void ClickExit()
    {

        // ���� ���� 
#if UNITY_EDITOR // ������ �ϰ�� 

        // �����Ϳ����� ��� ������ ��
        UnityEditor.EditorApplication.isPlaying = false;

#else
        // �������Ͽ����� ��� ������ �� 
  Application.Quit();

#endif



    }





    // Update is called once per frame
    void Update()
    {
        



    }



}
