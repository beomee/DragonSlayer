using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMapTitle4 : MonoBehaviour
{

    public Text mapTitle4;
    

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

            AudioManager.instance.PlayStartBossBgm();
        }



    }



    IEnumerator ShowMapTitle3_Coroutine()   // �ؽ�Ʈ�� �÷��� �� a���� 0���� 1�� �ø��� �Լ� 
    {
        yield return new WaitForSeconds(0.1f);

        float time = 0;

        Color mapTitleColor = mapTitle4.color;

        while (mapTitle4.color.a != 1f) // 1�� �Ǳ� ������ �ݺ� 
        {

            time += Time.deltaTime;

            // 3�ʸ��� ���ڰ� ��Ÿ���� �ڵ� 
            mapTitle4.color = Vector4.Lerp(mapTitleColor, new Vector4(mapTitleColor.r, mapTitleColor.g, mapTitleColor.b, 1f), time / 1f);

            yield return null; // �� ������ ���� 
           
        }

    }


    IEnumerator CloseMapTitle3_Coroutine() // �ؽ�Ʈ�� �÷��� �� a���� 255���� 0���� ������ �Լ� 
    {

        yield return new WaitForSeconds(3f);

        float time = 0;

        Color mapTitleColor = mapTitle4.color;

        while (mapTitle4.color.a > 0f) // 0�� �Ǳ� �������� �ݺ� 
        {

            time += Time.deltaTime;

            // 3�ʸ��� ���ڰ� ������� �ڵ� 
            mapTitle4.color = Vector4.Lerp(mapTitleColor, new Vector4(mapTitleColor.r, mapTitleColor.g, mapTitleColor.b, 0.0f), time / 2f);


            yield return null; // �� ������ ���� 
        }

    }



}
