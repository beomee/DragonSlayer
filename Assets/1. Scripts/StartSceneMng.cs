using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class StartSceneMng : MonoBehaviour
{

    public Button StartBtn;  // 처음시작
    public Button ExitBtn;   // 게임종료
    public Button LoadBtn;   // 이어하기   
    public Button QuitBtn;   // 창 닫기
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
        // 스토리씬으로 전환하는 기능
        SceneManager.LoadScene("2.StoryScene");
       
        
    }

    public void ClickExit()
    {

        // 게임 종료 
#if UNITY_EDITOR // 에디터 일경우 

        // 에디터에서만 사용 가능한 것
        UnityEditor.EditorApplication.isPlaying = false;

#else
        // 빌드파일에서만 사용 가능한 것 
  Application.Quit();

#endif



    }

    public void ClickLoad()
    {
        // 플레이씬으로 전환하는 기능
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
