using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBgmChange : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        // �÷��̾�� ������ �ؽ�Ʈ�� ���İ��� 255�� �ٲ��ְ�, 3�� �� 0 ���� �ٲٱ� . 
        if (other.CompareTag("Player"))
        {

            AudioManager.instance.PlayBossSceneBgm();
        }



    }
}
