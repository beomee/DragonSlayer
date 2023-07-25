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

        // �÷��̾�� ������ �ؽ�Ʈ�� ���İ��� 255�� �ٲ��ְ�, 3�� �� 0 ���� �ٲٱ� . 
        if (other.CompareTag("Player"))
        {

            Enemy_RedDragon.instance.enemyhp = Enemy_RedDragon.instance.wholeEnemyhp;
            Enemy_RedDragon.instance.hpBarSlider.value = Enemy_RedDragon.instance.enemyhp;
            AudioManager.instance.PlayStartMountainBgm();


        }



    }



    IEnumerator ShowMapTitle3_Coroutine()   // �ؽ�Ʈ�� �÷��� �� a���� 0���� 1�� �ø��� �Լ� 
    {
        yield return new WaitForSeconds(0.1f);

        float time = 0;

        Color mapTitleColor = mapTitle3.color;

        while (mapTitle3.color.a != 1f) // 1�� �Ǳ� ������ �ݺ� 
        {

            time += Time.deltaTime;

            // 3�ʸ��� ���ڰ� ��Ÿ���� �ڵ� 
            mapTitle3.color = Vector4.Lerp(mapTitleColor, new Vector4(mapTitleColor.r, mapTitleColor.g, mapTitleColor.b, 1f), time / 1f);

            yield return null; // �� ������ ���� 
           
        }

    }


    IEnumerator CloseMapTitle3_Coroutine() // �ؽ�Ʈ�� �÷��� �� a���� 255���� 0���� ������ �Լ� 
    {

        yield return new WaitForSeconds(3f);

        float time = 0;

        Color mapTitleColor = mapTitle3.color;

        while (mapTitle3.color.a > 0f) // 0�� �Ǳ� �������� �ݺ� 
        {

            time += Time.deltaTime;

            // 3�ʸ��� ���ڰ� ������� �ڵ� 
            mapTitle3.color = Vector4.Lerp(mapTitleColor, new Vector4(mapTitleColor.r, mapTitleColor.g, mapTitleColor.b, 0.0f), time / 2f);


            yield return null; // �� ������ ���� 
        }

    }



}
