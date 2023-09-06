using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownEnemyAttackCollision : MonoBehaviour
{

    public Enemy_BrownDragon brownDragon;
    public Collider collider_Brown;

    // 공격타점 생성 후 비활성화
    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }


    private void OnTriggerEnter(Collider other)
    {

        // 플레이어가 타격하는게 "Enemy" 일 경우, 에너미 클래스의 데미지함수를 호출 
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Damaged(brownDragon.enemystr); 
            collider_Brown.enabled = false;
        }

    }


    // 자동으로 공격타점 생성 및 해제
    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.2f);
        collider_Brown.enabled = true;
        gameObject.SetActive(false);

    }


}
