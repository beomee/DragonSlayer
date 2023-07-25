using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarterCamera3 : MonoBehaviour
{
    public Transform player;

    public float cameraSpeed;

    Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        // 카메라의 위치에서 플레이어의 위치를 뺀 값을 오프셋으로 넣기
        offset = transform.position - player.position;
        

    }

    void LateUpdate()
    {


        Vector3 target = offset + player.position;

        transform.position = Vector3.Lerp(transform.position, target, cameraSpeed * Time.deltaTime);



    }



    // Update is called once per frame
    void Update()
    {
        


    }



}
