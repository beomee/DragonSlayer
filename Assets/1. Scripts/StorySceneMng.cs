using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StorySceneMng : MonoBehaviour
{
    public Button skipbtn;
   

    // Start is called before the first frame update
    void Start()
    {
        skipbtn.onClick.AddListener(Clickskip);
    }

    



    public void Clickskip()
    {
        // ���丮������ ��ȯ�ϴ� ���
        SceneManager.LoadScene("3.GameScene");

    }


    // Update is called once per frame
    void Update()
    {
        



    }



}
