using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_ : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    public CharacterController cc;
    Animator animator;


    public float gravityScale; // 중력 크기
    public float jumpPower;    // 점프하는 힘 

    float _y; // y축 관리용 변수 



    private void Awake()
    {
        Cursor.visible = false;  // 마우스 커서 안보이게 하기 (이동할 건 다하고, 보이지만 않게) 
        Cursor.lockState = CursorLockMode.Locked;
        cc.enabled = true;
        //cameraArm.position = characterBody.position + new Vector3(0, 2f, 0);

    }
    // Start is called before the first frame update
    void Start()
    {



        animator = characterBody.GetComponent<Animator>();



    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();




    }

    private void FixedUpdate()
    {

    }
    private void Move()
    {
        Vector2 moveinput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool ismove = moveinput.magnitude != 0;
        animator.SetBool("isMove", ismove);
        if (ismove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveinput.y + lookRight * moveinput.x;

            characterBody.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * 10f;
        }



    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }

        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);

    }

}
