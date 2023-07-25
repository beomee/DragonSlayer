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

        // 플레이어가 타격하는게 "Enemy" 일 경우, 에너미 클래스의 데미지함수를 호출 
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<Player>().Damaged(redDragon.enemystr * breathAttackDamage);  // 플레이어에게 데미지를 입히는 함수.
            Player.instance.Damaged(redDragon.enemystr * breathAttackDamage);
            print(redDragon.enemystr * breathAttackDamage);
            breathcollider.enabled = false;
        }

    }



    private IEnumerator AutoDisable()
    {
        // 0.5초 후에 오브젝트가 사라지도록 한다.
        yield return new WaitForSeconds(1.5f);
        breathcollider.enabled = true;
        gameObject.SetActive(false);

    }


}
