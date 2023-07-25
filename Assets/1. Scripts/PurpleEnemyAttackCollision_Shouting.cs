using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleEnemyAttackCollision_Shouting : MonoBehaviour
{

    public Enemy_PurpleDragon purpleDragon;
    public Collider collider_Purple;
    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }


    private void OnTriggerEnter(Collider other)
    {

        // �÷��̾ Ÿ���ϴ°� "Enemy" �� ���, ���ʹ� Ŭ������ �������Լ��� ȣ�� 
        if (other.CompareTag("Player"))
        {
                other.GetComponent<Player>().Damaged(purpleDragon.enemystr);  // �÷��̾�� �������� ������ �Լ�.
            collider_Purple.enabled = false;
        }

    }



    private IEnumerator AutoDisable()
    {
        // 0.2�� �Ŀ� ������Ʈ�� ��������� �Ѵ�.
        yield return new WaitForSeconds(0.2f);
        collider_Purple.enabled = true;
        gameObject.SetActive(false);

    }


}
