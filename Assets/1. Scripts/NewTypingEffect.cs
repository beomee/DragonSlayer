using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewTypingEffect : MonoBehaviour
{
    string targetMsg; // ��ȭ �۾��� �� ���� 
    public int CharPerSeconds; // 1�ʿ� ����ڰ� ������ 
    Text msgText;
    int index; // ���� ������ �������Ѿ��ϴϱ�
    public GameObject NextText; // ���� ��ȭâ���� �� �̹����� setActive = true ���ֱ� ���� ���� 

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
        msgText.text = ""; // �����Ҷ��� ����
        index = 0;
        NextText.SetActive(false);

        Invoke("Effecting", 1 / CharPerSeconds); // ���� �ð��� ������ Effecting�� ������ ��.
    }

    void Effecting()
    {
        if (msgText.text == targetMsg) // ����Լ� ���������� ��
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index]; // �ѱ��� �ѱ��� �ٿ��ֱ�  
        index++;

        Invoke("Effecting", 1 / CharPerSeconds); // ����Լ� - �ڱ��Լ��� ��� ���� 
    }
    void EffectEnd()
    {
        NextText.SetActive(true);
    }

}
