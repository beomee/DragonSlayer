using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections; // 코루틴 사용을 위해서 추가

public class FadeEffect : MonoBehaviour
{
    Image image;   // 페이드 효과에 사용되는 검은 바탕 이미지 

    [SerializeField]
    [Range(0.01f, 10f)]
    float fadeTime;    // fadeSpeed 값이 10이면 1초 -> 값이 클수록 빠름 

    public Slider redDragonHpBar;


    // Start is called before the first frame update
    void Start()
    {

        image = GetComponent<Image>();

        //// Fade In. 배경의 알파값이 1에서 0으로 (화면이 점점 밝아짐) 
        StartCoroutine(Fade(1, 0));

    }

    private void Update()
    {
        if (Json.instance.data.hp <= 0) // 플레이어가 죽었을 때 
        {

            // Fade Out. 배경의 알파값이 0에서 1으로 (화면이 점점 어두워짐)
            FadeEffectStart();

        }

        else if (redDragonHpBar.value <= 0) // 레드드래곤을 잡았을 때 
        {
            // Fade Out. 배경의 알파값이 0에서 1으로 (화면이 점점 어두워짐)
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
         

            // fadeTime으로 나누어서 fadeTime 시간동안 
            // percent 값이 0에서 1로 증가하도록 함.
            currenTime += Time.deltaTime;
            percent = currenTime / fadeTime;

            // 알파값을 start부터 end까지 fadeTime 시간 동안 변화시킨다
            Color color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;
        
        
        
        }
    }

}
