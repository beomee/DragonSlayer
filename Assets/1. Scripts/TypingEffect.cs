using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{

    public Text StoryText;
    string m_text = 
        "10�� �� ����, ���� �θ���� �Ҿ���. \n" +
        "\n" +
        "��ī�̼����� �츮 ������ �����߰� �θ���� ���� ���� �� ���ư��̴�. \n" +
        "\n" +
        "������ ���� ���� ���������� �����Ǿ���, ���� ģ��ó�� Ű���̴ּ�. \n " +
        "\n" +
        "10�⵿�� ��� �� ������ ������ \n" +
        "\n" +
        "���� ���� �θ���� ������ ���� ���� ������ ������.";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TypingStoryText());
    }

    IEnumerator TypingStoryText()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i <= m_text.Length; i++)
        {
            StoryText.text = m_text.Substring(0, i);    //Substring(������ġ, ����) ���ڿ����� �׸� ���ΰ�?

            yield return new WaitForSeconds(0.14f);  // Ÿ���� �Ǵ� �ð� ���� (0.10�ʸ��� ��Ÿ���� ǥ��) 
        }
    }
}
