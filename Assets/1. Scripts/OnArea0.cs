using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnArea0 : MonoBehaviour
{
    public BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowArea0());
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    IEnumerator ShowArea0() // 게임시작 시 1초뒤에 콜라이더가 켜지는 코루틴함수

    {
        yield return new WaitForSeconds(0.5f);
        boxCollider.enabled = true;
    }
}
