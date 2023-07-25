using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour
{
    public float rotSpeed;     // 회전 속도

    float clampX;

    bool isDownMax; // 마우스를 내릴 수 있는 최대치에 도착했는지 판단 
    bool isUpMax;   // 마우스를 올릴 수 있는 최대치에 도착했는지 판단

    public Transform player;
    //public GameObject storeImg;

    // Start is called before the first frame update


    private void Awake()
    {

    }
    void Start()
    {
        Cursor.visible = false;  // 마우스 커서 안보이게 하기 (이동할 건 다하고, 보이지만 않게) 
        Cursor.lockState = CursorLockMode.Locked;



    }

    // Update is called once per frame
    void Update()
    {

        //// 액티브 셀프
        //if (!Inventory.inventoryActivated)
        //{
        //    float rotX = Input.GetAxis("Mouse Y") * Time.deltaTime * rotSpeed;

        //    clampX -= rotX;
        //    clampX = Mathf.Clamp(clampX, -30, 30);

        //    transform.eulerAngles = new Vector3(clampX, transform.eulerAngles.y, transform.eulerAngles.z);
        //}
    }


}
