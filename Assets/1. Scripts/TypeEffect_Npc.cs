using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect_Npc : MonoBehaviour
{
    public int CharPerSeconds; // 글자 재생속도
    string targetMsg; // 표현 할 메세지를 담는 변수 
    Text msgText;
    int index;
    float interval;

    private void Awake()
    {
        msgText = GetComponent<Text>();
    }

    public void SetMsg(string msg)
    {
        targetMsg = msg;
        EffectStart();
    }


    void EffectStart()
    {
        msgText.text = "";
        index = 0;

        interval = 1.0f / CharPerSeconds;
        Invoke("Effecting", interval);  // 1초 / CharPerSeconds : 1글자가 나오는 딜레이
    }

    void Effecting()
    {
        if (msgText.text == targetMsg)
        {
            return;
        }

        msgText.text += targetMsg[index]; // 한 글자씩 붙혀주고 
        index++;

        Invoke("Effecting", interval);  // 1초 / CharPerSeconds : 1글자가 나오는 딜레이 -> 자기 자신을 돌리기
    }

    void EffectEnd()
    { 
      
    }




}
