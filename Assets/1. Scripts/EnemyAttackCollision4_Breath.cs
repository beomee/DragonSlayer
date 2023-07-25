using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollision4_Breath : MonoBehaviour
{
    
    public Enemy_RedDragon redDragon;
    public Collider breathcollider;

    const float breathAttackDamage = 1.5f;
    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }


    private void OnTriggerEnter(Collider other)
    {

        // �÷��̾ Ÿ���ϴ°� "Enemy" �� ���, ���ʹ� Ŭ������ �������Լ��� ȣ�� 
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<Player>().Damaged(redDragon.enemystr * breathAttackDamage);  // �÷��̾�� �������� ������ �Լ�.
            Player.instance.Damaged(redDragon.enemystr * breathAttackDamage);
            print(redDragon.enemystr * breathAttackDamage);
            breathcollider.enabled = false;
        }

    }



    private IEnumerator AutoDisable()
    {
        // 0.5�� �Ŀ� ������Ʈ�� ��������� �Ѵ�.
        yield return new WaitForSeconds(1.5f);
        breathcollider.enabled = true;
        gameObject.SetActive(false);

    }


}
