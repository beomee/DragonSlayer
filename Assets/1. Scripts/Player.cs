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
    float distance_Store_One; // 1�� ������ �÷��̾���� �Ÿ��� ��� ���� 
    float distance_Store_Two; // 2�� ������ �÷��̾���� �Ÿ��� ��� ����
    float distance_Npc; // NPC�� �÷��̾���� �Ÿ��� ��� ����
    public bool isConnect = false;


    CinemachineImpulseSource impulseSource;

    float _y; // y�� ������ ���� 
    public float speed;
    bool slowStart;
    bool burningStart;


    public bool isStore = false;
    public bool isNpc = false;
    public bool isGetItem = false;
    public bool isAttack;

    public GameObject slowDebuff;
    public GameObject burningDebuff;
    public GameManager gm; // ���ӸŴ���
    public GameObject scanObject;  // npc ��ȭ ��������
    public GameObject talkPanel;  // ��ǳ�����
    public Transform npc; // npc ��ġ
    public Npc npc_; // npc ��ũ��Ʈ ��������
    public GameObject InteractionUi; // ��ȣ�ۿ� ���� 
    public Slider hpBar;    // �÷��̾��� ü�¹�    
    //public Camera3 camera3;
    public Animator anim; // ĳ���� �������� ��, �ִϸ��̼��� idle�� �ϱ����� public���� �س���.
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



    public Image bloodScreen; // �ǰݽ� ���� ȭ���� �ߴ� �̹����� ���� ���� 
    //CameraShake cameraShake;
    public ActionController actionController;
    CharacterController cc;
    public Cinemachine.CinemachineBrain camera3;
    public CinemachineFreeLook freelook;
    PlayerMove playerMove;
    public Slider staminaBar;

    public float maxStamina = 300f; // �ִ� ���¹̳� ��
    public float currentStamina = 10f; // ���� ���¹̳� ��
    Coroutine regen;

    public static Player instance;

    public Enemy_RedDragon redDragon;

    public SkinnedMeshRenderer amor;
    public SkinnedMeshRenderer body;
    public SkinnedMeshRenderer hair;



    [SerializeField]
    private string PlayerAttack_Sound; // �÷��̾ Į�� �ֵθ��� �Ҹ� 

    [SerializeField]
    private string PlayerAttack_Sound2; // �÷��̾ Į�� �ֵθ��� �Ҹ� 2��°

    [SerializeField]
    private string PlayerAttack_Sound3; // �÷��̾ Į�� �ֵθ��� �Ҹ� 3��°


    [SerializeField]
    private string BossEnemy_Laugh; // �������Լ� �÷��̾� �ǰ� 0�� �Ǿ��� ��, ������ ������ �����Ҹ��� ���� ���� 
    [SerializeField]
    private string PlayerDamaged; // �������Լ� �÷��̾� �ǰ� 0�� �Ǿ��� ��, ������ ������ �����Ҹ��� ���� ���� 
    [SerializeField]
    private string PlayerBlock; // �� ���������� ������ �Ҹ�
    [SerializeField]
    private string criticalSound; // ũ��Ƽ�� �������� ������ �� ���� ���� 

    [SerializeField]
    private string PlayerMoveSound; // ũ��Ƽ�� �������� ������ �� ���� ���� 

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
        //freelook.m_YAxis.Value = 0.26f;  // �������� Yxis�ȿ� value���� 0.26���� �������ִ� �ڵ� (������Ʈ���� ���� �� �����ص� ���� 0.5�� �����ż� ��ũ��Ʈ�� ��������)

        Json.instance.Load();



        anim.SetFloat("WalkSpeed", 1.0f); // ���ο� ���ݿ� �¾��� �� �ӵ��� ���̱����ؼ�, ó�� ���ӽ��������� �⺻�ӵ��� �־���°�;
        anim.SetFloat("RunSpeed", 1.0f); // ���� ����
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basic"))
        {
            if (playerMove.isBlock == true)
            {
                anim.SetTrigger("isBlockOk"); // �������� �ǰݵ� ��� ���� �ִϸ��̼� 

                StartCoroutine(KnockBack_Shield());


            }

            if (playerMove.isBlock == false)
            {
                anim.SetTrigger("damaged"); // �ǰ� �ִϸ��̼�

                StartCoroutine(KnockBack_Basic()); // �˹������, �ڷ� �з����� �ϴ� �ڷ�ƾ �Լ�
                StartCoroutine(KnockBackMoveCool()); // �˹������, 1�ʰ� ������ �ȵǰ��ϴ� �ڷ�ƾ �Լ�
            }
        }

       

        if (other.CompareTag("Fire"))  // �ҼӼ� ������ ������
        {
            if (playerMove.isBlock == true)
            {
                anim.SetTrigger("isBlockOk"); // �������� �ǰݵ� ��� ���� �ִϸ��̼� 

                StartCoroutine(KnockBack_Shield());


            }

            if (playerMove.isBlock == false)
            {
                anim.SetTrigger("damaged"); // �ǰ� �ִϸ��̼�

                //StartCoroutine(KnockBack_Basic()); // �˹������, �ڷ� �з����� �ϴ� �ڷ�ƾ �Լ�
                StartCoroutine(KnockBackMoveCool()); // �˹������, 1�ʰ� ������ �ȵǰ��ϴ� �ڷ�ƾ �Լ�

                StartCoroutine(Damaged_Burn());
            }
        }

        if (other.CompareTag("Shouting"))
        {

            if (playerMove.isBlock == true)
            {
                anim.SetTrigger("isBlockOk"); // �������� �ǰݵ� ��� ���� �ִϸ��̼� 

                StartCoroutine(KnockBack_Shield());


            }

            if (playerMove.isBlock == false)
            {
                anim.SetTrigger("damaged"); // �ǰ� �ִϸ��̼�

                StartCoroutine(KnockBack_Basic()); // �˹������, �ڷ� �з����� �ϴ� �ڷ�ƾ �Լ�
                StartCoroutine(KnockBackMoveCool()); // �˹������, 1�ʰ� ������ �ȵǰ��ϴ� �ڷ�ƾ �Լ�

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

    public void Damaged(float damage) // ���� ������ ȣ��
    {

       
        if (playerMove.isBlock == true)
        {
            //anim.SetTrigger("isBlockOk"); // �������� �ǰݵ� ��� ���� �ִϸ��̼� 
            AudioManager.instance.PlaySE(PlayerBlock,1,1); // ���� �ǰ� �� ������ �Ҹ�

            StartCoroutine(KnockBack_Shield()); 
           

        }

        if (playerMove.isBlock == false)
        {
           
            //anim.SetTrigger("damaged"); // �ǰ� �ִϸ��̼�
            Json.instance.data.hp -= damage;      //ü�� ����
            hpBar.value = Json.instance.data.hp; // ���� ü���� Hp�ٿ� ���� 
            StartCoroutine(ShowBloodScreen()); // �ǰ� �� ȭ�� ������ �ϴ� �ڷ�ƾ�Լ� ȣ��
            AudioManager.instance.PlaySE(PlayerDamaged,Random.Range(0.95f,1.05f), Random.Range(0.85f, 1.0f)); // �÷��̾� �ǰ� �� ������ �Ҹ�
            
            CameraShakeManager.instance.CameraShake(impulseSource); // ī�޶� ����ũ // �ǰݽ� ī�޶� ��鸲 ȿ�� 
            //StartCoroutine(KnockBack_Basic()); // �˹������, �ڷ� �з����� �ϴ� �ڷ�ƾ �Լ�
            //StartCoroutine(KnockBackMoveCool()); // �˹������, 1�ʰ� ������ �ȵǰ��ϴ� �ڷ�ƾ �Լ�

            


            // ���� hp�� 0 ���� �϶��� ��Ȳ -> �׾��� ��
            if (Json.instance.data.hp <= 0)
            {
              
                AudioManager.instance.PlaySE(BossEnemy_Laugh,1,1); // ���������� �����Ҹ����

                anim.SetTrigger("dead"); // ���� �ִϸ��̼� ��ȯ


                // �� �κ� ���� �ʿ��� 
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





                playerMove.enabled = false; // �÷��̾� ��� �ߴ�  

                GetComponent<CharacterController>().enabled = false; // ĳ������Ʈ�ѷ� ��� �ߴ�

                //GetComponentInChildren<Camera3>().enabled = false; // ī�޶� ��� �ߴ�)
                camera3.enabled = false; // ī�޶� ��� �ߴ�

                StartCoroutine(gm.RespawnPlayer()); // �÷��̾� ������ 

                 
                this.enabled = false;


            }

        }

        //else
        //{
        //    anim.SetTrigger("damaged"); // �ǰ� �ִϸ��̼�
        //    Json.instance.data.hp -= damage;      //ü�� ����
        //    hpBar.value = Json.instance.data.hp; // ���� ü���� Hp�ٿ� ���� 
        //    StartCoroutine(ShowBloodScreen()); // �ǰ� �� ȭ�� ������ �ϴ� �ڷ�ƾ�Լ� ȣ��
        //    AudioManager.instance.PlaySE(PlayerDamaged); // �÷��̾� �ǰ� �� ������ �Ҹ�
        //}

    


       

    }


    public void Healing()  // ü��ȸ�������� �Ծ����� 

    {

        Json.instance.data.hp += Json.instance.data.maxhp * 0.3f; //hp�� ��ü ü���� 30���� ȸ�� 

        hpBar.value = Json.instance.data.hp; // ���� hp ��ġ�� hp���� value���� �־��ִ°�

        Json.instance.data.hp = Mathf.Clamp(Json.instance.data.hp, 0, Json.instance.data.maxhp); // ���� hp�� maxhp�� ���� �ʵ��� ���� �ϴ� �ڵ�



    }

    public void HealingStamina() // ���¹̳�ȸ�������� �Ծ�����
    {
        currentStamina += maxStamina * 0.3f; // ���¹̳��� ��ü�� 30���� ȸ��
        staminaBar.value = currentStamina; // ���� ���¹̳� ��ġ�� ���¹̳��ٿ� ����

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina); // ���� ���¹̳��� �ִ� ���¹̳��� ���� �ʵ��� ���� �ϴ� �ڵ�.
    }


    public void OnweaponAttack()
    {
    
        anim.SetTrigger("onWeaponAttack"); // ���� �ִϸ��̼� ��ȯ
        
        //StartCoroutine(MoveStop());

    }

    IEnumerator MoveStop()
    {
        playerMove.enabled = false;

        yield return new WaitForSeconds(1f);

        playerMove.enabled = true;
    }
    public void OnAttackCollision() // �ִϸ��̼� å���ǿ��� ȣ��
    {
        attackCollision.SetActive(true);  // �������� Ȱ��ȭ
        AudioManager.instance.PlaySE(PlayerAttack_Sound,1,1); // ���� ���� ���

    }

    public void OnAttackCollision2() // �ִϸ��̼� å���ǿ��� ȣ��
    {
        attackCollision2.SetActive(true);  // �������� Ȱ��ȭ
        AudioManager.instance.PlaySE(PlayerAttack_Sound2,1,1); // ���� ���� ���

    }
    public void OnAttackCollision3() // �ִϸ��̼� å���ǿ��� ȣ��
    {
        attackCollision3.SetActive(true);  // �������� Ȱ��ȭ
        AudioManager.instance.PlaySE(PlayerAttack_Sound3,1,1); // ���� ���� ���
    
    }
    public void OnAttackCollision_Strong() // �ִϸ��̼� å���ǿ��� ȣ��
    {
        attackCollision4.SetActive(true);  // �������� Ȱ��ȭ
        AudioManager.instance.PlaySE(PlayerAttack_Sound3, 1,1); // ���� ���� ���
        
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






        // ������
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








        StartRegenStamina(); // ���¹̳��� �����Ǵ� �Լ�

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(2))  // �޸��⳪ ���콺�����̳� �����̽��� ���� ��쿡
        {
            StopRegenStanima();  // ���¹̳� ���� ����
        }


        //if (!Inventory.inventoryActivated && !GameSceneMng.settingActivated && !GameManager.fullCameraActivated)//&& currentStamina > 0.15f && playerMove.jumpStart == false*//*&& cc.isGrounded*/ ) // �κ��丮�� ����â�� ������ �ʾҰ�, ���¹̳��� 0 �ʰ��ϋ��� 
        //{
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    OnAttackCollision();
        //}

        if (inventoryUi.InvenUiOpen == false/* && gm.isWorldMapOpen == false*/ && gameSceneMng.isSettingUi == false && storeUi.isShopOpenUi == false && npc_.isNpcUi == false && Json.instance.data.hp > 0.0f )
        {

                // ���콺 ���� Ŭ�� �� �����ϱ� 
                if (Input.GetMouseButtonDown(0))
                {
                    OnweaponAttack();  // ���� �ִϸ��̼� ���� �ϴ� �ڵ�    
                }

        }

        //else if (Input.GetMouseButtonUp(0))
        //{
        //    anim.SetBool("isRun", false);
        //    anim.SetFloat("speed", 0);


        //}
        //}



        /**
���࿡ �÷��̾ �������� �ʴ� ���� �� ��쿡�� ���¹̳��� ������. 
=> ���� �ÿ��� ���¹̳��� ȸ���Ǵ� ������ ���� �ž�. 
=> ���� �� �� ���¹̳��� ��������, ���� ���� �� �� ���¸� true��, ������ false�� �ٲ��ְ�
=> ���������ʰ� + ���°� false�϶��� ���¹̳��� �����ǰ� �ϸ� ���.
 
 */



    }




    public void UseStamina(float amount) // ���¹̳� ��� �Լ�
    {
        if (currentStamina - amount >= 0)  // ���� ���¹̳��� �Ҹ�ġ ���� ũ�ų� ���� ��쿡
        {
            currentStamina -= amount;  // ���� ���¹̳����� �Ҹ�ġ�� �� ���� �־��ְ�.
            staminaBar.value = currentStamina;  // ���� ���¹̳��� ���¹̳����� ���� �־��ش�.

            if (regen != null)  // ������ null ���°� �ƴ϶�� => ���¹̳��� ���� ���ִٸ� 
            {
                StopCoroutine(RegenStamina());   // �����ϴ°��� �����.
            }

        }

        else
        {
            print("���¹̳��� �����մϴ�. ");
        }
    }

    public void StartRegenStamina() // ���¹̳� ���� ����
    {
        if (regen == null)
        {
            regen = StartCoroutine(RegenStamina());
        }
    }

    public void StopRegenStanima() // ���¹̳� ���� ����
    {
        if (regen != null)
        {
            StopCoroutine(regen);
            regen = null;
        }
    }

    public IEnumerator RegenStamina() // ���¹̳� ����(������ ���)
    {

       yield return new WaitForSeconds(2f);   // 2�ʵڿ� 

        while (currentStamina < maxStamina)  // ���� ���¹̳��� �ִ� ���¹̳� ���� ���� ���
        {
        currentStamina += 1f;   // ���� ���¹̳��� 10��ŭ ��� �ݺ��ؼ� �����.
        staminaBar.value = currentStamina;    // ���¹̳����� value�� �ٲ��� 

        yield return new WaitForSeconds(0.1f);   // 2�ʸ��� ��� �ݺ��ؼ� ���

      }
        
      regen = null;  // ������ �Ҷ��� null ���·� �ٲ�.
       
    }

     

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {


        if (hit.gameObject.tag == "Item")
        {
            actionController.ItemInfoAppear();

        }
    }




    IEnumerator ShowBloodScreen() // �ǰ� �� ������ �̹����� ��� ������ �ڷ�ƾ �Լ� ����.
    {
        bloodScreen.color = new Color(1, 0, 0, 0.4f);  // ������ �⺻������ ������, ������ 0.4�� ������ ���
        yield return new WaitForSeconds(0.4f); //0.4�ʰ� ���� �� �ٽ� �̹����� �����ϰ� ����.
        bloodScreen.color = Color.clear; //�ٽ� �̹��� �����ϵ��� ��.
    }

    IEnumerator ShowBlockScreen() // �ǰ� �� ������ �̹����� ��� ������ �ڷ�ƾ �Լ� ����.
    {
        bloodScreen.color = new Color(0, 1, 1, 0.4f);  // ������ �⺻������ ������, ������ 0.4�� ������ ���
        yield return new WaitForSeconds(0.4f); //0.4�ʰ� ���� �� �ٽ� �̹����� �����ϰ� ����.
        bloodScreen.color = Color.clear; //�ٽ� �̹��� �����ϵ��� ��.
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

    IEnumerator KnockBack_Basic() // �⺻ �ǰ� �˹� �Ÿ� 
    {
       
        float knockBackSpeed_Basic = 100f; // �⺻ �ǰ� �˹��� �Ǵ� ���ǵ�

        while (knockBackSpeed_Basic > 1.1f) // �ڷ� �и��µ�, �������� �������� ����������
        {
            Vector3 moveDirection = -transform.forward.normalized;
            cc.Move(moveDirection * knockBackSpeed_Basic * Time.deltaTime);
            moveDirection.y = transform.position.y;
            knockBackSpeed_Basic = Mathf.Sqrt(knockBackSpeed_Basic);
            yield return null; // �� �����Ӿ� ����

        }

        //knockBackSpeed_Basic = 100f;
    }

    IEnumerator KnockBack_Shield() // �⺻ �ǰ� �˹� �Ÿ� 
    {

        shieldEffect.SetActive(true);

        float knockBackSpeed_Shield = 30f; // �⺻ �ǰ� �˹��� �Ǵ� ���ǵ�

        while (knockBackSpeed_Shield > 1.1f) // �ڷ� �и��µ�, �������� �������� ����������
        {
            Vector3 moveDirection = -transform.forward.normalized;
            cc.Move(moveDirection * knockBackSpeed_Shield * Time.deltaTime);
            moveDirection.y = transform.position.y;
            knockBackSpeed_Shield = Mathf.Sqrt(knockBackSpeed_Shield);
            yield return null; // �� �����Ӿ� ����

        }

        yield return new WaitForSeconds(1f);
        shieldEffect.SetActive(false);
    }


    IEnumerator Damaged_Burn()
    {
        StartCoroutine(Damaged_Burn_Color());  // �� ������ �ٲ�� �ڵ� 

        burningDebuff.SetActive(true);

        yield return new WaitForSeconds(1f);
        Json.instance.data.hp -= redDragon.enemystr / 10.0f;      //ü�� ����
        hpBar.value = Json.instance.data.hp; // ���� ü���� Hp�ٿ� ���� 
        //AudioManager.instance.PlaySE(PlayerDamaged,Random.Range(0.8f,1.2f)); // �÷��̾� �ǰ� �� ������ �Ҹ�
        StartCoroutine(ShowBloodScreen()); // �ǰ� �� ȭ�� ������ �ϴ� �ڷ�ƾ�Լ� ȣ��
        yield return new WaitForSeconds(1f);

        Json.instance.data.hp -= redDragon.enemystr / 10.0f;      //ü�� ����
        hpBar.value = Json.instance.data.hp; // ���� ü���� Hp�ٿ� ���� 
        //AudioManager.instance.PlaySE(PlayerDamaged, Random.Range(0.8f, 1.2f)); // �÷��̾� �ǰ� �� ������ �Ҹ�
        StartCoroutine(ShowBloodScreen()); // �ǰ� �� ȭ�� ������ �ϴ� �ڷ�ƾ�Լ� ȣ��
        yield return new WaitForSeconds(1f);

        Json.instance.data.hp -= redDragon.enemystr / 10.0f;      //ü�� ����
        hpBar.value = Json.instance.data.hp; // ���� ü���� Hp�ٿ� ���� 
        //AudioManager.instance.PlaySE(PlayerDamaged, Random.Range(0.8f, 1.2f)); // �÷��̾� �ǰ� �� ������ �Ҹ�
        StartCoroutine(ShowBloodScreen()); // �ǰ� �� ȭ�� ������ �ϴ� �ڷ�ƾ�Լ� ȣ��
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

    //IEnumerator KnockBack_Powerful() // �⺻ �ǰ� �˹� �Ÿ� 
    //{

    //    float knockBackSpeed_Powerful = 100f; // �⺻ �ǰ� �˹��� �Ǵ� ���ǵ�

    //    while (knockBackSpeed_Powerful > 1.0f) // �ڷ� �и��µ�, �������� �������� ����������
    //    {
    //        Debug.Log(knockBackSpeed_Powerful);
    //        Vector3 moveDirection = -transform.forward.normalized;
    //        cc.Move(moveDirection * knockBackSpeed_Powerful * Time.deltaTime);
    //        moveDirection.y = transform.position.y;
    //        knockBackSpeed_Powerful = Mathf.Sqrt(knockBackSpeed_Powerful) ;
    //        yield return null; // �� �����Ӿ� ����

    //    }


    //}
}





















