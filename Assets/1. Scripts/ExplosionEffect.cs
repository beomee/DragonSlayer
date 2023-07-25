using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{

    public GameObject meteo; // 메테오의 위치값을 가져올 변수
    public Enemy_RedDragon redDragon;
    ParticleSystem ps;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Environment")
        {
            ExplosionStart();
            Destroy(gameObject, 4);
        }

        if ((collision.gameObject.tag == "Player"))
        {
            ExplosionStart();
            Destroy(gameObject, 4);

        }
    }
    private void OnTriggerEnter(Collider collision)
    {

    }


    public void ExplosionStart()
    {
        print("익스플로젼");
        ps.Play();
        meteo.transform.position = transform.position;
        transform.gameObject.SetActive(true);
        GetComponent<Player>().Damaged(redDragon.enemystr);
        StartCoroutine(ExplosionEnd());

    }

    IEnumerator ExplosionEnd()
    {
        yield return new WaitForSeconds(3f);
        transform.gameObject.SetActive(false);
        Destroy(gameObject);

    }
}
