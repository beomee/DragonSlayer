using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
  

    // �������� Ȱ��ȭ
    private void OnEnable()
    {
        Invoke("ResetAttack", 0.3f);

    }
    void ResetAttack()
    {
        // ��Ȱ��ȭ
        gameObject.SetActive(false);
    }








}
