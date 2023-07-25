using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollision3 : MonoBehaviour
{
    //public GameObject criticalUi;  // 크리티컬 UI를 담을 게임오브젝트 변수
    public GameObject effect_MonsterHit_Critical;
    public GameObject effect_MonsterHit;
    //public ParticleSystem ps;
    public Collider coll;
    int randomInt;  // 크리티컬 확률을 저장하기 위한 변수 

    [SerializeField]
    private string criticalSound; // 크리티컬 데미지를 입혔을 때 나는 사운드 

    [SerializeField]
    private string monsterDamaged;

    [SerializeField]
    private string monsterDamaged2;

    [SerializeField]
    private string monsterDamaged3;

    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }

  
    private void OnTriggerEnter(Collider other)
    {
        randomInt = Random.Range(1, 6);

    

        // 플레이어가 타격하는게 "Enemy" 일 경우, 에너미 클래스의 데미지함수를 호출 
        if (other.CompareTag("Enemy"))
        {
          
            coll.enabled = false;

           

            if (randomInt > 1)
            {
                other.GetComponent<Enemy_RedDragon>().Damaged(Json.instance.data.str);
                Instantiate(effect_MonsterHit, transform.position, transform.rotation);
                AudioManager.instance.PlaySE(monsterDamaged3, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f)); // 첫번째 공격에 맞은 몬스터의 소리 
                

            }

            else 
            {

                other.GetComponent<Enemy_RedDragon>().Damaged(Json.instance.data.str * Json.instance.data.criticalStr);
                Instantiate(effect_MonsterHit, transform.position, transform.rotation);
                Instantiate(effect_MonsterHit_Critical, transform.position, transform.rotation);
                AudioManager.instance.PlaySE(monsterDamaged3, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f)); // 첫번째 공격에 맞은 몬스터의 소리  
                AudioManager.instance.PlaySE(criticalSound, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f));

            }

        }

        // 플레이어가 타격하는게 "Enemy_Brown" 일 경우, 에너미 클래스의 데미지함수를 호출 
        if (other.CompareTag("Enemy_Brown"))
        {
           

            coll.enabled = false;

           

            if (randomInt > 1)
            {
                other.GetComponent<Enemy_BrownDragon>().Damaged(Json.instance.data.str);
                Instantiate(effect_MonsterHit, transform.position, transform.rotation);
                AudioManager.instance.PlaySE(monsterDamaged3, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f)); // 첫번째 공격에 맞은 몬스터의 소리 

            }

            else
            {
                other.GetComponent<Enemy_BrownDragon>().Damaged(Json.instance.data.str * Json.instance.data.criticalStr);
                Instantiate(effect_MonsterHit, transform.position, transform.rotation);
                Instantiate(effect_MonsterHit_Critical, transform.position, transform.rotation);
                AudioManager.instance.PlaySE(monsterDamaged3, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f)); // 첫번째 공격에 맞은 몬스터의 소리  
                AudioManager.instance.PlaySE(criticalSound, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f));
            }

        }

        // 플레이어가 타격하는게 "Enemy_Gray" 일 경우, 에너미 클래스의 데미지함수를 호출 
        if (other.CompareTag("Enemy_Gray"))
        {
            coll.enabled = false;

     

            if (randomInt > 1) // 일반공격 일때 
            {
                other.GetComponent<Enemy_GrayDragon>().Damaged(Json.instance.data.str);
                Instantiate(effect_MonsterHit, transform.position, transform.rotation);
                AudioManager.instance.PlaySE(monsterDamaged, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f)); // 첫번째 공격에 맞은 몬스터의 소리 

            }

            else // 크리티컬 공격 일때 
            {
                other.GetComponent<Enemy_GrayDragon>().Damaged(Json.instance.data.str * Json.instance.data.criticalStr);
                Instantiate(effect_MonsterHit, transform.position, transform.rotation);
                Instantiate(effect_MonsterHit_Critical, transform.position, transform.rotation);
                AudioManager.instance.PlaySE(monsterDamaged, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f)); // 첫번째 공격에 맞은 몬스터의 소리 
                AudioManager.instance.PlaySE(criticalSound, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f));

            }

        }

        // 플레이어가 타격하는게 "Enemy_Purple" 일 경우, 에너미 클래스의 데미지함수를 호출 
        if (other.CompareTag("Enemy_Purple"))
        {
            coll.enabled = false;

           

            if (randomInt > 1)
            {
                other.GetComponent<Enemy_PurpleDragon>().Damaged(Json.instance.data.str);
                Instantiate(effect_MonsterHit, transform.position, transform.rotation);
                AudioManager.instance.PlaySE(monsterDamaged3, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f)); // 첫번째 공격에 맞은 몬스터의 소리 
            }

            else
            {
                other.GetComponent<Enemy_PurpleDragon>().Damaged(Json.instance.data.str * Json.instance.data.criticalStr);
                Instantiate(effect_MonsterHit, transform.position, transform.rotation);
                Instantiate(effect_MonsterHit_Critical, transform.position, transform.rotation);
                AudioManager.instance.PlaySE(monsterDamaged3, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f)); // 첫번째 공격에 맞은 몬스터의 소리  
                AudioManager.instance.PlaySE(criticalSound, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f));
            }

        }
    }



    private IEnumerator AutoDisable()
    {

        // 0.2초 후에 오브젝트가 사라지도록 한다.
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        coll.enabled = true;


    }

    public IEnumerator ParticleStart()
    {
        Instantiate(effect_MonsterHit_Critical, transform.position, transform.rotation);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }


}
