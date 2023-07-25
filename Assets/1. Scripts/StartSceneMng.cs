using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class StartSceneMng : MonoBehaviour
{

    public Button StartBtn;  // ó������
    public Button ExitBtn;   // ��������
    public Button LoadBtn;   // �̾��ϱ�   
    public Button QuitBtn;   // â �ݱ�
    public Image ExplainImage;

    // Start is called before the first frame update
    void Start()
    {


        StartBtn.onClick.AddListener(ClickStart);

        ExitBtn.onClick.AddListener(ClickExit);

        LoadBtn.onClick.AddListener(ClickLoad);

        QuitBtn.onClick.AddListener(ClickQuit);


    }

    

    public void ClickStart()
    {
        // ���丮������ ��ȯ�ϴ� ���
        SceneManager.LoadScene("2.StoryScene");
       
        
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

    public void ClickLoad()
    {
        // �÷��̾����� ��ȯ�ϴ� ���
        SceneManager.LoadScene("3.GameScene");


    }

    public void ClickQuit()
    {
        ExplainImage.gameObject.SetActive(false);
    }


    public void ShowExplainImg()
    {
        ExplainImage.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        



    }



}
