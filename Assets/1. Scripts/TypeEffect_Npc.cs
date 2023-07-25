using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect_Npc : MonoBehaviour
{
    public int CharPerSeconds; // ���� ����ӵ�
    string targetMsg; // ǥ�� �� �޼����� ��� ���� 
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
        Invoke("Effecting", interval);  // 1�� / CharPerSeconds : 1���ڰ� ������ ������
    }

    void Effecting()
    {
        if (msgText.text == targetMsg)
        {
            return;
        }

        msgText.text += targetMsg[index]; // �� ���ھ� �����ְ� 
        index++;

        Invoke("Effecting", interval);  // 1�� / CharPerSeconds : 1���ڰ� ������ ������ -> �ڱ� �ڽ��� ������
    }

    void EffectEnd()
    { 
      
    }




}
