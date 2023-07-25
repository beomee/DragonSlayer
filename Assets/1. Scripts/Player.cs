using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Cinemachine;

public class Player : MonoBehaviour
{

    float MaxDistance = 5f;
    int dir;
    float distance;
    float distance_Store_One; // 1번 상점과 플레이어와의 거리를 담는 변수 
    float distance_Store_Two; // 2번 상점과 플레이어와의 거리를 담는 변수
    float distance_Npc; // NPC와 플레이어와의 거리를 담는 변수
    public bool isConnect = false;


    CinemachineImpulseSource impulseSource;

    float _y; // y축 관리용 변수 
    public float speed;
    bool slowStart;
    bool burningStart;


    public bool isStore = false;
    public bool isNpc = false;
    public bool isGetItem = false;
    public bool isAttack;

    public GameObject slowDebuff;
    public GameObject burningDebuff;
    public GameManager gm; // 게임매니저
    public GameObject scanObject;  // npc 대화 가져오기
    public GameObject talkPanel;  // 말풍선배경
    public Transform npc; // npc 위치
    public Npc npc_; // npc 스크립트 가져오기
    public GameObject InteractionUi; // 상호작용 문구 
    public Slider hpBar;    // 플레이어의 체력바    
    //public Camera3 camera3;
    public Animator anim; // 캐릭터 리스폰할 때, 애니메이션을 idle로 하기위해 public으로 해놓음.
    public GameObject attackCollision;
    public GameObject attackCollision2;
    public GameObject attackCollision3;
    public GameObject attackCollision4;
    public Inventory inventoryUi;
    public GameSceneMng gameSceneMng;
    public Store_ storeUi;

    public GameObject shieldEffect;
    public MeleeWeaponTrail WeaponTrail;

    public GameObject storeObject1;
    public GameObject storeObject2;



    public Image bloodScreen; // 피격시 빨간 화면이 뜨는 이미지를 담을 변수 
    //CameraShake cameraShake;
    public ActionController actionController;
    CharacterController cc;
    public Cinemachine.CinemachineBrain camera3;
    public CinemachineFreeLook freelook;
    PlayerMove playerMove;
    public Slider staminaBar;

    public float maxStamina = 300f; // 최대 스태미나 값
    public float currentStamina = 10f; // 현재 스태미나 값
    Coroutine regen;

    public static Player instance;

    public Enemy_RedDragon redDragon;

    public SkinnedMeshRenderer amor;
    public SkinnedMeshRenderer body;
    public SkinnedMeshRenderer hair;



    [SerializeField]
    private string PlayerAttack_Sound; // 플레이어가 칼을 휘두르는 소리 

    [SerializeField]
    private string PlayerAttack_Sound2; // 플레이어가 칼을 휘두르는 소리 2번째

    [SerializeField]
    private string PlayerAttack_Sound3; // 플레이어가 칼을 휘두르는 소리 3번째


    [SerializeField]
    private string BossEnemy_Laugh; // 보스에게서 플레이어 피가 0이 되었을 떄, 나오는 보스의 웃음소리를 담을 변수 
    [SerializeField]
    private string PlayerDamaged; // 보스에게서 플레이어 피가 0이 되었을 떄, 나오는 보스의 웃음소리를 담을 변수 
    [SerializeField]
    private string PlayerBlock; // 방어에 성공했을때 나오는 소리
    [SerializeField]
    private string criticalSound; // 크리티컬 데미지를 입혔을 때 나는 사운드 

    [SerializeField]
    private string PlayerMoveSound; // 크리티컬 데미지를 입혔을 때 나는 사운드 

    private void Awake()
    {
       

        if (instance == null)
        {
            instance = this;

            //DontDestroyOnLoad(gameObject);
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        talkPanel.SetActive(false);
        hpBar.value = Json.instance.data.hp;
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        //camera3 = GetComponentInChildren<Camera3>();
        //cameraShake = GetComponentInChildren<CameraShake>();
        playerMove = GetComponent<PlayerMove>();
        //currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        //staminaBar.value = maxStamina;
        staminaBar.value = currentStamina;
        impulseSource = GetComponent<CinemachineImpulseSource>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = Json.instance.data.position;
        //freelook.m_YAxis.Value = 0.26f;  // 프리룩의 Yxis안에 value값을 0.26으로 변경해주는 코드 (컴포넌트에서 변경 후 저장해도 값이 0.5로 고정돼서 스크립트로 변경해줌)

        Json.instance.Load();



        anim.SetFloat("WalkSpeed", 1.0f); // 슬로우 공격에 맞았을 때 속도를 줄이기위해서, 처음 게임시작했을때 기본속도를 넣어놓는것;
        anim.SetFloat("RunSpeed", 1.0f); // 이하 동문
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basic"))
        {
            if (playerMove.isBlock == true)
            {
                anim.SetTrigger("isBlockOk"); // 막았을때 피격될 경우 막는 애니메이션 

                StartCoroutine(KnockBack_Shield());


            }

            if (playerMove.isBlock == false)
            {
                anim.SetTrigger("damaged"); // 피격 애니메이션

                StartCoroutine(KnockBack_Basic()); // 넉백됐을때, 뒤로 밀려나게 하는 코루틴 함수
                StartCoroutine(KnockBackMoveCool()); // 넉백됐을때, 1초간 조작이 안되게하는 코루틴 함수
            }
        }

       

        if (other.CompareTag("Fire"))  // 불속성 공격이 닿으면
        {
            if (playerMove.isBlock == true)
            {
                anim.SetTrigger("isBlockOk"); // 막았을때 피격될 경우 막는 애니메이션 

                StartCoroutine(KnockBack_Shield());


            }

            if (playerMove.isBlock == false)
            {
                anim.SetTrigger("damaged"); // 피격 애니메이션

                //StartCoroutine(KnockBack_Basic()); // 넉백됐을때, 뒤로 밀려나게 하는 코루틴 함수
                StartCoroutine(KnockBackMoveCool()); // 넉백됐을때, 1초간 조작이 안되게하는 코루틴 함수

                StartCoroutine(Damaged_Burn());
            }
        }

        if (other.CompareTag("Shouting"))
        {

            if (playerMove.isBlock == true)
            {
                anim.SetTrigger("isBlockOk"); // 막았을때 피격될 경우 막는 애니메이션 

                StartCoroutine(KnockBack_Shield());


            }

            if (playerMove.isBlock == false)
            {
                anim.SetTrigger("damaged"); // 피격 애니메이션

                StartCoroutine(KnockBack_Basic()); // 넉백됐을때, 뒤로 밀려나게 하는 코루틴 함수
                StartCoroutine(KnockBackMoveCool()); // 넉백됐을때, 1초간 조작이 안되게하는 코루틴 함수

                StartCoroutine(Damaged_Shouting());
            }



        }
    }

    public void MoveMap0()
    {
      transform.position = new Vector3(-235.1356f, 0.1230214f, -236.6318f);
    }

    public void MoveMap1()
    {
   
        transform.position = new Vector3(-47.41106f, -0.01990014f, -140.8867f);
    }
    public void MoveMap2()
    {
        transform.position = new Vector3(26.97785f, -0.09817976f, -169.2689f);

    }

    public void MoveMap3()
    {
        transform.position = new Vector3(67.61109f, 8.144979f, -98.27073f);
    }

    public void MoveMap4()
    {
        transform.position = new Vector3(156.1792f, 0.07790053f, -58.56399f);
    }

    public void Damaged(float damage) // 공격 받으면 호출
    {

       
        if (playerMove.isBlock == true)
        {
            //anim.SetTrigger("isBlockOk"); // 막았을때 피격될 경우 막는 애니메이션 
            AudioManager.instance.PlaySE(PlayerBlock,1,1); // 방패 피격 시 나오는 소리

            StartCoroutine(KnockBack_Shield()); 
           

        }

        if (playerMove.isBlock == false)
        {
           
            //anim.SetTrigger("damaged"); // 피격 애니메이션
            Json.instance.data.hp -= damage;      //체력 감소
            hpBar.value = Json.instance.data.hp; // 나의 체력을 Hp바에 적용 
            StartCoroutine(ShowBloodScreen()); // 피격 시 화면 빨갛게 하는 코루틴함수 호출
            AudioManager.instance.PlaySE(PlayerDamaged,Random.Range(0.95f,1.05f), Random.Range(0.85f, 1.0f)); // 플레이어 피격 시 나오는 소리
            
            CameraShakeManager.instance.CameraShake(impulseSource); // 카메라 쉐이크 // 피격시 카메라 흔들림 효과 
            //StartCoroutine(KnockBack_Basic()); // 넉백됐을때, 뒤로 밀려나게 하는 코루틴 함수
            //StartCoroutine(KnockBackMoveCool()); // 넉백됐을때, 1초간 조작이 안되게하는 코루틴 함수

            


            // 나의 hp가 0 이하 일때의 상황 -> 죽었을 때
            if (Json.instance.data.hp <= 0)
            {
              
                AudioManager.instance.PlaySE(BossEnemy_Laugh,1,1); // 보스몬스터의 웃음소리출력

                anim.SetTrigger("dead"); // 죽음 애니메이션 전환


                // 이 부분 수정 필요함 
                if (Enemy_RedDragon.instance.enemyhp > 0)
                {
                    Enemy_RedDragon.instance.enabled = false;
                }


                if (Enemy_GrayDragon.instance.enemyhp > 0)
                {
                    Enemy_GrayDragon.instance.enabled = false;
                }



                if (Enemy_BrownDragon.instance.enemyhp > 0)
                {
                    Enemy_BrownDragon.instance.enabled = false;
                }



                if (Enemy_PurpleDragon.instance.enemyhp > 0)
                {
                    Enemy_PurpleDragon.instance.enabled = false;
                }





                playerMove.enabled = false; // 플레이어 기능 중단  

                GetComponent<CharacterController>().enabled = false; // 캐릭터컨트롤러 기능 중단

                //GetComponentInChildren<Camera3>().enabled = false; // 카메라 기능 중단)
                camera3.enabled = false; // 카메라 기능 중단

                StartCoroutine(gm.RespawnPlayer()); // 플레이어 리스폰 

                 
                this.enabled = false;


            }

        }

        //else
        //{
        //    anim.SetTrigger("damaged"); // 피격 애니메이션
        //    Json.instance.data.hp -= damage;      //체력 감소
        //    hpBar.value = Json.instance.data.hp; // 나의 체력을 Hp바에 적용 
        //    StartCoroutine(ShowBloodScreen()); // 피격 시 화면 빨갛게 하는 코루틴함수 호출
        //    AudioManager.instance.PlaySE(PlayerDamaged); // 플레이어 피격 시 나오는 소리
        //}

    


       

    }


    public void Healing()  // 체력회복포션을 먹었을때 

    {

        Json.instance.data.hp += Json.instance.data.maxhp * 0.3f; //hp를 전체 체력의 30프로 회복 

        hpBar.value = Json.instance.data.hp; // 현재 hp 수치를 hp바의 value값에 넣어주는것

        Json.instance.data.hp = Mathf.Clamp(Json.instance.data.hp, 0, Json.instance.data.maxhp); // 현재 hp가 maxhp를 넘지 않도록 제한 하는 코드



    }

    public void HealingStamina() // 스태미나회복포션을 먹었을때
    {
        currentStamina += maxStamina * 0.3f; // 스태미나를 전체의 30프로 회복
        staminaBar.value = currentStamina; // 현재 스태미나 수치를 스태미나바에 적용

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina); // 현재 스태미나가 최대 스태미나를 넘지 않도록 제한 하는 코드.
    }


    public void OnweaponAttack()
    {
    
        anim.SetTrigger("onWeaponAttack"); // 공격 애니메이션 전환
        
        //StartCoroutine(MoveStop());

    }

    IEnumerator MoveStop()
    {
        playerMove.enabled = false;

        yield return new WaitForSeconds(1f);

        playerMove.enabled = true;
    }
    public void OnAttackCollision() // 애니메이션 책갈피에서 호출
    {
        attackCollision.SetActive(true);  // 공격지점 활성화
        AudioManager.instance.PlaySE(PlayerAttack_Sound,1,1); // 공격 사운드 출력

    }

    public void OnAttackCollision2() // 애니메이션 책갈피에서 호출
    {
        attackCollision2.SetActive(true);  // 공격지점 활성화
        AudioManager.instance.PlaySE(PlayerAttack_Sound2,1,1); // 공격 사운드 출력

    }
    public void OnAttackCollision3() // 애니메이션 책갈피에서 호출
    {
        attackCollision3.SetActive(true);  // 공격지점 활성화
        AudioManager.instance.PlaySE(PlayerAttack_Sound3,1,1); // 공격 사운드 출력
    
    }
    public void OnAttackCollision_Strong() // 애니메이션 책갈피에서 호출
    {
        attackCollision4.SetActive(true);  // 공격지점 활성화
        AudioManager.instance.PlaySE(PlayerAttack_Sound3, 1,1); // 공격 사운드 출력
        
    }


    private void Update()
    {
        if (isConnect == true)
        {
            distance_Npc = Vector3.Distance(transform.position, npc.position);
            distance_Store_One = Vector3.Distance(transform.position, storeObject1.transform.position);
            distance_Store_Two = Vector3.Distance(transform.position, storeObject2.transform.position);


            if (distance_Store_One < 10f)
            { 
              if (distance_Store_One < 3f)
              {
             
                isStore = true;
              }

              else
              {
                isStore = false;
              }
            }

            else if (distance_Store_Two < 10f)
            { 
              if (distance_Store_Two < 3f)
              { 
                isStore = true;
              }

              else
              {
                isStore = false;
              }
            }

            else if (distance_Npc < 10f)
            {
                if (distance_Npc < 3f)
                {
                    isNpc = true;
                }

                else
                {
                    isNpc = false;
                }
            }
        }






        // 전에거
        //distance_Npc = Vector3.Distance(transform.position, npc.position);
        //distance_Store_One = Vector3.Distance(transform.position, storeObject1.transform.position);
        //distance_Store_Two = Vector3.Distance(transform.position, storeObject2.transform.position);

        // if (distance_Store_One < 10f)
        // { 
        //     if (distance_Store_One < 3f)
        //     {
        //         isStore = true;
        //     }

        //     else
        //     {
        //         isStore = false;
        //     }

        // }

        // if (distance_Store_Two < 10f)
        // {
        //     if (distance_Store_Two < 3f)
        //     {
        //         isStore = true;
        //     }

        //     else
        //     {
        //         isStore = false;
        //     }
        // }

        // if (distance_Npc < 10f)
        // {
        //     if (distance_Npc < 3f)
        //     {
        //         isNpc = true;
        //     }

        //     else
        //     {
        //         isNpc = false;
        //     }
        // }








        StartRegenStamina(); // 스태미나가 리젠되는 함수

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(2))  // 달리기나 마우스왼쪽이나 스페이스를 누를 경우에
        {
            StopRegenStanima();  // 스태미나 리젠 중지
        }


        //if (!Inventory.inventoryActivated && !GameSceneMng.settingActivated && !GameManager.fullCameraActivated)//&& currentStamina > 0.15f && playerMove.jumpStart == false*//*&& cc.isGrounded*/ ) // 인벤토리와 설정창이 열리지 않았고, 스태미나가 0 초과일떄만 
        //{
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    OnAttackCollision();
        //}

        if (inventoryUi.InvenUiOpen == false/* && gm.isWorldMapOpen == false*/ && gameSceneMng.isSettingUi == false && storeUi.isShopOpenUi == false && npc_.isNpcUi == false && Json.instance.data.hp > 0.0f )
        {

                // 마우스 왼쪽 클릭 시 공격하기 
                if (Input.GetMouseButtonDown(0))
                {
                    OnweaponAttack();  // 공격 애니메이션 실행 하는 코드    
                }

        }

        //else if (Input.GetMouseButtonUp(0))
        //{
        //    anim.SetBool("isRun", false);
        //    anim.SetFloat("speed", 0);


        //}
        //}



        /**
만약에 플레이어가 움직이지 않는 상태 일 경우에는 스태미나가 리젠됨. 
=> 공격 시에는 스태미나가 회복되는 오류가 있을 거야. 
=> 공격 할 때 스태미나가 안차려면, 공격 시작 할 때 상태를 true로, 끝나면 false로 바꿔주고
=> 움직이지않고 + 상태가 false일때만 스태미나가 리젠되게 하면 어떨까.
 
 */



    }




    public void UseStamina(float amount) // 스태미나 사용 함수
    {
        if (currentStamina - amount >= 0)  // 현재 스태미나가 소모치 보다 크거나 같을 경우에
        {
            currentStamina -= amount;  // 현재 스태미나에서 소모치를 뺀 값을 넣어주고.
            staminaBar.value = currentStamina;  // 현재 스태미나를 스태미나바의 값에 넣어준다.

            if (regen != null)  // 리젠이 null 상태가 아니라면 => 스태미나가 가득 차있다면 
            {
                StopCoroutine(RegenStamina());   // 리젠하는것을 멈춰라.
            }

        }

        else
        {
            print("스태미나가 부족합니다. ");
        }
    }

    public void StartRegenStamina() // 스태미나 리젠 시작
    {
        if (regen == null)
        {
            regen = StartCoroutine(RegenStamina());
        }
    }

    public void StopRegenStanima() // 스태미나 리젠 중지
    {
        if (regen != null)
        {
            StopCoroutine(regen);
            regen = null;
        }
    }

    public IEnumerator RegenStamina() // 스태미나 리젠(실질적 기능)
    {

       yield return new WaitForSeconds(2f);   // 2초뒤에 

        while (currentStamina < maxStamina)  // 현재 스태미나가 최대 스태미나 보다 작을 경우
        {
        currentStamina += 1f;   // 현재 스태미나를 10만큼 계속 반복해서 상승함.
        staminaBar.value = currentStamina;    // 스태미나바의 value를 바꿔줌 

        yield return new WaitForSeconds(0.1f);   // 2초마다 계속 반복해서 상승

      }
        
      regen = null;  // 리젠을 할때는 null 상태로 바뀜.
       
    }

     

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {


        if (hit.gameObject.tag == "Item")
        {
            actionController.ItemInfoAppear();

        }
    }




    IEnumerator ShowBloodScreen() // 피격 시 빨간색 이미지를 잠깐 보여줄 코루틴 함수 정의.
    {
        bloodScreen.color = new Color(1, 0, 0, 0.4f);  // 색깔은 기본색으로 빨간색, 투명도는 0.4의 값으로 출력
        yield return new WaitForSeconds(0.4f); //0.4초간 보인 후 다시 이미지를 투명하게 만듦.
        bloodScreen.color = Color.clear; //다시 이미지 투명하도록 함.
    }

    IEnumerator ShowBlockScreen() // 피격 시 빨간색 이미지를 잠깐 보여줄 코루틴 함수 정의.
    {
        bloodScreen.color = new Color(0, 1, 1, 0.4f);  // 색깔은 기본색으로 빨간색, 투명도는 0.4의 값으로 출력
        yield return new WaitForSeconds(0.4f); //0.4초간 보인 후 다시 이미지를 투명하게 만듦.
        bloodScreen.color = Color.clear; //다시 이미지 투명하도록 함.
    }

    void TrailViewFalse()
    {
        WeaponTrail._use = false;
    }

    void TrailViewTrue()
    {
        WeaponTrail._use = true;
    }

    IEnumerator KnockBackMoveCool()
    {
        yield return new WaitForSeconds(0.1f);

        GetComponent<Player>().enabled = false;
        GetComponent<PlayerMove>().enabled = false;

        yield return new WaitForSeconds(1.0f);
        GetComponent<Player>().enabled = true;
        GetComponent<PlayerMove>().enabled = true;

    }

    IEnumerator KnockBack_Basic() // 기본 피격 넉백 거리 
    {
       
        float knockBackSpeed_Basic = 100f; // 기본 피격 넉백이 되는 스피드

        while (knockBackSpeed_Basic > 1.1f) // 뒤로 밀리는데, 느려지는 구간에서 끊어지도록
        {
            Vector3 moveDirection = -transform.forward.normalized;
            cc.Move(moveDirection * knockBackSpeed_Basic * Time.deltaTime);
            moveDirection.y = transform.position.y;
            knockBackSpeed_Basic = Mathf.Sqrt(knockBackSpeed_Basic);
            yield return null; // 한 프레임씩 적용

        }

        //knockBackSpeed_Basic = 100f;
    }

    IEnumerator KnockBack_Shield() // 기본 피격 넉백 거리 
    {

        shieldEffect.SetActive(true);

        float knockBackSpeed_Shield = 30f; // 기본 피격 넉백이 되는 스피드

        while (knockBackSpeed_Shield > 1.1f) // 뒤로 밀리는데, 느려지는 구간에서 끊어지도록
        {
            Vector3 moveDirection = -transform.forward.normalized;
            cc.Move(moveDirection * knockBackSpeed_Shield * Time.deltaTime);
            moveDirection.y = transform.position.y;
            knockBackSpeed_Shield = Mathf.Sqrt(knockBackSpeed_Shield);
            yield return null; // 한 프레임씩 적용

        }

        yield return new WaitForSeconds(1f);
        shieldEffect.SetActive(false);
    }


    IEnumerator Damaged_Burn()
    {
        StartCoroutine(Damaged_Burn_Color());  // 몸 색깔이 바뀌는 코드 

        burningDebuff.SetActive(true);

        yield return new WaitForSeconds(1f);
        Json.instance.data.hp -= redDragon.enemystr / 10.0f;      //체력 감소
        hpBar.value = Json.instance.data.hp; // 나의 체력을 Hp바에 적용 
        //AudioManager.instance.PlaySE(PlayerDamaged,Random.Range(0.8f,1.2f)); // 플레이어 피격 시 나오는 소리
        StartCoroutine(ShowBloodScreen()); // 피격 시 화면 빨갛게 하는 코루틴함수 호출
        yield return new WaitForSeconds(1f);

        Json.instance.data.hp -= redDragon.enemystr / 10.0f;      //체력 감소
        hpBar.value = Json.instance.data.hp; // 나의 체력을 Hp바에 적용 
        //AudioManager.instance.PlaySE(PlayerDamaged, Random.Range(0.8f, 1.2f)); // 플레이어 피격 시 나오는 소리
        StartCoroutine(ShowBloodScreen()); // 피격 시 화면 빨갛게 하는 코루틴함수 호출
        yield return new WaitForSeconds(1f);

        Json.instance.data.hp -= redDragon.enemystr / 10.0f;      //체력 감소
        hpBar.value = Json.instance.data.hp; // 나의 체력을 Hp바에 적용 
        //AudioManager.instance.PlaySE(PlayerDamaged, Random.Range(0.8f, 1.2f)); // 플레이어 피격 시 나오는 소리
        StartCoroutine(ShowBloodScreen()); // 피격 시 화면 빨갛게 하는 코루틴함수 호출
        yield return new WaitForSeconds(0.5f);

        burningDebuff.SetActive(false);

    }

    IEnumerator Damaged_Burn_Color()
    {
        amor.material.color = new Vector4(0.7921f, 0.3411f, 0.3411f, 1);
        body.materials[0].color = new Vector4(0.7921f, 0.3411f, 0.3411f, 1);
        hair.material.color = new Vector4(0.7921f, 0.3411f, 0.3411f, 1);

        yield return new WaitForSeconds(4.3f);

        amor.material.color = new Vector4(1f, 1f, 1f, 1);
        body.materials[0].color = new Vector4(1f, 1f, 1f, 1);
        hair.material.color = new Vector4(1f, 1f, 1f, 1); 
    }

    IEnumerator Damaged_Shouting()
    {
        GetComponent<PlayerMove>().speed = 2.0f;
        GetComponent<PlayerMove>().runspeed = 5.0f;
        anim.SetFloat("WalkSpeed", 0.5f);
        anim.SetFloat("RunSpeed", 0.5f);
        slowDebuff.SetActive(true);
        slowStart = true;
        yield return new WaitForSeconds(3f);

        slowDebuff.SetActive(false);
        GetComponent<PlayerMove>().speed = 5.0f;
        GetComponent<PlayerMove>().runspeed = 10.0f;
        anim.SetFloat("WalkSpeed", 1.0f);
        anim.SetFloat("RunSpeed", 1.0f);
        slowStart = false;
    }


    void PlayerMoveSoundStart()
    {
        AudioManager.instance.PlaySE(PlayerMoveSound, 1, 0.2f);
    }

    //IEnumerator KnockBack_Powerful() // 기본 피격 넉백 거리 
    //{

    //    float knockBackSpeed_Powerful = 100f; // 기본 피격 넉백이 되는 스피드

    //    while (knockBackSpeed_Powerful > 1.0f) // 뒤로 밀리는데, 느려지는 구간에서 끊어지도록
    //    {
    //        Debug.Log(knockBackSpeed_Powerful);
    //        Vector3 moveDirection = -transform.forward.normalized;
    //        cc.Move(moveDirection * knockBackSpeed_Powerful * Time.deltaTime);
    //        moveDirection.y = transform.position.y;
    //        knockBackSpeed_Powerful = Mathf.Sqrt(knockBackSpeed_Powerful) ;
    //        yield return null; // 한 프레임씩 적용

    //    }


    //}
}





















