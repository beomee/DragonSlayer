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
        // 2. �ҷ�����
        hp = PlayerPrefs.GetFloat("hp");
        money = PlayerPrefs.GetInt("Money", money);
        nickname = PlayerPrefs.GetString("Nickname", nickname);

        print("�ҷ����� �Ϸ�");


        //PlayerPrefs.HasKey("Ű�̸�"); -> Ű ���� �ִ��� 

        //PlayerPrefs.DeleteAll(); -> ���� ����
        //PlayerPrefs.DeleteKey("Ű�̸�"); -> Ű �ϳ� ����

    }

    // Update is called once per frame
    void Update()
    {
        // 1. �����ϱ�
        if (Input.GetMouseButtonDown(1))
        {
            // ���� 
            PlayerPrefs.SetFloat("hp", hp);
            PlayerPrefs.SetInt("Money", money);
            PlayerPrefs.SetString("Nickname", nickname);

            PlayerPrefs.Save(); // ���� ��ũ �ȿ� ����Ǵϱ� ������� �ؾ� ������.

            print("����Ϸ�");

        }



    }



}

