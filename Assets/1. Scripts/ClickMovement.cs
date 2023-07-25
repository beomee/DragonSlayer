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





        // ���콺�� ��Ŭ�� �� �� 
        if (Input.GetMouseButton(1))
        {

            // ĳ���Ͱ� �ٴڿ� �������� �������ְ� �ȴ�.
            RaycastHit hit;


            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit,float.MaxValue,clickLayer))
            {
         
                    SetDestination(hit.point);
            }

        }

        Move();

    }

    // Ŭ���� ������ ���� �κ��� �ٴ��϶��� �۵��ǰ�!

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
