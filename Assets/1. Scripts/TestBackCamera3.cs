using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBackCamera3 : MonoBehaviour
{
    public GameObject player;   // 따라다닐 중심(타겟)이 될 게임오브젝트

    public float rotSpeed;  // 회전 속도

    public float delayTime;  // 카메라가 타겟을 따라다니는 시간(속도의 개념)

    bool isDownMax; // 마우스를 내릴 수 있는 최대치에 도착했는지 판단하는 코드
    bool isUpMax;  // 마우스를 올릴 수 있는 최대치에 도착했는지 판단하는 코드 


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        // < 3인칭 카메라의 마우스로 움직이는 기능 > 

        // 마우스의 상하 움직임 저장
        float rotX = Input.GetAxis("Mouse Y") * Time.deltaTime * rotSpeed;

        // 마우스의 좌우 움직임 저장
        float rotY = Input.GetAxis("Mouse X") * Time.deltaTime * rotSpeed;



        // 마우스를 내리다가 각도가 30이상이 되면 최대라고 저장하기
        if (transform.eulerAngles.x >= 30 && transform.eulerAngles.x <= 180)  // 30도 이상과 350도 이하라는 조건이 겹치는 경우를 없애기 위해 180이라는 조건을 추가로 넣음.
        {
            isDownMax = true;  // 최대치에 도착했음을 저장
        }

        // 마우스를 올리다가 각도가 -10(350)이하가 되면 최대라고 저장시키기 
        else if (transform.eulerAngles.x <= 350 && transform.eulerAngles.x >= 180)
        {
            isUpMax = true;
        }

        // 마우스를 위로 올렸다면 최대치 제한 해제시키기 (제한을 푸는 코드는 꼭 중간에 작성하기)
        if (rotX > 0)
        {
            isDownMax = false;
        }

        // 마우스를 아래로 내렸다면 최대치 제한 해제시키기 (제한을 푸는 코드는 꼭 중간에 작성하기)
        else if (rotX < 0)
        {
            isUpMax = false;
        }

        // 최대치에 도착하지 않았을때만 상하 회전하게끔 
        if (!isDownMax && !isUpMax)
        {

            transform.RotateAround(player.transform.position, Vector3.right, -rotX); // 누구를 기준으로, 어떤 축으로, 얼마만큼 

        }

        // 캐릭터를 기준으로 좌우 회전하는 코드
        transform.RotateAround(player.transform.position, Vector3.up, rotY);

        transform.LookAt(player.transform.position);




    }

}




