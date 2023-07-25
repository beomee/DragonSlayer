using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMapTitle_Player : MonoBehaviour
{

    public Text[] mapTitles;
    //public BoxCollider boxCollider;

    Text ShowMapTitle; // �� �ؽ�Ʈ�� ���� ���� 

    bool isArea0 = false;
    bool isArea1 = false;
    bool isArea2 = false;
    bool isArea3 = false;
    bool isArea4 = false;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(ShowArea0());
       
    }

    // Update is called once per frame
    void Update()
    {



    }




    private void OnTriggerEnter(Collider other)
    {
        new WaitForSeconds(2f);

        if (other.CompareTag("Area0"))
        {
            ShowMapTitle = mapTitles[0];  // ShowMapTitle�ȿ� ��ܿ� ǥ���� ���� �̸� �ؽ�Ʈ�� ���ִ� ����

            ChangeArea(ShowMapTitle);

            //Bgm ����
             AudioManager.instance.PlayStartGameSceneBgm();
        }

        else if (other.CompareTag("Area1"))
        {
            ShowMapTitle = mapTitles[1];  // ShowMapTitle�ȿ� ��ܿ� ǥ���� ���� �̸� �ؽ�Ʈ�� ���ִ� ����

            ChangeArea(ShowMapTitle);

            AudioManager.instance.PlayStartFieldBgm();
        }

        else if (other.CompareTag("Area2"))
        {
            ShowMapTitle = mapTitles[2];  // ShowMapTitle�ȿ� ��ܿ� ǥ���� ���� �̸� �ؽ�Ʈ�� ���ִ� ����

            ChangeArea(ShowMapTitle);

            AudioManager.instance.StartBeachBgm();

        }

        else if (other.CompareTag("Area3"))
        {
            ShowMapTitle = mapTitles[3];  // ShowMapTitle�ȿ� ��ܿ� ǥ���� ���� �̸� �ؽ�Ʈ�� ���ִ� ����

            ChangeArea(ShowMapTitle);

            Enemy_RedDragon.instance.enemyhp = Enemy_RedDragon.instance.wholeEnemyhp;
            Enemy_RedDragon.instance.hpBarSlider.value = Enemy_RedDragon.instance.enemyhp;
            AudioManager.instance.PlayStartMountainBgm();
        }

        else if (other.CompareTag("Area4"))
        {
            ShowMapTitle = mapTitles[4];  // ShowMapTitle�ȿ� ��ܿ� ǥ���� ���� �̸� �ؽ�Ʈ�� ���ִ� ����

            ChangeArea(ShowMapTitle);

            AudioManager.instance.PlayStartBossBgm();
        }








    }

    void ChangeArea(Text ShowMapTitle)
    {
       // �۾� �����ִ� �ڵ�
       StartCoroutine(ShowMapTitle0_Coroutine(ShowMapTitle));

       // �۾� õõ�� ������� �ϴ� �ڵ� 
       StartCoroutine(CloseMapTitle0_Coroutine(ShowMapTitle));
       
    }


    IEnumerator ShowMapTitle0_Coroutine(Text mapTitle)   // �ؽ�Ʈ�� �÷��� �� a���� 0���� 1�� �ø��� �Լ�(�۾��� ���� �������)
    {
        yield return new WaitForSeconds(0.1f);

        float time = 0;

        Color mapTitleColor = mapTitle.color;

        while (mapTitle.color.a != 1f) // 1�� �Ǳ� ������ �ݺ� 
        {

            time += Time.deltaTime;

            // 3�ʸ��� ���ڰ� ��Ÿ���� �ڵ� 
            mapTitle.color = Vector4.Lerp(mapTitleColor, new Vector4(mapTitleColor.r, mapTitleColor.g, mapTitleColor.b, 1f), time / 1f);

            yield return null; // �� ������ ���� 

        }

    }

    // �۾��� ������ ��������� �ϴ� �Լ� 
    IEnumerator CloseMapTitle0_Coroutine(Text mapTitle) // �ؽ�Ʈ�� �÷��� �� a���� 255���� 0���� ������ �Լ� (�۾��� ���� ��ο�����)
    {

        yield return new WaitForSeconds(3f);

        float time = 0;

        Color mapTitleColor = mapTitle.color;

        while (mapTitle.color.a > 0f) // 0�� �Ǳ� �������� �ݺ� 
        {

            time += Time.deltaTime;

            // 1.5�ʸ��� ���ڰ� ������� �ڵ� 
            mapTitle.color = Vector4.Lerp(mapTitleColor, new Vector4(mapTitleColor.r, mapTitleColor.g, mapTitleColor.b, 0.0f), time / 0.5f);


            yield return null; // �� ������ ���� 
        }

    }

    // �۾��� �ٷ� ��������� �ϴ� �Լ� 
    IEnumerator ExitMapTitle0_Coroutine(Text mapTitle) // �ؽ�Ʈ�� �÷��� �� a���� 255���� 0���� ������ �Լ� (�۾��� ���� ��ο�����)
    {
        yield return new WaitForSeconds(3f);
        float time = 0;

        Color mapTitleColor = mapTitle.color;

        while (mapTitle.color.a > 0f) // 0�� �Ǳ� �������� �ݺ� 
        {

            time += Time.deltaTime;

            // �ʸ��� ���ڰ� ������� �ڵ� 
            mapTitle.color = Vector4.Lerp(mapTitleColor, new Vector4(mapTitleColor.r, mapTitleColor.g, mapTitleColor.b, 0.0f), time / 0.01f);


            yield return null; // �� ������ ���� 
        }

    }


    IEnumerator ShowArea0() // ���ӽ��� �� 1�ʵڿ� �ݶ��̴��� ������ �ڷ�ƾ�Լ�

    {
        yield return new WaitForSeconds(0.5f);
        //boxCollider.enabled = true;
    }
}
