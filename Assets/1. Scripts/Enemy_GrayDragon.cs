using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

class BiteSkill_Gray : Skill
{


    public BiteSkill_Gray(float coolTime, int priority, Animator anim) : base(coolTime, priority, anim) // 생성자 메소드
    {
        // 위 변수 외에 추가 할 내용이 있으면 여기에 추가 
    }

    public override void Attack() // 실제
    {
        // 공격 애니메이션
        anim.SetTrigger("attack");

    }
}

class ShoutingAttackSkill_Gray: Skill
{

    public ShoutingAttackSkill_Gray(float coolTime, int priority, Animator anim) : base(coolTime, priority, anim) // 생성자 메소드
    {
        // 위 변수 외에 추가 할 내용이 있으면 여기에 추가 
    }

    public override void Attack() // 실제
    {
        anim.SetTrigger("attack2");
    }

}

class TailAttackSkill_Gray : Skill
{


    public TailAttackSkill_Gray(float coolTime, int priority, Animator anim) : base(coolTime, priority, anim) // 생성자 메소드
    {
        // 위 변수 외에 추가 할 내용이 있으면 여기에 추가 
    }

    public override void Attack() // 실제
    {
        anim.SetTrigger("attack3");
    }
}




public class Enemy_GrayDragon : MonoBehaviour
{
    public float distance; // 플레이어와의 거리
    public float enemyhp; // 적의 체력
    public float enemystr;


    //public GameObject effect_MonsterHit_Critical;
    //public GameObject effect_MonsterHit;


    public GameObject attackCollision_GrayEnemy; // 깨물기 공격 콜라이더를 담을 변수 
    public GameObject attackCollision_GrayEnemy_Shouting; // 샤우팅 공격콜라이더를 담을 변수
    public GameObject attackCollision_GrayEnemy_Tail; // 꼬리치기 공격콜라이더를 담을 변수 
    public GameObject[] attackCollision_GrayEnemy_Shouting_Particles; // 샤우팅 파티클을 담을 변수 

    public Collider bossStart;

    float speed = 3f; // 몬스터가 회전하는 속도를 컨트롤하는 변수 
    public Rigidbody rigid;
    Animator anim;
    public NavMeshAgent agent;
    public Slider EnemySlider;


    bool isAttacking = false; // 공격 중일때 회전을 컨트롤 할 수 있게끔 하기 위한 변수 / false = 움직일 수 있는 상태
    bool isJump = false;
    bool isAttack = false;
    bool isAttackStart = false;
    bool startFightBgm = false;
    bool BackOriginBgm = false;

    AudioManager audioManager;
    SkinnedMeshRenderer mesh;

    public  CapsuleCollider capCollider; // 몬스터 무적판정을 가져오기위해 몬스터의 콜라이더를 담는 변수 

    // 외부에 있는 플레이어 트랜스폼 가져오기 
    public Transform player; // 플레이어 가져오기(플레이어의 위치값을 가져와야 하니까)

    bool pazeTwoStart = false;

    [SerializeField]
    public GameObject goldPref; // 골드 아이템 담을 변수
    [SerializeField]
    public GameObject hpHealingPotionPref; // 체력회복포션 아이템 담을 변수

    [SerializeField]
    public GameObject criticalPref; // 크리티컬 증가 아이템을  담을 변수

    [SerializeField]
    public GameObject maxHpPotionPref; // 체력증가포션 포션 아이템 담을 변수

    [SerializeField]
    public GameObject staminaPotion; // 스태미나 회복 포션 아이템 담을 변수

    // 필요한 사운드 이름
    [SerializeField]
    private string damaged_Sound;

    [SerializeField]
    private string Dead_Sound;

    [SerializeField]
    private string grayEnemyAttack1_Sound_Bite; // 깨물기

    [SerializeField]
    private string grayEnemyAttack2_Sound_Shouting; // 샤우팅

    [SerializeField]
    private string grayEnemyAttack3_Sound_Tail; // 꼬리치기 소리 

    [SerializeField]
    private string PhazeChangeSound_Gray;  // 페이즈 변환 소리 

    [SerializeField]
    private string DropItemSound; // 아이템 드랍 

    [SerializeField]
    private string GrayEnemyWalk_Sound; 


    // 적 상태를 나타내는 자료형 (열거형) 
    public enum EnemyStete
    {
        Idle,        // 기본
        Walk,        // 이동
        Attack,      // 공격
        Dead         // 죽음
    }

    // 상태 변수 선언 + 첫 상태는 기본 
    public EnemyStete eState = EnemyStete.Idle;

    public static Enemy_GrayDragon instance;

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
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        eState = EnemyStete.Idle; // 기본상태 Idle 

        BiteSkill_Gray biteSkill_Gray = new BiteSkill_Gray(1f, 3, anim);
        ShoutingAttackSkill_Gray shoutingAttackSkill_Gray = new ShoutingAttackSkill_Gray(10.0f, 2, anim);

        skillList_Gray.Add(biteSkill_Gray as Skill);  // Skill에 상속되어있는 biteSkill를 skillList에 저장! => (Skill)biteSkill의 모양으로도 쓸 수 있음.  
        skillList_Gray.Add(shoutingAttackSkill_Gray as Skill);



        //리스폰할때 canvas 가져오기 
        #region
        //// 활성화 되어있는 Canvas를 가져오기. 
        //hpBarSliderCanvas = FindObjectOfType<Canvas>();

        /** 아래 코드에서 오류나는 이유
            게임이 시작될때 바로 위 코드에서 CANVAS를 가져오는데, 
            다른 CANVAS가 켜져있는경우(상점 등) 거기에도 CANVAS가 있어서 뭘 가져올지 몰라서 에러가 나게 된다.
            그래서 리스폰을 할 때 HP캔버스를 가져오는 경우 다른 방법을 생각해봐야 할 것 같음.
         */
        //EnemySlider = hpBarSliderCanvas.transform.GetChild(2).GetComponent<Slider>();   // 리스폰될때 hp캔버스를 가져오는 코드 


        // 캡슐 콜라이더가 리스폰되면서 다시 생김
        //GetComponent<CapsuleCollider>().enabled = true;

        #endregion



    }



    // Update is called once per frame
    void Update()
    {
   
        // 브레스스킬을 만드는거는 Start가 됐을때 진행되고, 그걸 매개변수로 받아와서 스킬리스트에 추가를 해야함. 
        if (pazeTwoStart == false)
        {

            if (enemyhp < 1000.0f)
            {
                TailAttackSkill_Gray tailAttackSkill_Gray = new TailAttackSkill_Gray(10.0f, 1, anim);
                skillList_Gray.Add(tailAttackSkill_Gray);
                pazeTwoStart = true;
                anim.SetTrigger("PhazeChange");
            }

        }

        // 플레이어와의 거리 계산
        distance = Vector3.Distance(transform.position, player.position);

        // 이동에 따라 애니메이션 전환
        anim.SetFloat("speed", agent.velocity.magnitude); // 방향의 크기만 남기려면 magnitude


        // 상태별로 할 일 정리
        switch (eState)
        {
            case EnemyStete.Idle: Idle(); break;
            case EnemyStete.Walk: Walk(); break;
            case EnemyStete.Attack:;  break;
        }


        // 적과의 거리가 20미만이 될때
        if (distance < 25.0f)
        {
            // 에너미 hp바 켜기
           EnemySlider.gameObject.SetActive(true);
        }

        // 적과의 거리가 20초과가 되거나, hp가 0 이하가 될 때
        if (distance > 25.0f || enemyhp <= 0)
        { 
            // 에너미 hp바 끄기
            EnemySlider.gameObject.SetActive(false);
        }

        if (isAttacking == true)
        {
            Vector3 l_vector = player.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(l_vector), Time.deltaTime * speed);

        }
    }

    void ColliderOn()  // 메테오&페이즈 변환 시작 시 무적을 표현하기 위한 메서드 on : 무적해제  
    {
        capCollider.enabled = true;

    }

    void ColliderOff()  // 메테오&페이즈 시작 시 무적을 표현하기 위한 메서드 off : 무적  
    {
        capCollider.enabled = false;

    }


    // 사운드 작업 

    void GrayEnemySoundStart_Bite()
    {
        AudioManager.instance.PlaySE(grayEnemyAttack1_Sound_Bite, 1,1);
    }

    void GrayEnemySoundStart_Shouting()
    {
        AudioManager.instance.PlaySE(grayEnemyAttack2_Sound_Shouting, 1,1);
    }

    void GrayEnemySoundStart_Tail() // 페이즈가 변할때 용이 울부짖는 소리를 출력 (싱크를 맞추기 위해서 특정 애니메이션에서 호출)
    {
        AudioManager.instance.PlaySE(grayEnemyAttack3_Sound_Tail, 1,1);
    }

    void PhazeChangeSoundStart() // 페이즈가 변할때 용이 울부짖는 소리를 출력 (싱크를 맞추기 위해서 특정 애니메이션에서 호출)
    {
        AudioManager.instance.PlaySE(PhazeChangeSound_Gray, 1,1);
    }

    void DropItemSoundStart()
    {
        AudioManager.instance.PlaySE(DropItemSound, 1, 1);
    }

    void GrayEnemySoundStart_Walk()
    {
        AudioManager.instance.PlaySE(GrayEnemyWalk_Sound, 1, 0.8f);
    }


    void EnemyFix() // 공격하는 도중에는 네비게이션의 속도를 0으로 만들어서 움직이지 않게 하기 위한 메서드
    {
        agent.speed = 0.0f;
    }

    void EnemyMoveStart() // 공격이 끝나고나면 다시 원래대로 네비게이션의 속도를 줘서 플레이어가 멀리 있는경우 따라가도록 하게 하는 메서드.
    {
        agent.speed = 3.5f;
    }


    public void Damaged(float damage) // 공격 받으면 호출

    {
        StartCoroutine(DamageColor()); // 피격시 색깔 변화



        enemyhp -= damage; //체력 감소

        EnemySlider.value = enemyhp;

        agent.isStopped = true;

        agent.ResetPath();


        if (enemyhp <= 0)    // 적의 hp가 0이하로 떨어질 경우.
        {

            anim.SetTrigger("dead"); // 죽음 애니메이션 실행

            Dead(); // 적 죽음 함수 호출
        }

    }

    IEnumerator DamageColor()  // 피격 시 색깔을 바꿔주는 함수 
    {
        mesh.material.color = new Vector4(1f, 0.3349057f, 0.3349057f, 1f);

        yield return new WaitForSeconds(0.1f);

        mesh.material.color = Color.white;
    }

    void Dead() // 적 죽음 함수 
    {
        isAttacking = false;
        bossStart.enabled = true;
        AudioManager.instance.PlaySE(Dead_Sound,1,1);
        AudioManager.instance.PlayStartFieldBgm();

        // 캡슐 콜라이더가 사라짐
        capCollider.enabled = false;

  
        StartCoroutine(EnemyDestroy());

        float GoldRandomCountDrop = Random.Range(1, 2);

        float HpPotionRandomDrop = Random.Range(1, 2);

        float criticalRandomDrop = Random.Range(1, 2);

        switch (GoldRandomCountDrop)
        {
            case 1:


                for (int i = 1; i < 11; i++)
                {
                Instantiate(goldPref, transform.position, Quaternion.identity);
                }


                break;

        }

        switch (HpPotionRandomDrop)
        {
            case 1:
                // 힐링포션 1개 드랍
                Instantiate(hpHealingPotionPref, transform.position, Quaternion.identity);
                // 최대체력증가 물약 1개 드랍
                Instantiate(maxHpPotionPref, transform.position, Quaternion.identity);
                // 스태미나 물약 1개 드랍
                Instantiate(staminaPotion, transform.position, Quaternion.identity);
                break;            
        }

        switch (criticalRandomDrop)
        {
            case 1:         // 전사의 돌 드랍
                Instantiate(criticalPref, transform.position, Quaternion.identity);
                break;

        }

    }

    void Idle() // 기본상태 일 때 할일 
    {

        BackOriginBgm = true;
        startFightBgm = false;
        // 길 찾기 중지 + 경로초기화는 세트로 해주는게 좋음
        // 길 찾기 중기
        agent.isStopped = true;
        // 경로 초기화
        agent.ResetPath();

        // 플레이어와 거리가 20초과 30 이하라면  => 20이하일때만 공격 => 20초과 할 시 공격을 멈춘다.

        if (distance <= 25 )
        {
            eState = EnemyStete.Walk; // 이동 상태로 전환 

        }

    }

    void Walk() // 이동상태 일 때 할일 -> 일정거리 안으로 되면 공격하기 + 멀어지면 idle로 있는 거
    {
        if (startFightBgm == false)
        {
            AudioManager.instance.PlayBossSceneBgm();
            startFightBgm = true;
        }

        agent.isStopped = false; // 길 찾기 시작

        //isAttacking = true; // 플레이어를 향해서 바라보도록 하는 함수 
        //                    //플레이어를 목적지로 두고 따라가기 
        agent.destination = player.position;


        // 플레이어와 거리가 30보다 크다면
        if (distance > 25/* && isAttack == false*/)
        {
            // 기본 상태로 전환
            eState = EnemyStete.Idle;

            if (BackOriginBgm == true)
            {
                AudioManager.instance.PlayStartFieldBgm();
                BackOriginBgm = false;
            }
        }

        //if (distance < 6.5f)
        //{
        //    agent.isStopped = true;
        //}

        //else
        //{
        //    agent.isStopped = false;
        //}

    }

    void Attack()
    {
      
        #region
        //// 공격 시 적의 방향이 나를 바라보면서 회전하는 코드
        //Vector3 l_vector = player.position - transform.position;
        //transform.rotation = Quaternion.LookRotation(l_vector).normalized;
        ////transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(l_vector), Time.deltaTime * speed);


        //// 플레이어와의 거리가 7보다 크다면 
        //if (distance > 6)
        //{
        //    // 이동 상태로 전환
        //    eState = EnemyStete.Walk;

        //    // 네비게이션 자동이동 멈추는 게 false -> 다시 움직인다
        //    agent.isStopped = false;
        //}

        //else
        //{

        //    attackcool += Time.deltaTime;

        //    // 2.5초 주기로 발동되게 하는 딜레이 넣어주기
        //    if (attackcool >= 2.5f)
        //    {
        //        int randomAttack = Random.Range(1, 3); // 50프로 확률로 다른 공격을 하기 위한 변수 

        //        if (randomAttack == 1) // 25프로 확률로 특수공격 
        //        {
        //            // 공격 애니메이션
        //            anim.SetTrigger("attack"); // 공격 애니메이션 실행
        //            AudioManager.instance.PlaySE(grayEnemyAttack1_Sound);
        //        }

        //        else if (randomAttack == 2) // 25프로 확률로 특수공격 
        //        {
        //            // 공격 애니메이션
        //            anim.SetTrigger("attack2");
        //            AudioManager.instance.PlaySE(grayEnemyAttack2_Sound);
        //        }

        //        agent.isStopped = false;

        //        attackcool = 0;
        //    }

        //}
        #endregion
    }

    public void isAttackFalse()  // Animation의 Add event로 호출 
    {
        isAttack = false;  // 공격이 끝난 상태 : 새로운 패턴을 사용 할 수 있는 상태 -> 패턴이 겹치게 사용되는 것을 막기 위함
    }

    public void isAttackTrue()  // Animation의 Add event로 호출 
    {
        isAttack = true;  // 공격중인 상태 : 새로운 패턴을 사용 할 수 없는 상태 -> 패턴이 겹치게 사용되는 것을 막기 위함
    }

    public bool startCool = false;  // 쿨타임 초기화 변수 

    public void SkillStart_Gray()  // SkillStart 코루틴 함수를 호출하는 함수
    {
        if (distance <= 25 && Json.instance.data.hp > 0.0f)
        {
            StartCoroutine(CoroutineSkillStart_Gray());

        }

        else
        {
            eState = EnemyStete.Walk;
            return;
        }


    }

    // 스킬 클래스 자료형의 skillList List를 전역변수로 새롭게 생성.
    public List<Skill> skillList_Gray = new List<Skill>();

    // 사용된 스킬을 저장하는 리스트
    public List<Skill> usedskillList_Gray = new List<Skill>();

    public IEnumerator CoroutineSkillStart_Gray() // 공격중인지 체크 + 우선순위 높은 스킬 선정 + 사용 + 변수저장 + List2에 추가 + List2에 추가한 스킬 삭제 
    {
        /**
         스킬패턴 사용 중에 또 다시 패턴을 사용하는것을 방지하기 위해서 사용!
         현재 Animation의 Add event에서 isAttack을 false로 변경해주는 함수 호출
         */
        //isAttack = true;



        if (skillList_Gray.Count > 0) // isAttack이 false 일때만 기능 실행  *isAttack = true : 공격중인 상태 / false : 공격 중이 아닌상태 == 공격이 끝난 상태
        {

            int pos_Gray = 0;
            int maxNumber = int.MaxValue; // 가장 작은 숫자의 번호를 출력

            for (int j = 0; j < skillList_Gray.Count; j++) // 스킬리스트에 저장된 숫자만큼 반복
            {

                if (skillList_Gray[j].priority < maxNumber)  // priority가 가장 작다면 사용 
                {

                    maxNumber = skillList_Gray[j].priority;
                    pos_Gray = j;

                }

            }

            yield return new WaitForSeconds(2f);  // 공격패턴간의 시간 텀

            Skill usedSkill_Gray = skillList_Gray[pos_Gray];// 우선순위가 가장 높은 스킬을 지역변수로 저장

          


            usedSkill_Gray.Attack();  // 우선순위가 가장 높은 스킬을 사용해서 공격 
          

            usedskillList_Gray.Add(usedSkill_Gray); // 사용한 스킬을 '사용된 스킬 리스트'에 추가 하는 작업 
           

            skillList_Gray.Remove(usedSkill_Gray); // 우선순위가 가장 높다고 뽑힌 스킬을 '스킬 리스트'에서 삭제 
        

            
            startCool = true; // 쿨타임이 돌아간다는 것을 체크하기 위함 (true : 스킬이 시작해서 쿨타임이 돌아가고있는 상태 / false : 쿨타임이 0초가 되어서 안 돌아가고 있는 상태)

            StartCoroutine(coolManager(usedSkill_Gray));  // 쿨타임을 돌리고 ,체크하는 코루틴 메서드 호출 
           
            //yield return new WaitUntil(() => !isAttack); // isAttack = false가 될때까지 기다렸다가 아래 코드로 실행                                                                                 
        }



       

    }

    IEnumerator coolManager(Skill skill) // 사용된 스킬의 쿨타임을 가져와서 0으로 줄여주고, 0이 된 스킬을 변수로 선언 해주는 코루틴함수
    {

        float skillAddCoolTime = 0.1f;

        // 스킬을 사용했다면, 그 스킬의 현재 쿨타임이 0이 될때까지 계속 - 해주기 
        while (skill.coolTime >= skillAddCoolTime)
        {
            skill.coolTime -= Time.deltaTime;  // 프로퍼티에서 set까지 써줘야 직접 값을 변경 할 수 있음.
                                               //print(usedSkill.coolTime);
                                               //Mathf.Min(usedSkill.coolTime = 0.0f); // 0.0f 값 밑으로는 떨어지지 않게끔 제한
            yield return null;
        }

        RefillSkill(skill, skill.originCoolTime);  // 쿨타임이 0이 됐을 경우, 리필해주는 함수 호출

    }


    void RefillSkill(Skill refillSkill, float originCoolTimeValue) // 쿨타임이 0이 된 변수를 List1에 추가 + 추가한 값을 list2에서 삭제 해주는 함수
    {
        refillSkill.coolTime = originCoolTimeValue;  // 리필 하려는 스킬의 쿨타임에 원래 쿨타임 값을 넣어주기 

        skillList_Gray.Add(refillSkill);  // 리필 하려는 스킬을 skillList로 넣어주기

        usedskillList_Gray.Remove(refillSkill); // 사용한 스킬을 담은 usedskillList안의 리필 한 스킬을 삭제해주기.
    }

    // 공격중에는 또 다른 공격을 하는 것을 방지하기 위한 변수 
    public void IsAttackingTrue() // 다음 공격 시작 할 수 있음
    {
        isAttacking = true;
    }

    public void IsAttackingFalse() // 또 다른 공격 못함 
    {
        isAttacking = false;
    }


    public void OnAttackCollision_GrayEnemy() // 애니메이션 책갈피에서 호출
    {
        attackCollision_GrayEnemy.SetActive(true);  // 공격지점 활성화
        //AudioManager.instance.PlaySE(PlayerAttack_Sound); // 공격 사운드 출력

    }

    public void OnAttackCollision_GrayEnemy_TailAttack()
    {
        attackCollision_GrayEnemy_Tail.SetActive(true);
    }

    public void OnAttackCollision_GrayEnemy_Shouting()
    {
        attackCollision_GrayEnemy_Shouting.SetActive(true);
    }

    public IEnumerator ShoutingEffect()
    {
        attackCollision_GrayEnemy_Shouting_Particles[0].SetActive(true);
        attackCollision_GrayEnemy_Shouting_Particles[1].SetActive(true);
        attackCollision_GrayEnemy_Shouting_Particles[2].SetActive(true);

        yield return new WaitForSeconds(2f);

        attackCollision_GrayEnemy_Shouting_Particles[0].SetActive(false);
        attackCollision_GrayEnemy_Shouting_Particles[1].SetActive(false);
        attackCollision_GrayEnemy_Shouting_Particles[2].SetActive(false);

    }

    IEnumerator EnemyDestroy() // 몇초 뒤 몬스터를 삭제하는 코루틴 함수 
    {
        yield return new WaitForSeconds(3.5f);
        
        Destroy(this.gameObject);

    }





    // 플레이어의 피를 깎는 코드 : 애니메이션의 책갈피 기능에서 원하는 타이밍에 호출됨.

    //void RealAttack() // 공격 애니메이션 중에 호출됨.
    //{
    //    // 플레이어에 있는 데미지함수에 enemystr만큼 값을 보내준다.
    //    player.SendMessage("Damaged", enemystr);
    //}

    //void RealAttackDouble() // 특수공격때 플레이어에게 주는 데미지이며, 2배의 데미지를 줌
    //{
    //    // 플레이어에 있는 데미지함수에 enemystr만큼 값을 보내준다.
    //    player.SendMessage("Damaged", enemystr * 2);
    //}
}

