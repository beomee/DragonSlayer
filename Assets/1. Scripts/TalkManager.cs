using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    // 1. 어떤 대사가 들어갈 지 저장을 해야한다. 
    Dictionary<int, string[]> talkData;





    private void Awake()
    {
        talkData = new Dictionary<int, string[]>(); // 빈공간을 먼저 만들어주고 
        GenerateData(); //데이타를 넣는 함수를 만들기
    }


    void GenerateData()
    {
        talkData.Add(1000, new string[]
        {   "준비는 다 되었니...? \n" +
            "벌써 10년이라는 시간이 지났다니.. 믿기지 않는구나.. \n" +
            "몸 조심하고.. 꼭 살아서 돌아오려무나..\n" +
            "사랑한다 내 딸아" 
        }
        );
    }

    
    public string GetTalk(int id, int talkIndex) // 데이터안에 대사를 가져오기 talkIndex : 몇 번째 문장을 가져올 것인지 정하기 위해서 
    {

        if (talkIndex == talkData[id].Length)
        {
            return null;
        }

        else
        {
            return talkData[id][talkIndex]; // 한 문장씩 가지고와서 반환을 해줌      
        }


    }


}
