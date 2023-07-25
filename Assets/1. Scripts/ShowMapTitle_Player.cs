using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMapTitle_Player : MonoBehaviour
{

    public Text[] mapTitles;
    //public BoxCollider boxCollider;

    Text ShowMapTitle; // 맵 텍스트를 담을 변수 

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
            ShowMapTitle = mapTitles[0];  // ShowMapTitle안에 상단에 표기할 맵의 이름 텍스트가 들어가있는 상태

            ChangeArea(ShowMapTitle);

            //Bgm 변경
             AudioManager.instance.PlayStartGameSceneBgm();
        }

        else if (other.CompareTag("Area1"))
        {
            ShowMapTitle = mapTitles[1];  // ShowMapTitle안에 상단에 표기할 맵의 이름 텍스트가 들어가있는 상태

            ChangeArea(ShowMapTitle);

            AudioManager.instance.PlayStartFieldBgm();
        }

        else if (other.CompareTag("Area2"))
        {
            ShowMapTitle = mapTitles[2];  // ShowMapTitle안에 상단에 표기할 맵의 이름 텍스트가 들어가있는 상태

            ChangeArea(ShowMapTitle);

            AudioManager.instance.StartBeachBgm();

        }

        else if (other.CompareTag("Area3"))
        {
            ShowMapTitle = mapTitles[3];  // ShowMapTitle안에 상단에 표기할 맵의 이름 텍스트가 들어가있는 상태

            ChangeArea(ShowMapTitle);

            Enemy_RedDragon.instance.enemyhp = Enemy_RedDragon.instance.wholeEnemyhp;
            Enemy_RedDragon.instance.hpBarSlider.value = Enemy_RedDragon.instance.enemyhp;
            AudioManager.instance.PlayStartMountainBgm();
        }

        else if (other.CompareTag("Area4"))
        {
            ShowMapTitle = mapTitles[4];  // ShowMapTitle안에 상단에 표기할 맵의 이름 텍스트가 들어가있는 상태

            ChangeArea(ShowMapTitle);

            AudioManager.instance.PlayStartBossBgm();
        }








    }

    void ChangeArea(Text ShowMapTitle)
    {
       // 글씨 보여주는 코드
       StartCoroutine(ShowMapTitle0_Coroutine(ShowMapTitle));

       // 글씨 천천히 사라지게 하는 코드 
       StartCoroutine(CloseMapTitle0_Coroutine(ShowMapTitle));
       
    }


    IEnumerator ShowMapTitle0_Coroutine(Text mapTitle)   // 텍스트의 컬러값 중 a값을 0에서 1로 올리는 함수(글씨를 점점 밝아지게)
    {
        yield return new WaitForSeconds(0.1f);

        float time = 0;

        Color mapTitleColor = mapTitle.color;

        while (mapTitle.color.a != 1f) // 1이 되기 전까지 반복 
        {

            time += Time.deltaTime;

            // 3초만에 글자가 나타나는 코드 
            mapTitle.color = Vector4.Lerp(mapTitleColor, new Vector4(mapTitleColor.r, mapTitleColor.g, mapTitleColor.b, 1f), time / 1f);

            yield return null; // 한 프레임 쉬기 

        }

    }

    // 글씨가 서서히 사라지도록 하는 함수 
    IEnumerator CloseMapTitle0_Coroutine(Text mapTitle) // 텍스트의 컬러값 중 a값을 255에서 0으로 내리는 함수 (글씨를 점점 어두워지게)
    {

        yield return new WaitForSeconds(3f);

        float time = 0;

        Color mapTitleColor = mapTitle.color;

        while (mapTitle.color.a > 0f) // 0이 되기 전까지만 반복 
        {

            time += Time.deltaTime;

            // 1.5초만에 글자가 사라지는 코드 
            mapTitle.color = Vector4.Lerp(mapTitleColor, new Vector4(mapTitleColor.r, mapTitleColor.g, mapTitleColor.b, 0.0f), time / 0.5f);


            yield return null; // 한 프레임 쉬기 
        }

    }

    // 글씨가 바로 사라지도록 하는 함수 
    IEnumerator ExitMapTitle0_Coroutine(Text mapTitle) // 텍스트의 컬러값 중 a값을 255에서 0으로 내리는 함수 (글씨를 점점 어두워지게)
    {
        yield return new WaitForSeconds(3f);
        float time = 0;

        Color mapTitleColor = mapTitle.color;

        while (mapTitle.color.a > 0f) // 0이 되기 전까지만 반복 
        {

            time += Time.deltaTime;

            // 초만에 글자가 사라지는 코드 
            mapTitle.color = Vector4.Lerp(mapTitleColor, new Vector4(mapTitleColor.r, mapTitleColor.g, mapTitleColor.b, 0.0f), time / 0.01f);


            yield return null; // 한 프레임 쉬기 
        }

    }


    IEnumerator ShowArea0() // 게임시작 시 1초뒤에 콜라이더가 켜지는 코루틴함수

    {
        yield return new WaitForSeconds(0.5f);
        //boxCollider.enabled = true;
    }
}
