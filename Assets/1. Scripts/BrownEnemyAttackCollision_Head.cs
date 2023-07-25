using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownEnemyAttackCollision_Head : MonoBehaviour
{

    public Enemy_BrownDragon brownDragon;
    public Collider collider_Brown;
    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }


    private void OnTriggerEnter(Collider other)
    {

        // �÷��̾ Ÿ���ϴ°� "Enemy" �� ���, ���ʹ� Ŭ������ �������Լ��� ȣ�� 
        if (other.CompareTag("Player"))
        {
                other.GetComponent<Player>().Damaged(brownDragon.enemystr * 1.50f);  // �÷��̾�� �������� ������ �Լ�.
            collider_Brown.enabled = false;
        }

    }



    private IEnumerator AutoDisable()
    {
        // 0.2�� �Ŀ� ������Ʈ�� ��������� �Ѵ�.
        yield return new WaitForSeconds(0.2f);
        collider_Brown.enabled = true;
        gameObject.SetActive(false);

    }


}
