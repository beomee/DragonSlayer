using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
  

    // 공격지점 활성화
    private void OnEnable()
    {
        Invoke("ResetAttack", 0.3f);

    }
    void ResetAttack()
    {
        // 비활성화
        gameObject.SetActive(false);
    }








}
