using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
  


    private void OnEnable()
    {
        // 0.3�� �Ŀ� �ʱ�ȭ �Ǵ� �ڵ� 
        Invoke("ResetAttack", 0.3f);

    }
    void ResetAttack()
    {
        // ��Ȱ��ȭ
        gameObject.SetActive(false);
    }








}
