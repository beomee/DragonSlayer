using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollision : MonoBehaviour
{

    public Enemy_RedDragon redDragon;
    public Collider collider;

    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }


    private void OnTriggerEnter(Collider other)
    {

        // �÷��̾ Ÿ���ϴ°� "Enemy" �� ���, ���ʹ� Ŭ������ �������Լ��� ȣ�� 
        if (other.CompareTag("Player"))
        {
                other.GetComponent<Player>().Damaged(redDragon.enemystr);  // �÷��̾�� �������� ������ �Լ�.
            collider.enabled = false;
        }

    }



    private IEnumerator AutoDisable()
    {
        // 0.2�� �Ŀ� ������Ʈ�� ��������� �Ѵ�.
        yield return new WaitForSeconds(0.2f);
        collider.enabled = true;
        gameObject.SetActive(false);

    }


}
