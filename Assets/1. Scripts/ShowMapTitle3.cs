using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMapTitle3 : MonoBehaviour
{

    public Text mapTitle3;

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {



    }


    private void OnTriggerEnter(Collider other)
    {

        // 플레이어와 닿으면 텍스트의 알파값을 255로 바꿔주고, 3초 뒤 0 으로 바꾸기 . 
        if (other.CompareTag("Player"))
        {

            Enemy_RedDragon.instance.enemyhp = Enemy_RedDragon.instance.wholeEnemyhp;
            Enemy_RedDragon.instance.hpBarSlider.value = Enemy_RedDragon.instance.enemyhp;
            AudioManager.instance.PlayStartMountainBgm();


        }



    }



    IEnumerator ShowMapTitle3_Coroutine()   // 텍스트의 컬러값 중 a값을 0에서 1로 올리는 함수 
    {
        yield return new WaitForSeconds(0.1f);

        float time = 0;

        Color mapTitleColor = mapTitle3.color;

        while (mapTitle3.color.a != 1f) // 1이 되기 전까지 반복 
        {

            time += Time.deltaTime;

            // 3초만에 글자가 나타나는 코드 
            mapTitle3.color = Vector4.Lerp(mapTitleColor, new Vector4(mapTitleColor.r, mapTitleColor.g, mapTitleColor.b, 1f), time / 1f);

            yield return null; // 한 프레임 쉬기 
           
        }

    }


    IEnumerator CloseMapTitle3_Coroutine() // 텍스트의 컬러값 중 a값을 255에서 0으로 내리는 함수 
    {

        yield return new WaitForSeconds(3f);

        float time = 0;

        Color mapTitleColor = mapTitle3.color;

        while (mapTitle3.color.a > 0f) // 0이 되기 전까지만 반복 
        {

            time += Time.deltaTime;

            // 3초만에 글자가 사라지는 코드 
            mapTitle3.color = Vector4.Lerp(mapTitleColor, new Vector4(mapTitleColor.r, mapTitleColor.g, mapTitleColor.b, 0.0f), time / 2f);


            yield return null; // 한 프레임 쉬기 
        }

    }



}
