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


    public float gravityScale; // �߷� ũ��
    public float jumpPower;    // �����ϴ� �� 

    float _y; // y�� ������ ���� 



    private void Awake()
    {
        Cursor.visible = false;  // ���콺 Ŀ�� �Ⱥ��̰� �ϱ� (�̵��� �� ���ϰ�, �������� �ʰ�) 
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
