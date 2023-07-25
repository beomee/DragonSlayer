using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    // 1. � ��簡 �� �� ������ �ؾ��Ѵ�. 
    Dictionary<int, string[]> talkData;





    private void Awake()
    {
        talkData = new Dictionary<int, string[]>(); // ������� ���� ������ְ� 
        GenerateData(); //����Ÿ�� �ִ� �Լ��� �����
    }


    void GenerateData()
    {
        talkData.Add(1000, new string[]
        {   "�غ�� �� �Ǿ���...? \n" +
            "���� 10���̶�� �ð��� �����ٴ�.. �ϱ��� �ʴ±���.. \n" +
            "�� �����ϰ�.. �� ��Ƽ� ���ƿ�������..\n" +
            "����Ѵ� �� ����" 
        }
        );
    }

    
    public string GetTalk(int id, int talkIndex) // �����;ȿ� ��縦 �������� talkIndex : �� ��° ������ ������ ������ ���ϱ� ���ؼ� 
    {

        if (talkIndex == talkData[id].Length)
        {
            return null;
        }

        else
        {
            return talkData[id][talkIndex]; // �� ���徿 ������ͼ� ��ȯ�� ����      
        }


    }


}
