using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndingSceneMng : MonoBehaviour
{

    public Button ExitBtn;  // 처음시작


    // Start is called before the first frame update
    void Start()
    {
        ExitBtn.onClick.AddListener(ClickExit);

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





    // Update is called once per frame
    void Update()
    {
        



    }



}
