using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{

    public Text StoryText;
    string m_text = 
        "10년 전 오늘, 나는 부모님을 잃었다. \n" +
        "\n" +
        "라카이서스가 우리 마을을 공격했고 부모님은 나를 숨긴 뒤 돌아가셨다. \n" +
        "\n" +
        "다행히 나는 옆집 아저씨에게 구조되었고, 나를 친딸처럼 키워주셨다. \n " +
        "\n" +
        "10년동안 길고 긴 수련을 끝으로 \n" +
        "\n" +
        "나는 오늘 부모님의 원수를 갚기 위해 모험을 떠난다.";

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
            StoryText.text = m_text.Substring(0, i);    //Substring(시작위치, 개수) 문자열부터 그릴 것인가?

            yield return new WaitForSeconds(0.14f);  // 타이핑 되는 시간 간격 (0.10초마다 나타나는 표과) 
        }
    }
}
