using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayEnemyAttackCollision_Shouting : MonoBehaviour
{

    public Enemy_GrayDragon grayDragon;
    public Collider collider_Gray;
    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }


    private void OnTriggerEnter(Collider other)
    {

        // �÷��̾ Ÿ���ϴ°� "Enemy" �� ���, ���ʹ� Ŭ������ �������Լ��� ȣ�� 
        if (other.CompareTag("Player"))
        {
                other.GetComponent<Player>().Damaged(grayDragon.enemystr);  // �÷��̾�� �������� ������ �Լ�.
            collider_Gray.enabled = false;
        }

    }



    private IEnumerator AutoDisable()
    {
        // 0.2�� �Ŀ� ������Ʈ�� ��������� �Ѵ�.
        yield return new WaitForSeconds(0.2f);
        collider_Gray.enabled = true;
        gameObject.SetActive(false);

    }


}
