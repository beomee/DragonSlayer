using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float gravityScale; // �߷� ũ��
    public float jumpPower;    // �����ϴ� �� 
    public float rotSpeed;     // ȸ�� �ӵ�
    public bool toggleCameraRotation;    // art�� ������ ���콺�� �����̸� �ֺ��� �� �� �ִ� ������ ���� ���� 
    public float smoothness = 1000f;
    public bool isBlock = false;
    public bool isJump = false;

    public float Speed = 10.0f;

    public float rotateSpeed = 10.0f;       // ȸ�� �ӵ�

    float h, v;

    float forwardSpeed = 10.0f;
    float jumpSpeed = 10.0f;

    bool isAttacking = false;
    public bool jumpStart;

    float _y; // y�� ������ ���� 
    Player player;
    CharacterController cc;  // ĳ������Ʈ�ѷ� ���� ���� 
    Animator anim;           // �ִϸ����� ���� ����
    Camera camera; // ī�޶� ���� ���� 


    [SerializeField]
    private string Player_Walk; // �÷��̾ ���� �� ������ �Ҹ��� ���� ���� 

    [SerializeField]
    private string Player_Run; // �÷��̾ ���� �� ������ �Ҹ��� ���� ���� 

    [SerializeField]
    private string Player_Jump; // �÷��̾ ���� �� ������ �Ҹ��� ���� ���� 

    public float speed, runspeed; // �÷��̾ �Ȱ� �޸��� �ӵ��� ������ ���� 

    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        cc.enabled = true;
        camera = Camera.main;
        player = GetComponent<Player>();

    
    }

    public void isAttackEnd()
    {
        //anim.SetBool("isAttacking", false);
        isAttacking = false;
    }

    public void isAttackStart()

    {
        //anim.SetBool("isAttacking", true);
        isAttacking = true;
    }

    // Update is called once per frame
    void Update()
    {

        //  �߷� ����
        _y -= gravityScale * Time.deltaTime;


        //// ������ ���� �������� �߷��� �ۿ��ϵ��� ����.
        //if (!cc.isGrounded /*&& !isMouseLeftPressed*/)
        //{

        //}




        PlayerBlock();
        PlayerWalk();

        //if (!Inventory.inventoryActivated)
        //{

        if (isAttacking == false)
         {

           PlayerAttack_Strong();
           PlayerJump();
          
           
        }


        if (Input.GetMouseButton(0)) // ���� ���콺�� ���� ���¶��(���� ���̶��)
        {
            toggleCameraRotation = true;  // �ѷ����� Ȱ��ȭ
        }

        else
        {
            toggleCameraRotation = false;  // �ѷ����� ��Ȱ��ȭ
        }

        bool isMouseLeftPressed = Input.GetMouseButton(0);

        if (!isMouseLeftPressed || isAttacking == false)
                { 
                  
                  
                 
                   
        }


               
          

        else
        {
                //toggleCameraRotation = false;  // �ѷ����� ��Ȱ��ȭ
              
              
        }


            
        //}

        //if (isAttacking == true)
        //{
        //    GetComponent<PlayerMove>().enabled = false;
        //}

        //if (isAttacking == false)
        //{
        //    GetComponent<PlayerMove>().enabled = true;
        //}




    }


    private void LateUpdate()
    {
    
            Vector3 playerRotate = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);

    }

    void isJumpTrue()
    {
        isJump = true;
    }

    void isJumpFalse()
    {
        isJump = false;
    }
    void PlayerJump()
    {
       

        if (cc.isGrounded == true)
        {

            anim.SetBool("isJump", true);

            // �� ���� �������� + ����Ű�� ������ �� ����!
            if (Input.GetButtonDown("Jump"))
              {
                
               
                
                anim.SetTrigger("jump");
                
                AudioManager.instance.PlaySE(Player_Jump, 1, 1);
                _y = jumpPower;
                //Player.instance.UseStamina(20.0f);
              }

              // ���� ������ ���� �ִϸ��̼� ������ �ϱ�
              else
              {
              anim.SetBool("isJump", false);
                    
              }
            
        }

        else
        {
            anim.SetBool("isJump", false);
           
          
        }
    }

     void PlayerWalk()
     {
       

       

        // ���� Ű���� �޾ƿ��� 
        float v = Input.GetAxis("Vertical");

        // �¿� Ű���� �޾ƿ��� 
        float h = Input.GetAxis("Horizontal");

        // �̵��� ������ ������ �ִ� ���� + ����ȭ 
        Vector3 dir = new Vector3(h, 0, v).normalized;

        // �÷��̾ �ٶ󺸴� ������ �������� ����
        dir = transform.TransformDirection(dir);  // ���� ���ϱ� 

        // �̵� �ִϸ��̼� ����
        anim.SetFloat("speed", dir.magnitude);

        // y ���� ����
        dir.y = _y;


            // ĳ���� ���� (����ƮŰ ������ �޸���)
            if (Input.GetKey(KeyCode.LeftShift) && Player.instance.currentStamina > 1f)
            {
               cc.Move(dir * runspeed * Time.deltaTime); // ĳ���� ��Ʈ�ѷ��� �Լ��� �̿��ؼ� �յ��¿�� �����̴� �ڵ�   

                anim.SetBool("isRun", true); // �ٴ� �ִϸ��̼�

                Player.instance.UseStamina(0.15f); // ���¹̳� 0.25�� �Ҹ� 

            }

            else
            {
                // �ȴ� �ִϸ��̼�
                anim.SetBool("isRun", false);
                // ĳ���� ��Ʈ�ѷ��� �Լ��� �̿��ؼ� �յ��¿�� ������ �����̴� �ڵ� 
                cc.Move(dir * speed * Time.deltaTime);
            }
        

    }



    void PlayerBlock()
    {
        if (Player.instance.currentStamina > 15f)
        {

           if (Input.GetMouseButtonDown(1))
           {
              anim.SetTrigger("isBlock2");
              Player.instance.UseStamina(15f); // ���¹̳� 50 �Ҹ� 
           }

        }

    }

    void PlayerAttack_Strong()
    {

        if (Player.instance.currentStamina > 20f)
        {

            if (Input.GetMouseButtonDown(2))
            {
                anim.SetTrigger("isAttack_Strong");
                Player.instance.UseStamina(20f); // ���¹̳� 50 �Ҹ� 
            }

        }

    }


    void PlayerBlockTrue()
    {
        isBlock = true;
    }

    void PlayerBlockFalse()
    {
        isBlock = false;
    }



    void PlayerMouseMove()  // => ���׸ӽ� ������� ���� ������� ����.
        {

            // <���콺�� �����̴� ���>
            // ���콺�� �¿� ������ ����
            float rotY = Input.GetAxis("Mouse X") * Time.deltaTime * rotSpeed;

            // ���콺 �¿� �����Ӵ�� ȸ��

            transform.Rotate(0, rotY, 0);
        }

    
}


// ������ �ΰ� ���� �־���� �ڿ������� �ȴ�. (any state���� ��ȯ�Ǵϱ� Ʈ���� �߰�) 
// ��? bool�� ������ ���߿����� ����ؼ� true ���� �ݺ��ż� animator���� ����� ���δ�.
// �ӵ� ��ŸŸ�� �� ����ȭ�� ���� ������ �ؾ���!!!
// ������ ũ�⸸ ������ �ϴ°� : manitude -> ������ �̵��ؾ��ϴϱ� 
// ����ȭ (ũ�⸦ 1�� �����ؼ� ���⸸ ������) -> ��ȯ ���� ��� �ٷ� ���� ����. 
// dir = dir.normalized; // ����ȭ �ٸ� ��� -> ������ ó���ϴ� �� 
//anim.SetFloat("speed", dir.magnitude); // ũ�⸸ ������ �ϴ°�.(float �ڷ���)