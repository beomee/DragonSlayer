using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float gravityScale; // 중력 크기
    public float jumpPower;    // 점프하는 힘 
    public float rotSpeed;     // 회전 속도
    public bool toggleCameraRotation;    // art를 누르고 마우스를 움직이면 주변을 볼 수 있는 조건을 담을 변수 
    public float smoothness = 1000f;
    public bool isBlock = false;
    public bool isJump = false;

    public float Speed = 10.0f;

    public float rotateSpeed = 10.0f;       // 회전 속도

    float h, v;

    float forwardSpeed = 10.0f;
    float jumpSpeed = 10.0f;

    bool isAttacking = false;
    public bool jumpStart;

    float _y; // y축 관리용 변수 
    Player player;
    CharacterController cc;  // 캐릭터컨트롤러 담을 변수 
    Animator anim;           // 애니메이터 담을 변수
    Camera camera; // 카메라를 담을 변수 


    [SerializeField]
    private string Player_Walk; // 플레이어가 걸을 때 나오는 소리를 담을 변수 

    [SerializeField]
    private string Player_Run; // 플레이어가 걸을 때 나오는 소리를 담을 변수 

    [SerializeField]
    private string Player_Jump; // 플레이어가 걸을 때 나오는 소리를 담을 변수 

    public float speed, runspeed; // 플레이어가 걷고 달리는 속도를 제어할 변수 

    

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

        //  중력 적용
        _y -= gravityScale * Time.deltaTime;


        //// 땅위에 있지 않을때만 중력이 작용하도록 만듦.
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


        if (Input.GetMouseButton(0)) // 왼쪽 마우스를 누른 상태라면(공격 중이라면)
        {
            toggleCameraRotation = true;  // 둘러보기 활성화
        }

        else
        {
            toggleCameraRotation = false;  // 둘러보기 비활성화
        }

        bool isMouseLeftPressed = Input.GetMouseButton(0);

        if (!isMouseLeftPressed || isAttacking == false)
                { 
                  
                  
                 
                   
        }


               
          

        else
        {
                //toggleCameraRotation = false;  // 둘러보기 비활성화
              
              
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

            // 땅 위에 있을때만 + 점프키를 눌렀을 때 점프!
            if (Input.GetButtonDown("Jump"))
              {
                
               
                
                anim.SetTrigger("jump");
                
                AudioManager.instance.PlaySE(Player_Jump, 1, 1);
                _y = jumpPower;
                //Player.instance.UseStamina(20.0f);
              }

              // 땅에 닿으면 점프 애니메이션 끝나게 하기
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
       

       

        // 상하 키보드 받아오기 
        float v = Input.GetAxis("Vertical");

        // 좌우 키보드 받아오기 
        float h = Input.GetAxis("Horizontal");

        // 이동할 방향을 가지고 있는 벡터 + 정규화 
        Vector3 dir = new Vector3(h, 0, v).normalized;

        // 플레이어가 바라보는 방향을 기준으로 적용
        dir = transform.TransformDirection(dir);  // 방향 구하기 

        // 이동 애니메이션 관리
        anim.SetFloat("speed", dir.magnitude);

        // y 방향 적용
        dir.y = _y;


            // 캐릭터 무빙 (시프트키 누르면 달리기)
            if (Input.GetKey(KeyCode.LeftShift) && Player.instance.currentStamina > 1f)
            {
               cc.Move(dir * runspeed * Time.deltaTime); // 캐릭터 컨트롤러의 함수를 이용해서 앞뒤좌우로 움직이는 코드   

                anim.SetBool("isRun", true); // 뛰는 애니메이션

                Player.instance.UseStamina(0.15f); // 스태미나 0.25씩 소모 

            }

            else
            {
                // 걷는 애니메이션
                anim.SetBool("isRun", false);
                // 캐릭터 컨트롤러의 함수를 이용해서 앞뒤좌우로 실제로 움직이는 코드 
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
              Player.instance.UseStamina(15f); // 스태미나 50 소모 
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
                Player.instance.UseStamina(20f); // 스태미나 50 소모 
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



    void PlayerMouseMove()  // => 씨네머신 사용으로 인해 사용하지 않음.
        {

            // <마우스로 움직이는 기능>
            // 마우스의 좌우 움직임 저장
            float rotY = Input.GetAxis("Mouse X") * Time.deltaTime * rotSpeed;

            // 마우스 좌우 움직임대로 회전

            transform.Rotate(0, rotY, 0);
        }

    
}


// 조건을 두개 같이 넣어놔야 자연스럽게 된다. (any state에서 전환되니까 트리거 추가) 
// 왜? bool만 넣으면 공중에서는 계속해서 true 값이 반복돼서 animator에서 끊기게 보인다.
// 속도 델타타임 꼭 정규화를 해준 다음에 해야함!!!
// 벡터의 크기만 남도록 하는거 : manitude -> 방향대로 이동해야하니까 
// 정규화 (크기를 1로 고정해서 방향만 남게함) -> 반환 값이 없어서 바로 적용 가능. 
// dir = dir.normalized; // 정규화 다른 방법 -> 변수로 처리하는 법 
//anim.SetFloat("speed", dir.magnitude); // 크기만 남도록 하는것.(float 자료형)