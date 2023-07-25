using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour
{
    Enemy_RedDragon redDragon; // 용 스크립트 가져오기.
    public GameObject explosion; // 폭발하는 프리팹 가져오기
    Transform player;
    Vector3 playerPos; // 플레이어의 위치값을 저장하는 변수 
    MeshRenderer mesh;

    const float meteoAttackDamage = 1.5f;

    private void Start()
    {
        redDragon = GameObject.Find("Enemy(RedDragon)").GetComponent<Enemy_RedDragon>();
        player = GameObject.Find("Player").transform;
        playerPos = player.transform.position;  // 시작될때, 플레이어의 위치값을 한번 받아옴.
        mesh = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
           
            explosion.gameObject.SetActive(true);
            mesh.enabled = false; // 운석 메쉬 보이지 않게
            Enemy_RedDragon.instance.RedEnemySoundStart_Explosion();
            Destroy(gameObject, 3f);

        }

        if (collision.gameObject.tag == "Player")
        {
           
            explosion.gameObject.SetActive(true);
            mesh.enabled = false;
            collision.GetComponent<Player>().Damaged(redDragon.enemystr * meteoAttackDamage);
            Enemy_RedDragon.instance.RedEnemySoundStart_Explosion();

            Destroy(gameObject, 1f) ;
        }
    }


    private void Update()
    {
        // 메테오가, 플레이어의 위치를 받아온 값으로 계속 이동 
        transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * 30f);
        // transform.position = Vector3.Lerp(transform.position, playerPos, 0.1f);
        // 메테오의 위치를 받아온 플레이어의 위치값으로 이동하게 함. (Lerp 뒤의 숫자는 적을 수록 느림) => 러프라서 도착지점에 가까워질수록 속도가 느려짐. 

        //transform.position = Vector3.Lerp(transform.position, player.position, 0.0135f); => 언뜻 위의 코드와 비슷해 보이지만,
        //player.position은 실시간으로 플레이어가 있는 포지션을 계속 받아서 유도처럼 기능이 되는것. 

    }



}
