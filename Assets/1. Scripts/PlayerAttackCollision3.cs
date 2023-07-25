using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollision3 : MonoBehaviour
{
    //public GameObject criticalUi;  // ũ��Ƽ�� UI�� ���� ���ӿ�����Ʈ ����
    public GameObject effect_MonsterHit_Critical;
    public GameObject effect_MonsterHit;
    //public ParticleSystem ps;
    public Collider coll;
    int randomInt;  // ũ��Ƽ�� Ȯ���� �����ϱ� ���� ���� 

    [SerializeField]
    private string criticalSound; // ũ��Ƽ�� �������� ������ �� ���� ���� 

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

    

        // �÷��̾ Ÿ���ϴ°� "Enemy" �� ���, ���ʹ� Ŭ������ �������Լ��� ȣ�� 
        if (other.CompareTag("Enemy"))
        {
          
            coll.enabled = false;

           

            if (randomInt > 1)
            {
                other.GetComponent<Enemy_RedDragon>().Damaged(Json.instance.data.str);
                Instantiate(effect_MonsterHit, transform.position, transform.rotation);
                AudioManager.instance.PlaySE(monsterDamaged3, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f)); // ù��° ���ݿ� ���� ������ �Ҹ� 
                

            }

            else 
            {

                other.GetComponent<Enemy_RedDragon>().Damaged(Json.instance.data.str * Json.instance.data.criticalStr);
                Instantiate(effect_MonsterHit, transform.position, transform.rotation);
                Instantiate(effect_MonsterHit_Critical, transform.position, transform.rotation);
                AudioManager.instance.PlaySE(monsterDamaged3, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f)); // ù��° ���ݿ� ���� ������ �Ҹ�  
                AudioManager.instance.PlaySE(criticalSound, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f));

            }

        }

        // �÷��̾ Ÿ���ϴ°� "Enemy_Brown" �� ���, ���ʹ� Ŭ������ �������Լ��� ȣ�� 
        if (other.CompareTag("Enemy_Brown"))
        {
           

            coll.enabled = false;

           

            if (randomInt > 1)
            {
                other.GetComponent<Enemy_BrownDragon>().Damaged(Json.instance.data.str);
                Instantiate(effect_MonsterHit, transform.position, transform.rotation);
                AudioManager.instance.PlaySE(monsterDamaged3, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f)); // ù��° ���ݿ� ���� ������ �Ҹ� 

            }

            else
            {
                other.GetComponent<Enemy_BrownDragon>().Damaged(Json.instance.data.str * Json.instance.data.criticalStr);
                Instantiate(effect_MonsterHit, transform.position, transform.rotation);
                Instantiate(effect_MonsterHit_Critical, transform.position, transform.rotation);
                AudioManager.instance.PlaySE(monsterDamaged3, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f)); // ù��° ���ݿ� ���� ������ �Ҹ�  
                AudioManager.instance.PlaySE(criticalSound, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f));
            }

        }

        // �÷��̾ Ÿ���ϴ°� "Enemy_Gray" �� ���, ���ʹ� Ŭ������ �������Լ��� ȣ�� 
        if (other.CompareTag("Enemy_Gray"))
        {
            coll.enabled = false;

     

            if (randomInt > 1) // �Ϲݰ��� �϶� 
            {
                other.GetComponent<Enemy_GrayDragon>().Damaged(Json.instance.data.str);
                Instantiate(effect_MonsterHit, transform.position, transform.rotation);
                AudioManager.instance.PlaySE(monsterDamaged, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f)); // ù��° ���ݿ� ���� ������ �Ҹ� 

            }

            else // ũ��Ƽ�� ���� �϶� 
            {
                other.GetComponent<Enemy_GrayDragon>().Damaged(Json.instance.data.str * Json.instance.data.criticalStr);
                Instantiate(effect_MonsterHit, transform.position, transform.rotation);
                Instantiate(effect_MonsterHit_Critical, transform.position, transform.rotation);
                AudioManager.instance.PlaySE(monsterDamaged, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f)); // ù��° ���ݿ� ���� ������ �Ҹ� 
                AudioManager.instance.PlaySE(criticalSound, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f));

            }

        }

        // �÷��̾ Ÿ���ϴ°� "Enemy_Purple" �� ���, ���ʹ� Ŭ������ �������Լ��� ȣ�� 
        if (other.CompareTag("Enemy_Purple"))
        {
            coll.enabled = false;

           

            if (randomInt > 1)
            {
                other.GetComponent<Enemy_PurpleDragon>().Damaged(Json.instance.data.str);
                Instantiate(effect_MonsterHit, transform.position, transform.rotation);
                AudioManager.instance.PlaySE(monsterDamaged3, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f)); // ù��° ���ݿ� ���� ������ �Ҹ� 
            }

            else
            {
                other.GetComponent<Enemy_PurpleDragon>().Damaged(Json.instance.data.str * Json.instance.data.criticalStr);
                Instantiate(effect_MonsterHit, transform.position, transform.rotation);
                Instantiate(effect_MonsterHit_Critical, transform.position, transform.rotation);
                AudioManager.instance.PlaySE(monsterDamaged3, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f)); // ù��° ���ݿ� ���� ������ �Ҹ�  
                AudioManager.instance.PlaySE(criticalSound, Random.Range(0.95f, 1.05f), Random.Range(0.85f, 1.0f));
            }

        }
    }



    private IEnumerator AutoDisable()
    {

        // 0.2�� �Ŀ� ������Ʈ�� ��������� �Ѵ�.
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
