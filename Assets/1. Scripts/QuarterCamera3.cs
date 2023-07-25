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
        // ī�޶��� ��ġ���� �÷��̾��� ��ġ�� �� ���� ���������� �ֱ�
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
