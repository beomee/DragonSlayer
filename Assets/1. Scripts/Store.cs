using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{

    RaycastHit hit;
    float MaxDistance = 2f;
    int dir;
    public Transform player;
    public GameObject InteractionUi;
    float uicool;
    public GameObject Character;
    public GameObject store; //상점 오브젝트 


    // Start is called before the first frame update
    void Start()
    {
        //컴포넌트 비활성화 


    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Player")
        {
            Debug.Log("ddda");
        }
    }

    // Update is called once per frame
    void Update()
    {
//          

                //레이시작위치, 방향 & 길이, 색깔, 유지할 시간
                Debug.DrawRay(transform.position, transform.forward * MaxDistance, Color.blue, 0.1f);


                //레이시작위치, 레이방향, 레이충돌반환, 레이길이값)
                if (Physics.Raycast(transform.position, transform.forward * MaxDistance, out hit, MaxDistance))
                {

                    if (hit.transform.tag == "Player")
                    {
                Debug.Log("ddda");
                        if (Input.GetKeyDown(KeyCode.G))
                       {


                    Character.GetComponent<PlayerMove_>().enabled = false;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.Confined;

                }


                }

                // 상호작용 
                //// 시간설정하기
                //uicool += Time.deltaTime;

                //// 3초가 지난뒤에 ui끄기 
                //if (uicool >= 3)
                //{
                //    InteractionUi.SetActive(false);
                //}

            }



        }

     
        



}
