using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;



public abstract class Skill // Skill 추상 클래스
{
    
    public float coolTime { get; set; } // 현재 스킬 쿨타임 값
    public float originCoolTime { get; set; }  // 최초 스킬 쿨타임 값
    public Animator anim;
    public int priority { get; } // 스킬 우선순위 값

    public Skill(float coolTime, int priority, Animator anim)
    {
        this.coolTime = coolTime;
        this.priority = priority;
        this.anim = anim;

        originCoolTime = coolTime;
    }


    public abstract void Attack();  // 상속된 Attack() 실행
}

class BiteSkill : Skill
{

  
    public BiteSkill(float coolTime, int priority, Animator anim) : base(coolTime, priority, anim) 
    {
       
    }

    public override void Attack() 
    {
        // 공격 애니메이션
        anim.SetTrigger("attack");
    }
}


class JumpAttackSkill : Skill
{

    public JumpAttackSkill(float coolTime, int priority, Animator anim) : base(coolTime, priority, anim) 
    {
       
    }

    public override void Attack()
    {
        // 공격 애니메이션
        anim.SetTrigger("attack2");
    }
}


class ShoutingAttackSkill : Skill
{
    [SerializeField]
    private string Attack3_Sound;

    public ShoutingAttackSkill(float coolTime, int priority, Animator anim) : base(coolTime, priority, anim) 
    {
       
    }

    public override void Attack() 
    {
        // 공격 애니메이션
        anim.SetTrigger("attack3");
    }

}


class BreatheAttackSkill : Skill
{

    [SerializeField]
    private string Attack3_Sound;

    public BreatheAttackSkill(float coolTime, int priority, Animator anim) : base(coolTime, priority, anim) 
    {
       
    }

    public override void Attack() // 실제
    {
        // 공격 애니메이션
        anim.SetTrigger("FrameAttack");
    }

}

class MeteoAttackSkill : Skill
{

    [SerializeField]
    private string Attack3_Sound;

    // 추가할 변수 값이 있으면,MeteoAttackSkill(추가 할 변수)
    public MeteoAttackSkill(float coolTime, int priority, Animator anim) : base(coolTime, priority, anim)
    {

    }

    public override void Attack() // 실제
    {
        // 공격 애니메이션
        anim.SetTrigger("FlyFrameAttack");
    }


}


public class Enemy_RedDragon : MonoBehaviour
{

    public float distance; // 플레이어와의 거리
    public float enemyhp; // 적의 체력
    public float wholeEnemyhp = 13000.0f;// 적의 전체 체력
    public float attackSpeed = 10f;
    public float enemystr; // 적의 공격력 
    public bool startCool = false;  // 쿨타임 초기화 변수 


    bool isAttacking = false;
    float attackcool;
    float speed = 3f;
    bool isJump = false;
    bool isAttack = false;
    bool isAttackStart = false;
    bool BackOriginBgm = false;
    bool startFightBgm = false;
    bool pazeTwoStart = false;
    bool pazeThreeStart = false;


    public GameObject attackCollision_Enemy; // 깨물기 공격 콜라이더를 담을 변수 
    public GameObject attackCollision_Enemy_Jump; // 뭉개기 공격콜라이더를 담을 변수 
    public GameObject attackCollision_Enemy_Shouting; // 샤우팅 공격콜라이더를 담을 변수
    public GameObject[] attackCollision_Enemy_Shouting_Particles; // 샤우팅 파티클을 담을 변수 
    public GameObject attackCollision_Enemy_Breath; // 브레쓰 공격 콜라이더를 담을 변수 
    public GameObject meteoPref; // 메테오 프리팹을 담을 변수
    public Transform meteoPort;
    public Transform attackPosition_Head;
    public Transform attackPosition_Center;
    public Transform player; 
    public Slider hpBarSlider; // 적의 체력 바
    public GameObject victoryMessagePanel; // 승리의 독백 메세지 
    public GameSceneMng gameSceneMng;
    public GameObject PhazeChangePanel_Two;
    public GameObject PhazeChangePanel_Three;
    public Rigidbody rigid; // 리지드바디를 담는 변수
    public NavMeshAgent agent; // 네비게이션을 담는 변수
    public CapsuleCollider[] capColliders; // 몬스터 피격판정 콜라이더


    // 스킬 클래스 자료형의 skillList List를 전역변수로 새롭게 생성.
    public List<Skill> skillList = new List<Skill>();

    // 사용된 스킬을 저장하는 리스트
    public List<Skill> usedskillList = new List<Skill>();

    Animator anim;
    SkinnedMeshRenderer mesh; // 피격 시 색깔 변경에 쓰이는 mesh
    AudioManager audioManager;



    // 적 상태 
    public enum EnemyStete
    {
        Idle,        // 기본
        Walk,        // 이동
        Attack,      // 공격
        Dead,         // 죽음
    }

    // 상태 변수 선언 + 첫 상태는 기본 
    public EnemyStete eState = EnemyStete.Idle;


    // 필요한 사운드 이름
    [SerializeField]
    private string damaged_Sound; // 피격

    [SerializeField]
    private string Dead_Sound; // 죽음

    [SerializeField]
    private string redEnemyAttack1_Sound_Bite; // 깨물기 

    [SerializeField]
    private string redEnemyAttack2_Sound_Shouting; // 샤우팅

    [SerializeField]
    private string redEnemyAttack3_Sound_JumpAttack; // 뭉개기 

    [SerializeField]
    private string PhazeChangeSound; // 페이즈 변환

    [SerializeField]
    private string redEnemyAttack4_Sound_Breathe; // 브레스  

    [SerializeField]
    private string redEnemyAttack5_Sound_Fly; // 메테오(파이어볼) 시작 전 날갯짓 효과음 

    [SerializeField]
    private string redEnemyAttack6_Sound_Meteo; // 메테오(파이어볼) 발사 시 공격 효과음 

    [SerializeField]
    private string redEnemyAttack7_Sound_Explosion; // 메테오(파이어볼) 폭발 효과음 

    [SerializeField]
    private string redEnemyAttack8_Sound_Landing; // 메테오(파이어볼) 폭발 후 불길 효과음

    [SerializeField]
    private string redEnemyWalk_Sound; // 라카이서스 걷는 소리 

    [SerializeField]
    private string Boss_Bgm; // 몬스터와 마주했을 때 나오는 Bgm


    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        // 몬스터 공격 생성 
        BiteSkill biteSkill = new BiteSkill(1f, 5, anim);
        JumpAttackSkill jumpAttackSkill = new JumpAttackSkill(8f, 4, anim);
        ShoutingAttackSkill shoutingAttackSkill = new ShoutingAttackSkill(12f, 3, anim);

        // Skill에 상속되어있는 biteSkill를 skillList에 저장! => (Skill)biteSkill의 모양으로도 쓸 수 있음.  
        skillList.Add(biteSkill as Skill); 
        skillList.Add(jumpAttackSkill as Skill);
        skillList.Add(shoutingAttackSkill as Skill);

    }



    IEnumerator PhazeChangeMessage_Two()  // 2페이즈가 시작됨을 알리는 메세지를 on /off 해주는 함수
    {
        PhazeChangePanel_Two.SetActive(true);

        yield return new WaitForSeconds(3.5f);

        PhazeChangePanel_Two.SetActive(false);
    }

    IEnumerator PhazeChangeMessage_Three() // 3페이즈가 시작됨을 알리는 메세지를 on/off 해주는 함수
    {
        PhazeChangePanel_Three.SetActive(true);

        yield return new WaitForSeconds(3.5f);

        PhazeChangePanel_Three.SetActive(false);
    }


    void ColliderOn()  // 메테오&페이즈 변환 시작 시 무적을 표현하기 위한 메서드 on : 무적해제  
    {
        capColliders[0].enabled = true;
        capColliders[1].enabled = true;
        capColliders[2].enabled = true;
    }

    void ColliderOff()  // 메테오&페이즈 시작 시 무적을 표현하기 위한 메서드 off : 무적  
    {
        capColliders[0].enabled = false;
        capColliders[1].enabled = false;
        capColliders[2].enabled = false;
    }

    void RedEnemySoundStart_Bite() 
    {
        AudioManager.instance.PlaySE(redEnemyAttack1_Sound_Bite, 1,1);
    }

    void RedEnemySoundStart_JumpAttack()
    {
        AudioManager.instance.PlaySE(redEnemyAttack3_Sound_JumpAttack, 1,1);
    }

    void RedEnemySoundStart_ShoutingAttack()
    {
        AudioManager.instance.PlaySE(redEnemyAttack2_Sound_Shouting, 1,1);
    }


    void PhazeChangeSoundStart() // 페이즈가 변할때 용이 울부짖는 소리를 출력 (싱크를 맞추기 위해서 특정 애니메이션에서 호출)
    {
        AudioManager.instance.PlaySE(PhazeChangeSound, 1,1);
    }

    void RedEnemySoundStart_Breathe()
    {
        AudioManager.instance.PlaySE(redEnemyAttack4_Sound_Breathe, 1,1);
    }

    void RedEnemySoundStart_Fly()
    {
        AudioManager.instance.PlaySE(redEnemyAttack5_Sound_Fly, 1,1);
    }

    void RedEnemySoundStart_Meteo()
    {
        AudioManager.instance.PlaySE(redEnemyAttack6_Sound_Meteo, 1,1);
    }

    public void RedEnemySoundStart_Explosion()
    {
        AudioManager.instance.PlaySE(redEnemyAttack7_Sound_Explosion, 1,0.8f);
    }

    void RedEnemySoundStart_Landing()
    {
        AudioManager.instance.PlaySE(redEnemyAttack8_Sound_Landing, 1,1);
    }

    void RedEnemySoundStart_Walk()
    {
        AudioManager.instance.PlaySE(redEnemyWalk_Sound, 1,0.7f);
    }



    void EnemyFix() // 공격하는 도중에는 네비게이션의 속도를 0으로 만들어서 움직이지 않게 하기 위한 메서드
    {
        agent.speed = 0.0f;
    }

    void EnemyMoveStart() // 공격이 끝나고나면 다시 원래대로 네비게이션의 속도를 줘서 플레이어가 멀리 있는경우 따라가도록 하게 하는 메서드.
    {
        agent.speed = 3.5f;
    }

    // Update is called once per frame
    void Update()
    {

        if (pazeTwoStart == false)
        { 
          // 2페이즈 시작
          if (enemyhp < 10000.0f)
          {
                BreatheAttackSkill breatheAttackSkill = new BreatheAttackSkill(15f, 2, anim);
                skillList.Add(breatheAttackSkill);
                pazeTwoStart = true;
                anim.SetTrigger("PhazeChange");
                StartCoroutine(PhazeChangeMessage_Two());
          }

        }

        if (pazeThreeStart == false)
        {
            // 3페이즈 시작
            if (enemyhp < 5000.0f)
            {
               
                MeteoAttackSkill meteoAttackSkill = new MeteoAttackSkill(35f, 1, anim);
                skillList.Add(meteoAttackSkill);
                pazeThreeStart = true;
                anim.SetTrigger("PhazeChange");
                StartCoroutine(PhazeChangeMessage_Three());
                
            }
        
        }

        // 플레이어와의 거리 계산
        distance = Vector3.Distance(transform.position, player.position);

        // 이동에 따라 애니메이션 전환
        anim.SetFloat("speed", agent.velocity.magnitude); 


        // 상태별로 할 일 정리
        switch (eState)
        {
            case EnemyStete.Idle: Idle(); break;
            case EnemyStete.Walk: Walk(); break;
        }

        // 적과의 거리가 30미만이 될때
        if (distance < 30.0f)
        {
            // 에너미 hp바 켜기
            hpBarSlider.gameObject.SetActive(true);
        }

        // 적과의 거리가 30이상 될때
        if (distance >= 30.0f || enemyhp <= 0)
        {
            // 에너미 hp바 끄기
            hpBarSlider.gameObject.SetActive(false);
        }

        if (isAttacking == true) // 몬스터가 플레이어를 향해 회전하는 코드 
        {
            Vector3 l_vector = player.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(l_vector), Time.deltaTime * speed);
        }
    }



    public void Damaged(float damage) // 공격 받으면 호출

    {
        StartCoroutine(DamageColor()); // 피격시 색깔 변화

        enemyhp -= damage; 
        hpBarSlider.value = enemyhp; 

        agent.isStopped = true; // 움직임 멈추기

        agent.ResetPath(); // 경로 초기화

        // 몬스터가 죽었을 때
        if (enemyhp < 0)
        {
            anim.SetTrigger("dead");
            Dead();

        }

    }

    // 적 죽음 
    void Dead()
    {
        isAttacking = false;

        AudioManager.instance.PlayEndingBgm();
        AudioManager.instance.PlayStartBossBgm();
        AudioManager.instance.PlaySE(Dead_Sound,1,1);

        // 캡슐 콜라이더가 사라짐
        capColliders[0].enabled = false;
        capColliders[1].enabled = false;
        capColliders[2].enabled = false;

        StartCoroutine(startEndingScene()); // 엔딩씬으로 가는 코루팀 함수 호출 

    }

    IEnumerator startEndingScene() // 엔딩씬으로 넘어가는 코루틴 함수
    {
        yield return new WaitForSeconds(10f);

        gameSceneMng.StartEndingScene();
    }

    void Idle() // 기본상태 일 때 할일 
    {
        BackOriginBgm = true;
        startFightBgm = false;

        // 길 찾기 중기
        agent.isStopped = true;
        // 경로 초기화
        agent.ResetPath();

        if (distance <= 30.0f)
        {
            eState = EnemyStete.Walk; // 이동 상태로 전환 
        }
    }

    void Walk() // 이동상태 일 때 할일 -> 일정거리 안으로 되면 공격하기 + 멀어지면 idle로 변경
    {

        if (startFightBgm == false)
        {
            AudioManager.instance.PlayBossSceneBgm();
            startFightBgm = true;
        }
        agent.isStopped = false; // 길 찾기 시작
        agent.destination = player.position;

        // 플레이어와 거리가 30보다 크다면
        if (distance > 30.0f)
        {
            // 기본 상태로 전환
            eState = EnemyStete.Idle;

            // 기존 Bgm으로 변경
            if (BackOriginBgm == true)
            {
                AudioManager.instance.PlayStartBossBgm();
                BackOriginBgm = false;
            }

        }

        if (distance < 12.0f)
        {
            agent.isStopped = true;
        }

        else
        {
            agent.isStopped = false;
        }


    }



    void Attack()
    {
        #region 
        // 페이즈에 따른 몬스터 패턴을 랜덤확률로 구현하려 했으나, 쿨타임과 우선순위로 패턴을 구현하기 위해 사용하지 않음
        //{
        //    StartCoroutine(AttackStart());
        //}

        //isAttack = false;

        //yield return new WaitForSeconds(0.1f); // 이 시간이 길어질 수록 난이도가 쉬워짐 -> 난이도 조절에 용이함

        //if (distance > 12f)
        //{

        //    agent.enabled = false;

        //    //// 네비게이션이 비활성화 됐을때 -y축으로 추락하지 않도록 하는 코드   -> 이거는 툴의 설정을 바꾸는거라 굉장히 위험
        //    //RigidbodyConstraints originConstraints = rigid.constraints;
        //    //rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

        //    //agent.enabled = true;

        //    // 네비게이션이 비활성화 됐을때 -y축으로 추락하지 않도록 하는 코드 마무리 -> 이거는 툴의 설정을 바꾸는거라 굉장히 위험함.
        //    //rigid.constraints = originConstraints;


        //    StartCoroutine(Think());
        //}


        //if (enemyhp >= 10000f && enemyhp < 15000f) // 1페이즈

        //{

        //    int randomAction1 = UnityEngine.Random.Range(4, 5);


        //    switch (randomAction1) // && 지금 몬스터가공격중이 아닐때)
        //    {
        //        // 깨물기 공격 (40프로)
        //        case 0:
        //        case 1:
        //            StartCoroutine(Attack_Basic());
        //            break;
        //        // 뭉개기 공격 (40프로)
        //        case 2:
        //        case 3:
        //            StartCoroutine(Attack_Jump());
        //            break;
        //        // 뒤로 점프 (20프로)
        //        case 4:
        //            StartCoroutine(Move_BackJump());

        //            break;

        //    }

        //}

        //if (enemyhp >= 5000f && enemyhp < 10000f) // 1페이즈
        //{
        //    int randomAction2 = UnityEngine.Random.Range(0, 6);
        //    switch (randomAction2)
        //    {
        //        // 깨물기 공격 (16프로)
        //        case 0:
        //            StartCoroutine(Attack_Basic());
        //            break;

        //        // 샤우팅 공격 (33프로)
        //        case 1:
        //        case 2:
        //            StartCoroutine(Attack_Shouting());

        //            break;
        //        // 뭉개기 공격 (33프로)
        //        case 3:
        //        case 4:
        //            StartCoroutine(Attack_Jump());
        //            break;

        //        // 뒤로 점프 (16프로)
        //        case 5:
        //            StartCoroutine(Move_BackJump());
        //            break;

        //    }

        //}

        //if (enemyhp < 5000f)
        //{

        //    int randomAction3 = UnityEngine.Random.Range(0, 10);
        //    switch (randomAction3)
        //    {
        //        // 뭉개기 공격 (10프로)
        //        case 0:

        //            StartCoroutine(Attack_Jump());
        //            break;
        //        // 샤우팅 공격 (20프로)
        //        case 1:
        //        case 6:
        //            StartCoroutine(Attack_Shouting());
        //            break;

        //        // 브레스 공격 (30프로)
        //        case 2:
        //        case 3:
        //        case 7:
        //            StartCoroutine(Attack_Breath());
        //            break;
        //        // 메테오 공격 (40프로)
        //        case 4:
        //        case 5:
        //        case 8:
        //        case 9:
        //            StartCoroutine(Attack_Meteo());
        //            break;


        //    }


        //}

        #endregion
    }



    // 공격시작을 구분짓기 위한 변수 (true == 시작함 / false == 공격중이 아님)

    public void isAttackFalse()  // Animation의 Add event로 호출 
    {
        isAttack = false;  // 공격이 끝난 상태 : 새로운 패턴을 사용 할 수 있는 상태 -> 패턴이 겹치게 사용되는 것을 막기 위함
    }

    public void isAttackTrue()  // Animation의 Add event로 호출 
    {
        isAttack = true;  // 공격중인 상태 : 새로운 패턴을 사용 할 수 없는 상태 -> 패턴이 겹치게 사용되는 것을 막기 위함
    }


    public void SkillStart()  // SkillStart 코루틴 함수를 호출하는 함수
    {
        if (distance <= 30 && Json.instance.data.hp > 0.0f)
        {
            StartCoroutine(CoroutineSkillStart());
        }

        else
        {
            eState = EnemyStete.Walk;
            return;
        }

    }
    
    public IEnumerator CoroutineSkillStart() // 우선순위 높은 스킬 선정 + 사용 + 변수저장 + List2에 추가 + List2에 추가한 스킬 삭제 
    {
  

        if (skillList.Count > 0)  // 공격할 수 있는 스킬의 개수가 1개 이상인 경우에만
        {

            int pos = 0; // 우선순위를 담을 변수 초기화

            int maxNumber = int.MaxValue; // 가장 작은 숫자의 번호를 출력

            for (int i = 0; i < skillList.Count; i++) // 스킬리스트에 저장된 숫자만큼 반복
            {

                if (skillList[i].priority < maxNumber)  // priority가 가장 작다면 사용 
                {
                    maxNumber = skillList[i].priority;
                    pos = i;
                }
            }

            yield return new WaitForSeconds(1f);  // 공격패턴간의 시간 텀

            Skill usedSkill = skillList[pos];// 우선순위가 가장 높은 스킬을 지역변수로 저장
    
            usedSkill.Attack();  // 우선순위가 가장 높은 스킬을 사용해서 공격 

            usedskillList.Add(usedSkill); // 사용한 스킬을 '사용된 스킬 리스트'에 추가 하는 작업    

            skillList.Remove(usedSkill); // 우선순위가 가장 높다고 뽑힌 스킬을 '스킬 리스트'에서 삭제 
           
            startCool = true; // 쿨타임이 돌아간다는 것을 체크하기 위함 (true : 스킬이 시작해서 쿨타임이 돌아가고있는 상태 / false : 쿨타임이 0초가 되어서 안 돌아가고 있는 상태)

            StartCoroutine(coolManager(usedSkill));  // 쿨타임을 돌리고 ,체크하는 코루틴 메서드 호출                                                                               
        }

    }

    IEnumerator coolManager(Skill skill) // 사용된 스킬의 쿨타임을 가져와서 0으로 줄여주고, 0이 된 스킬을 변수로 선언 해주는 코루틴함수
    {

        float skillAddCoolTime = 0.1f;

            // 스킬을 사용했다면, 그 스킬의 현재 쿨타임이 0이 될때까지 계속 
            while (skill.coolTime >= skillAddCoolTime)
            {
                skill.coolTime -= Time.deltaTime;  
                                               
                yield return null;
            }

            RefillSkill(skill, skill.originCoolTime);  // 쿨타임이 0이 됐을 경우, 리필해주는 함수 호출

    }


    void RefillSkill(Skill refillSkill, float originCoolTimeValue) // 쿨타임이 0이 된 변수를 List1에 추가 + 추가한 값을 list2에서 삭제 해주는 함수
    {
        refillSkill.coolTime = originCoolTimeValue;  // 리필 하려는 스킬의 쿨타임에 원래 쿨타임 값을 넣어주기 

        skillList.Add(refillSkill);  // 리필 하려는 스킬을 skillList로 넣어주기

        usedskillList.Remove(refillSkill); // 사용한 스킬을 담은 usedskillList안의 리필 한 스킬을 삭제해주기.
    }


    public void Attack_MeteoStart() //메테오 공격 함수 - 애니메이션에서 호출
    {

        StartCoroutine(Attack_MeteoCoroutine());

    }

    // 메테오프리팹 생성 코루틴 함수 
    IEnumerator Attack_MeteoCoroutine()
    {
        GameObject meteo = Instantiate(meteoPref, meteoPort.position, meteoPort.rotation);

        yield return new WaitForSeconds(1f);

        meteo = Instantiate(meteoPref, meteoPort.position, meteoPort.rotation);

        yield return new WaitForSeconds(1f);

        meteo = Instantiate(meteoPref, meteoPort.position, meteoPort.rotation);

        yield return new WaitForSeconds(1f);

    }

    public void IsAttackingTrue()
    {
        isAttacking = true;
    }

    public void IsAttackingFalse()
    {
        isAttacking = false;
    }

    // 플레이어에게 피해를 주는 함수 : 애니메이션의 책갈피 기능에서 원하는 타이밍에 호출됨.
    void RealAttack() // 기본공격때 플레이어에게 주는 데미지.
    {
        player.SendMessage("Damaged", enemystr);
    }

    void RealAttackDouble() // 특수공격때 플레이어에게 주는 데미지이며, 2배의 데미지를 줌
    {
        player.SendMessage("Damaged", enemystr * 2);
    }


    void RealAttackTriple() // 특수공격때 플레이어에게 주는 데미지이며, 3배의 데미지를 줌
    {
        player.SendMessage("Damaged", enemystr * 3);
    }

    public void OnAttackCollision_Enemy() // 애니메이션 책갈피에서 호출
    {
        attackCollision_Enemy.SetActive(true);  // 공격지점 활성화

    }

    public void OnAttackCollision_Enemy_JumpAttack()
    {
        attackCollision_Enemy_Jump.SetActive(true);
    }

    public void OnAttackCollision_Enemy_Shouting()
    {
        attackCollision_Enemy_Shouting.SetActive(true);
    }

    public void OnAttackCollision_Enemy_Breath()
    {
        attackCollision_Enemy_Breath.SetActive(true);
    }

    // 샤우팅 공격 시 파티클이펙트 생성 및 해제
    public IEnumerator ShoutingEffect()
    {
        attackCollision_Enemy_Shouting_Particles[0].SetActive(true);
        attackCollision_Enemy_Shouting_Particles[1].SetActive(true);
        attackCollision_Enemy_Shouting_Particles[2].SetActive(true);

        yield return new WaitForSeconds(2f);

        attackCollision_Enemy_Shouting_Particles[0].SetActive(false);
        attackCollision_Enemy_Shouting_Particles[1].SetActive(false);
        attackCollision_Enemy_Shouting_Particles[2].SetActive(false);

    }

    // 피격시 색깔 변경 
    IEnumerator DamageColor()
    {
        mesh.material.color = new Vector4(0.9622642f, 0.3948914f, 0.9108856f, 1f);

        yield return new WaitForSeconds(0.1f);

        mesh.material.color = Color.white;
    }


    #region
    // 아래 공격 함수는 현재 사용하지 않음.
    // 몬스터 공격에 상속과 생성자메서드로 구현하기 전, 몬스터 패턴을 확률로만 개발할 당시 작성했던 코드

    //IEnumerator Attack_Basic()
    //{
    //    isAttack = true;

    //    yield return new WaitForSeconds(3.0f);
    //    // 공격중이아닐때의 변수를 false로 - isAttack = false
    //}

    //IEnumerator Attack_Shouting()
    //{
    //    // 샤우팅
    //    // 공격 애니메이션

    //    yield return new WaitForSeconds(4.0f);
    //    StartCoroutine(AttackStart());
    //}

    //IEnumerator Attack_Jump()
    //{

    //    yield return new WaitForSeconds(5.0f);
    //    StartCoroutine(AttackStart());
    //}

    //IEnumerator Attack_Breath()
    //{

    //    yield return new WaitForSeconds(5.0f);

    //    agent.speed = 2.5f;
    //    StartCoroutine(AttackStart());

    //    transform.position = Vector3.MoveTowards(transform.position, transform.position + (-transform.forward * 100.0f), 0.1f);
    //}

    //IEnumerator Attack_Meteo()
    //{

    //    yield return new WaitForSeconds(13.0f);
    //    StartCoroutine(AttackStart());

    //}

    //public float jumpTime; // 점프하고있는 시간
    //public float jumpForce;  // 뒤로 점프할때 밀어내는 힘 

    //IEnumerator Move_BackJump()
    //{

    //    anim.SetTrigger("Move_BackJump");
    //    yield return new WaitForSeconds(1.0f);

    //    agent.isStopped = true; // 몬스터 무빙 멈추기1
    //    agent.enabled = false; //  네비게이션 비활성화
    //    rigid.velocity = Vector3.zero; // 속도 0 으로 내리기 
    //    rigid.AddForce(-transform.forward * jumpForce * 2 + transform.up * jumpForce, ForceMode.Impulse);
    //    yield return new WaitForSeconds(jumpTime);

    //    rigid.useGravity = false;
    //    rigid.velocity = Vector3.zero;
    //    agent.enabled = true;
    //    agent.isStopped = false;


    //    rigid.AddForce(transform.forward * -25, ForceMode.Impulse); // 뒤로 물어나게 되는 것

    //    yield return new WaitForSeconds(2.0f);
    //    StartCoroutine(AttackStart());

    //}

    #endregion

}