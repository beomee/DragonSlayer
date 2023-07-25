using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove3 : MonoBehaviour
{

    public GameObject player;   // 따라다닐 중심(타겟)이 될 게임오브젝트

    public float offsetX;   // 따라다닐 중심과 카메라 사이의 거리
    public float offsetY;
    public float offsetZ;

    float delayTime = 5;  // 카메라가 타겟을 따라다니는 시간(속도의 개념)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //카메라가 플레이어를 따라다니는 코드 
        Vector3 FixedPos = new Vector3(player.transform.position.x + offsetX, player.transform.position.y + offsetY, player.transform.position.z + offsetZ);

        transform.position = Vector3.Lerp(transform.position, FixedPos, Time.deltaTime * delayTime);
    }
}
