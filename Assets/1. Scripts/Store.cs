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
    public GameObject store; //���� ������Ʈ 


    // Start is called before the first frame update
    void Start()
    {
        //������Ʈ ��Ȱ��ȭ 


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

                //���̽�����ġ, ���� & ����, ����, ������ �ð�
                Debug.DrawRay(transform.position, transform.forward * MaxDistance, Color.blue, 0.1f);


                //���̽�����ġ, ���̹���, �����浹��ȯ, ���̱��̰�)
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

                // ��ȣ�ۿ� 
                //// �ð������ϱ�
                //uicool += Time.deltaTime;

                //// 3�ʰ� �����ڿ� ui���� 
                //if (uicool >= 3)
                //{
                //    InteractionUi.SetActive(false);
                //}

            }



        }

     
        



}
