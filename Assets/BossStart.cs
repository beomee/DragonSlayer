using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    bool isCollider = false;


    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 타격하는게 "Enemy" 일 경우, 에너미 클래스의 데미지함수를 호출 
        if (other.CompareTag("Enemy"))
        {
            Enemy_RedDragon.instance.SkillStart();
            Enemy_RedDragon.instance.eState = Enemy_RedDragon.EnemyStete.Attack;
            GetComponent<BoxCollider>().enabled = false;

            isCollider = false;
        }

        // 플레이어가 타격하는게 "Enemy_Gray" 일 경우, 에너미 클래스의 데미지함수를 호출 
        if (other.CompareTag("Enemy_Gray"))
        {
            Enemy_GrayDragon.instance.SkillStart_Gray();
            Enemy_GrayDragon.instance.eState = Enemy_GrayDragon.EnemyStete.Attack;
            GetComponent<BoxCollider>().enabled = false;

            isCollider = false;

        }

        // 플레이어가 타격하는게 "Enemy_Purple" 일 경우, 에너미 클래스의 데미지함수를 호출 
        if (other.CompareTag("Enemy_Purple"))
        {
            Enemy_PurpleDragon.instance.SkillStart_Purple();
            Enemy_PurpleDragon.instance.eState = Enemy_PurpleDragon.EnemyStete.Attack;
            GetComponent<BoxCollider>().enabled = false;

            isCollider = false;
        }

        // 플레이어가 타격하는게 "Enemy_Brown" 일 경우, 에너미 클래스의 데미지함수를 호출 
        if (other.CompareTag("Enemy_Brown"))
        {
           
            Enemy_BrownDragon.instance.SkillStart_Brown();
            Enemy_BrownDragon.instance.eState = Enemy_BrownDragon.EnemyStete.Attack;
            GetComponent<BoxCollider>().enabled = false;

            isCollider = false;

        }

        //if (other.gameObject.tag == "Store")
        //{
        //    print("StoreOk");
        //    Player.instance.isStore = true;
        //    //Player.instance.isNpc = true;
        //}

        //if (other.gameObject.tag == "Npc")
        //{
        //    Player.instance.isNpc = true;
        //    //Player.instance.isStore = true;
        //}

        //if (other.gameObject.tag == "Item")
        //{
        //    Player.instance.actionController.ItemInfoAppear();
        //    //Player.instance.isStore = false;
        //    //Player.instance.isNpc = false;

        //}


    }





    //private void OnTriggerStay(Collider hit)
    //{
    //    if (hit.gameObject.tag == "Store")
    //    {
    //        print("StoreOk");
    //        Player.instance.isStore = true;
    //        //Player.instance.isNpc = false;
    //    }

    //    if (hit.gameObject.tag == "Npc")
    //    {
    //        Player.instance.isNpc = true;
    //        //Player.instance.isStore = false;
    //    }

    //    if (hit.gameObject.tag == "Item")
    //    {
    //        Player.instance.actionController.ItemInfoAppear();
    //        //Player.instance.isStore = false;
    //        //Player.instance.isNpc = false;

    //    }
    //}

    //private void OnTriggerExit(Collider hit)
    //{
    //    if (hit.gameObject.tag == "Store")
    //    {
    //        Player.instance.isStore = false;
    //        //Player.instance.isNpc = false;
    //    }

    //    if (hit.gameObject.tag == "Npc")
    //    {
    //        Player.instance.isNpc = false;
    //        //Player.instance.isStore = false;
    //    }

    //    if (hit.gameObject.tag == "Item")
    //    {
    //        //Player.instance.actionController.ItemInfoAppear();
    //        //Player.instance.isStore = false;
    //        //Player.instance.isNpc = false;

    //    }
    //}

    private void Update()
    {

        if (Enemy_RedDragon.instance.eState == Enemy_RedDragon.EnemyStete.Walk && Enemy_RedDragon.instance.distance < 30f) // 추가로 일정 거리 안 일때만 적용 되도록 해보자.
        {
            GetComponent<BoxCollider>().enabled = true;
        }

        if (Enemy_GrayDragon.instance.eState == Enemy_GrayDragon.EnemyStete.Walk && Enemy_GrayDragon.instance.distance < 30f) // 추가로 일정 거리 안 일때만 적용 되도록 해보자.
        {
            GetComponent<BoxCollider>().enabled = true;
        }

        if (Enemy_BrownDragon.instance.eState == Enemy_BrownDragon.EnemyStete.Walk && Enemy_BrownDragon.instance.distance < 30f) // 추가로 일정 거리 안 일때만 적용 되도록 해보자.
        {
            GetComponent<BoxCollider>().enabled = true;
        }

        if (Enemy_PurpleDragon.instance.eState == Enemy_PurpleDragon.EnemyStete.Walk && Enemy_PurpleDragon.instance.distance < 30f) // 추가로 일정 거리 안 일때만 적용 되도록 해보자.
        {
            GetComponent<BoxCollider>().enabled = true;
        }




        //if (Enemy_RedDragon.instance.distance > 20.0f /*&& Enemy_RedDragon.instance.enemyhp >= 0.0f*/)
        //{
        //    GetComponent<BoxCollider>().enabled = true;
        //}

        //if (Enemy_GrayDragon.instance.distance > 20.0f && Enemy_GrayDragon.instance.enemyhp >= 0.0f)
        //{
        //    GetComponent<BoxCollider>().enabled = true;
        //}

        //if (isCollider)
        //{ 
        //  if (Enemy_GrayDragon.instance.enemyhp <= 0.0f)
        //  {
        //    GetComponent<BoxCollider>().enabled = true;
        //        isCollider = true;
        //  }

        //if (Enemy_PurpleDragon.instance.enemyhp <= 0.0f)
        //  {
        //    GetComponent<BoxCollider>().enabled = true;
        //        isCollider = true;
        //  }

        //    }
        //    //if (Enemy_PurpleDragon.instance.distance > 20.0f && Enemy_PurpleDragon.instance.enemyhp >= 0.0f)
        //    //{
        //    //    GetComponent<BoxCollider>().enabled = true;
        //    //}

        //    //if (Enemy_BrownDragon.instance.distance > 20.0f && Enemy_BrownDragon.instance.enemyhp >= 0.0f)
        //    //{
        //    //    GetComponent<BoxCollider>().enabled = true;
        //    //}
    }
}
