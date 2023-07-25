using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    public LayerMask clickLayer;


    Camera camera;
   


    bool isMove;
    Vector3 destination;



    private void Awake()
    {


    }
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;



    }

    // Update is called once per frame
    void Update()
    {





        // 마우스를 우클릭 할 시 
        if (Input.GetMouseButton(1))
        {

            // 캐릭터가 바닥에 목적지를 설정해주게 된다.
            RaycastHit hit;


            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit,float.MaxValue,clickLayer))
            {
         
                    SetDestination(hit.point);
            }

        }

        Move();

    }

    // 클릭을 했을때 닿은 부분이 바닥일때만 작동되게!

    void SetDestination(Vector3 dest)
    {
        destination = dest;

        isMove = true;

    }

    void Move()
    {

        if (isMove)
        {
            var dir = destination - transform.position;
            transform.forward = dir;
            transform.position += dir.normalized * Time.deltaTime * 3f;
        }

        if (Vector3.Distance(transform.position, destination) <= 0.1f)
        {
            isMove = false;

        }



    }

}
