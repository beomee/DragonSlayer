using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMapTitle : MonoBehaviour
{

    public Text mapTitle;


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

        new WaitForSeconds(2f);
        // �÷��̾�� ������ �ؽ�Ʈ�� ���İ��� 255�� �ٲ��ְ�, 3�� �� 0 ���� �ٲٱ� . 
        if (other.CompareTag("Player"))
        {
           
            AudioManager.instance.PlayStartFieldBgm();
        }



    }



    IEnumerator ShowMapTitle_Coroutine()   // �ؽ�Ʈ�� �÷��� �� a���� 0���� 1�� �ø��� �Լ� 
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


    IEnumerator CloseMapTitle_Coroutine() // �ؽ�Ʈ�� �÷��� �� a���� 255���� 0���� ������ �Լ� 
    {

        yield return new WaitForSeconds(3f);

        float time = 0;

        Color mapTitleColor = mapTitle.color;

        while (mapTitle.color.a > 0f) // 0�� �Ǳ� �������� �ݺ� 
        {

            time += Time.deltaTime;

            // 3�ʸ��� ���ڰ� ������� �ڵ� 
            mapTitle.color = Vector4.Lerp(mapTitleColor, new Vector4(mapTitleColor.r, mapTitleColor.g, mapTitleColor.b, 0.0f), time / 2f);


            yield return null; // �� ������ ���� 
        }

    }



}
