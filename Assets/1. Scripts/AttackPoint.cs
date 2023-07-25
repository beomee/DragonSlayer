using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
  


    private void OnEnable()
    {
        // 0.3초 후에 초기화 되는 코드 
        Invoke("ResetAttack", 0.3f);

    }
    void ResetAttack()
    {
        // 비활성화
        gameObject.SetActive(false);
    }








}
