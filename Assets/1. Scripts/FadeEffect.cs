using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections; // �ڷ�ƾ ����� ���ؼ� �߰�

public class FadeEffect : MonoBehaviour
{
    Image image;   // ���̵� ȿ���� ���Ǵ� ���� ���� �̹��� 

    [SerializeField]
    [Range(0.01f, 10f)]
    float fadeTime;    // fadeSpeed ���� 10�̸� 1�� -> ���� Ŭ���� ���� 

    public Slider redDragonHpBar;


    // Start is called before the first frame update
    void Start()
    {

        image = GetComponent<Image>();

        //// Fade In. ����� ���İ��� 1���� 0���� (ȭ���� ���� �����) 
        StartCoroutine(Fade(1, 0));

    }

    private void Update()
    {
        if (Json.instance.data.hp <= 0) // �÷��̾ �׾��� �� 
        {

            // Fade Out. ����� ���İ��� 0���� 1���� (ȭ���� ���� ��ο���)
            FadeEffectStart();

        }

        else if (redDragonHpBar.value <= 0) // ����巡���� ����� �� 
        {
            // Fade Out. ����� ���İ��� 0���� 1���� (ȭ���� ���� ��ο���)
            FadeEffectStart();
        }

    }

    public void FadeEffectStart()
    {
        StartCoroutine(Fade(0, 1));
    }
   public IEnumerator Fade(float start, float end)
    {
        
        float currenTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
         

            // fadeTime���� ����� fadeTime �ð����� 
            // percent ���� 0���� 1�� �����ϵ��� ��.
            currenTime += Time.deltaTime;
            percent = currenTime / fadeTime;

            // ���İ��� start���� end���� fadeTime �ð� ���� ��ȭ��Ų��
            Color color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;
        
        
        
        }
    }

}
