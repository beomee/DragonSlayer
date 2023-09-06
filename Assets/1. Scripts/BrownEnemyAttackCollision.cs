using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownEnemyAttackCollision : MonoBehaviour
{

    public Enemy_BrownDragon brownDragon;
    public Collider collider_Brown;

    // ����Ÿ�� ���� �� ��Ȱ��ȭ
    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }


    private void OnTriggerEnter(Collider other)
    {

        // �÷��̾ Ÿ���ϴ°� "Enemy" �� ���, ���ʹ� Ŭ������ �������Լ��� ȣ�� 
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Damaged(brownDragon.enemystr); 
            collider_Brown.enabled = false;
        }

    }


    // �ڵ����� ����Ÿ�� ���� �� ����
    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.2f);
        collider_Brown.enabled = true;
        gameObject.SetActive(false);

    }


}
