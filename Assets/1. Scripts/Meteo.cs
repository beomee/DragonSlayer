using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour
{
    Enemy_RedDragon redDragon; // �� ��ũ��Ʈ ��������.
    public GameObject explosion; // �����ϴ� ������ ��������
    Transform player;
    Vector3 playerPos; // �÷��̾��� ��ġ���� �����ϴ� ���� 
    MeshRenderer mesh;

    const float meteoAttackDamage = 1.5f;

    private void Start()
    {
        redDragon = GameObject.Find("Enemy(RedDragon)").GetComponent<Enemy_RedDragon>();
        player = GameObject.Find("Player").transform;
        playerPos = player.transform.position;  // ���۵ɶ�, �÷��̾��� ��ġ���� �ѹ� �޾ƿ�.
        mesh = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
           
            explosion.gameObject.SetActive(true);
            mesh.enabled = false; // � �޽� ������ �ʰ�
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
        // ���׿���, �÷��̾��� ��ġ�� �޾ƿ� ������ ��� �̵� 
        transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * 30f);
        // transform.position = Vector3.Lerp(transform.position, playerPos, 0.1f);
        // ���׿��� ��ġ�� �޾ƿ� �÷��̾��� ��ġ������ �̵��ϰ� ��. (Lerp ���� ���ڴ� ���� ���� ����) => ������ ���������� ����������� �ӵ��� ������. 

        //transform.position = Vector3.Lerp(transform.position, player.position, 0.0135f); => ��� ���� �ڵ�� ����� ��������,
        //player.position�� �ǽð����� �÷��̾ �ִ� �������� ��� �޾Ƽ� ����ó�� ����� �Ǵ°�. 

    }



}
