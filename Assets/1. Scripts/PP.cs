using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PP : MonoBehaviour
{
    public float hp;
    public int money;
    public string nickname;



    // Start is called before the first frame update
    void Start()
    {
        // 2. 불러오기
        hp = PlayerPrefs.GetFloat("hp");
        money = PlayerPrefs.GetInt("Money", money);
        nickname = PlayerPrefs.GetString("Nickname", nickname);

        print("불러오기 완료");


        //PlayerPrefs.HasKey("키이름"); -> 키 값이 있는지 

        //PlayerPrefs.DeleteAll(); -> 전부 삭제
        //PlayerPrefs.DeleteKey("키이름"); -> 키 하나 삭제

    }

    // Update is called once per frame
    void Update()
    {
        // 1. 저장하기
        if (Input.GetMouseButtonDown(1))
        {
            // 저장 
            PlayerPrefs.SetFloat("hp", hp);
            PlayerPrefs.SetInt("Money", money);
            PlayerPrefs.SetString("Nickname", nickname);

            PlayerPrefs.Save(); // 실제 디스크 안에 저장되니까 여기까지 해야 안전함.

            print("저장완료");

        }



    }



}

