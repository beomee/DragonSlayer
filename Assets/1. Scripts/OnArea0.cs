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



    IEnumerator ShowArea0() // ���ӽ��� �� 1�ʵڿ� �ݶ��̴��� ������ �ڷ�ƾ�Լ�

    {
        yield return new WaitForSeconds(0.5f);
        boxCollider.enabled = true;
    }
}
