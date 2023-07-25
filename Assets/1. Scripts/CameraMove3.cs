using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove3 : MonoBehaviour
{

    public GameObject player;   // ����ٴ� �߽�(Ÿ��)�� �� ���ӿ�����Ʈ

    public float offsetX;   // ����ٴ� �߽ɰ� ī�޶� ������ �Ÿ�
    public float offsetY;
    public float offsetZ;

    float delayTime = 5;  // ī�޶� Ÿ���� ����ٴϴ� �ð�(�ӵ��� ����)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ī�޶� �÷��̾ ����ٴϴ� �ڵ� 
        Vector3 FixedPos = new Vector3(player.transform.position.x + offsetX, player.transform.position.y + offsetY, player.transform.position.z + offsetZ);

        transform.position = Vector3.Lerp(transform.position, FixedPos, Time.deltaTime * delayTime);
    }
}
