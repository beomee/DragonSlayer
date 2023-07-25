using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBgmChange : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        // 플레이어와 닿으면 텍스트의 알파값을 255로 바꿔주고, 3초 뒤 0 으로 바꾸기 . 
        if (other.CompareTag("Player"))
        {

            AudioManager.instance.PlayBossSceneBgm();
        }



    }
}
