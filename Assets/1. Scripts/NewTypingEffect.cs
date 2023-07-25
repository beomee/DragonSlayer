using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewTypingEffect : MonoBehaviour
{
    string targetMsg; // 대화 글씨가 들어갈 변수 
    public int CharPerSeconds; // 1초에 몇글자가 나올지 
    Text msgText;
    int index; // 글자 개수를 증가시켜야하니까
    public GameObject NextText; // 다음 대화창으로 갈 이미지를 setActive = true 해주기 위한 변수 

    // Start is called before the first frame update
    public void SetMsg(string msg)
    {
        targetMsg = msg;
        EffectStart();


    }

    private void Awake()
    {
        msgText = GetComponent<Text>();
    }


    void EffectStart()
    {
        msgText.text = ""; // 시작할때는 공백
        index = 0;
        NextText.SetActive(false);

        Invoke("Effecting", 1 / CharPerSeconds); // 여기 시간이 지나면 Effecting이 실행이 됨.
    }

    void Effecting()
    {
        if (msgText.text == targetMsg) // 재귀함수 빠져나가는 곳
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index]; // 한글자 한글자 붙여주기  
        index++;

        Invoke("Effecting", 1 / CharPerSeconds); // 재귀함수 - 자기함수를 계속 돌려 
    }
    void EffectEnd()
    {
        NextText.SetActive(true);
    }

}
