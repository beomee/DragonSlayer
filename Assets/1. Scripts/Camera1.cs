using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour
{
    public float rotSpeed;     // ȸ�� �ӵ�

    float clampX;

    bool isDownMax; // ���콺�� ���� �� �ִ� �ִ�ġ�� �����ߴ��� �Ǵ� 
    bool isUpMax;   // ���콺�� �ø� �� �ִ� �ִ�ġ�� �����ߴ��� �Ǵ�

    public Transform player;
    //public GameObject storeImg;

    // Start is called before the first frame update


    private void Awake()
    {

    }
    void Start()
    {
        Cursor.visible = false;  // ���콺 Ŀ�� �Ⱥ��̰� �ϱ� (�̵��� �� ���ϰ�, �������� �ʰ�) 
        Cursor.lockState = CursorLockMode.Locked;



    }

    // Update is called once per frame
    void Update()
    {

        //// ��Ƽ�� ����
        //if (!Inventory.inventoryActivated)
        //{
        //    float rotX = Input.GetAxis("Mouse Y") * Time.deltaTime * rotSpeed;

        //    clampX -= rotX;
        //    clampX = Mathf.Clamp(clampX, -30, 30);

        //    transform.eulerAngles = new Vector3(clampX, transform.eulerAngles.y, transform.eulerAngles.z);
        //}
    }


}
