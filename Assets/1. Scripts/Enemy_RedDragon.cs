using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;



class BiteSkill : Skill
{

    // 몬스터 스킬 생성자 메소드
    public BiteSkill(float coolTime, int priority, Animator anim) : base(coolTime, priority, anim) 
    {
       
    }

    public override void Attack() // 실제
    {
        // 공격 애니메이션
        anim.SetTrigger("attack");
    }
}


class JumpAttackSkill : Skill
{

    public JumpAttackSkill(float coolTime, int priority, Animator anim) : base(coolTime, priority, anim) // 생성자 메소드
    {
        // 위 변수 외에 추가 할 내용이 있으면 여기에 추가 
    }

    public override void Attack() // 실제
    {
        anim.SetTrigger("attack2");
    }
}


class ShoutingAttackSkill : Skill
{
    [SerializeField]
    private string Attack3_Sound;

    public ShoutingAttackSkill(float coolTime, int priority, Animator anim) : base(coolTime, priority, anim) // 생성자 메소드
    {
        // 위 변수 외에 추가 할 내용이 있으면 여기에 추가 
    }

    public override void Attack() // 실제
    {
        anim.SetTrigger("attack3");
    }

}



class BackJumpSkill : Skill
{
    NavMeshAgent agent;
    Rigidbody rigid;
    Transform transform;

    float jumpTime = 0.2f; // 점프하고있는 시간
    float jumpForce = 5.0f;  // 뒤로 점프할때 밀어내는 힘 


    [SerializeField]
    private string Attack3_Sound;


    // 생성자 메소드
    public BackJumpSkill(float coolTime, int priority, Animator anim, NavMeshAgent agent, Rigidbody rigid, Transform transform) : base(coolTime, priority, anim)
    {

        this.agent = agent;
        this.rigid = rigid;
        this.transform = transform;
    }


    public override void Attack() // 실제
    {
        anim.SetTrigger("Move_BackJump");

    }



    public IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(0.2f);

        agent.isStopped = true; // 몬스터 무빙 멈추기
        agent.enabled = false; //  네비게이션 비활성화
        rigid.velocity = Vector3.zero; // 속도 0 으로 내리기 
        rigid.AddForce(-transform.forward * jumpForce * 2 + transform.up * jumpForce, ForceMode.Impulse);

        yield return new WaitForSeconds(1.0f);

        rigid.useGravity = false;
        rigid.velocity = Vector3.zero;
        agent.enabled = true;
        agent.isStopped = false;

    }

    public IEnumerator WaitJumpTime()
    {
        yield return new WaitForSeconds(0.2f);
    }



}

class BreatheAttackSkill : Skill
{

    [SerializeField]
    private string Attack3_Sound;

    public BreatheAttackSkill(float coolTime, int priority, Animator anim) : base(coolTime, priority, anim) // 생성자 메소드
    {
        // 위 변수 외에 추가 할 내용이 있으면 여기에 추가 
    }

    public override void Attack() // 실제
    {
        anim.SetTrigger("FrameAttack");

    }

}

class MeteoAttackSkill : Skill
{

    [SerializeField]
    private string Attack3_Sound;


    // 추가할 변수 값이 있으면,MeteoAttackSkill(추가 할 변수)
    public MeteoAttackSkill(float coolTime, int priority, Animator anim) : base(coolTime, priority, anim) // 생성자 메소드
    {

    }

    public override void Attack() // 실제
    {
        // 공격 애니메이션
        anim.SetTrigger("FlyFrameAttack");
        //AudioManager.instance.PlaySE(Attack3_Sound);
    }


}

public abstract class Skill // Skill 추상 클래스
{
    //protected string name; // 스킬 이름
    public float coolTime { get; set; } // 스킬 쿨타임 값
    public float originCoolTime { get; set; }
    public Animator anim;
    public int priority { get; } // 스킬 우선순위 값

    public Skill(float coolTime, int priority, Animator anim/*, string name,*/)
    {
        // this는 'Skill 클래스의' 를 의미하고, Skill class의 멤버변수 name안에 string name을 넣어주는 것.
        //this.name = name;
        this.coolTime = coolTime;
        this.priority = priority;
        this.anim = anim;

        originCoolTime = coolTime;
    }


    public abstract void Attack();  // 상속된 Attack()을 실행시키는 코드
}






public class Enemy_RedDragon : MonoBehaviour
{


    public float distance; // 플레이어와의 거리
    public float enemyhp; // 적의 체력
    public float wholeEnemyhp = 13000.0f;// 적의 전체 체력
    public GameObject attackCollision_Enemy; // 깨물기 공격 콜라이더를 담을 변수 
    public GameObject attackCollision_Enemy_Jump; // 뭉개기 공격콜라이더를 담을 변수 
    public GameObject attackCollision_Enemy_Shouting; // 샤우팅 공격콜라이더를 담을 변수
    public GameObject[] attackCollision_Enemy_Shouting_Particles; // 샤우팅 파티클을 담을 변수 
    //public Transform backJumpPos;

    public GameObject attackCollision_Enemy_Breath; // 브레쓰 공격 콜라이더를 담을 변수 
    //public GameObject attackCollision_Enemy_Breath_Particle; // 브레쓰 공격 파티클을 담을 변수 

    public GameObject meteoPref; // 메테오 프리팹을 담을 변수
    public Transform meteoPort;
    public float attackSpeed = 10f;


    public Transform attackPosition_Head;
    public Transform attackPosition_Center;

    public float enemystr; // 적의 공격력 
    public Transform player; // 플레이어 가져오기(hp랑 플레이어 위치를 가져와야함)
    public Slider hpBarSlider; // 적의 체력 바
    public GameObject victoryMessagePanel; // 승리의 독백 메세지 가져오기
    public GameSceneMng gameSceneMng;

    public GameObject PhazeChangePanel_Two;
    public GameObject PhazeChangePanel_Three;

    bool isAttacking = false;
    float attackcool;
    float speed = 3f;

    bool isJump = false;
    bool isAttack = false;
    bool isAttackStart = false;
    bool BackOriginBgm = false;

    bool startFightBgm = false;

    public Rigidbody rigid; // 리지드바디를 담는 변수
    Animator anim;
    public NavMeshAgent agent; // 네비게이션을 담는 변수

    AudioManager audioManager;

    SkinnedMeshRenderer mesh; // 피격 시 색깔 변경에 쓰이는 mesh

    public CapsuleCollider[] capColliders; // 몬스터 피격판정 콜라이더



    // 적 상태를 나타내는 자료형 (열거형) 
    public enum EnemyStete
    {
        Idle,        // 기본
        Walk,        // 이동
        Attack,      // 공격
        Dead,         // 죽음
        //BackJump,     // 뒤로점프
        //InProgress,
    }

    // 상태 변수 선언 + 첫 상태는 기본 
    public EnemyStete eState = EnemyStete.Idle;



    // 필요한 사운드 이름
    [SerializeField]
    private string damaged_Sound;

    [SerializeField]
    private string Dead_Sound;

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
    private string Boss_Bgm;

    public static Enemy_RedDragon instance; // 인스턴스화

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

        BiteSkill biteSkill = new BiteSkill(1f, 5, anim);
        JumpAttackSkill jumpAttackSkill = new JumpAttackSkill(8f, 4, anim);
        ShoutingAttackSkill shoutingAttackSkill = new ShoutingAttackSkill(12f, 3, anim);


        skillList.Add(biteSkill as Skill); // Skill에 상속되어있는 biteSkill를 skillList에 저장! => (Skill)biteSkill의 모양으로도 쓸 수 있음.  
        skillList.Add(jumpAttackSkill as Skill);
        skillList.Add(shoutingAttackSkill as Skill);

    }

    bool pazeTwoStart = false;
    bool pazeThreeStart = false;

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
    void RedEnemySoundStart_Bite() //
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
        anim.SetFloat("speed", agent.velocity.magnitude); // 방향의 크기만 남기려면 magnitude


        // 상태별로 할 일 정리
        switch (eState)
        {
            case EnemyStete.Idle: Idle(); break;
            case EnemyStete.Walk: Walk(); break;
            case EnemyStete.Attack:; break;


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


        enemyhp -= damage; //체력 감소
        hpBarSlider.value = enemyhp; // hp값 슬라이더에 반영

        agent.isStopped = true; // 움직임 멈추기

        agent.ResetPath(); // 경로 초기화

        // 적의 hp가 0보다 작을 때의 상황 => 몬스터가 죽었을때
        if (enemyhp < 0)
        {

            anim.SetTrigger("dead");

            // 죽었을 때 
            Dead();

        }


    }

    // 적 죽음 
    void Dead()
    {
        isAttacking = false;

        //Json.instance.Save();

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

        // 길 찾기 중지 + 경로초기화는 세트로 해주는게 좋음
        // 길 찾기 중기
        agent.isStopped = true;
        // 경로 초기화
        agent.ResetPath();

        // 플레이어와 거리가 20초과 30 이하라면  => 20이하일때만 공격 => 20초과 할 시 공격을 멈춘다.

        if (distance <= 30)
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

        agent.destination = player.position;


        // 플레이어와 거리가 60보다 크다면
        if (distance > 30/* && isAttack == false*/)
        {
            // 기본 상태로 전환
            eState = EnemyStete.Idle;

            if (BackOriginBgm == true)
            {
                AudioManager.instance.PlayStartBossBgm();
                BackOriginBgm = false;
            }

        }

        if (distance < 12)
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
        // 1.0 ver 몬스터 공격 

        //// 플레이어와의 거리가 30보다 크다면 
        //if (distance > 40)
        //{
        //    // 이동 상태로 전환
        //    eState = EnemyStete.Walk;

        //    // 네비게이션 자동이동 멈추는 게 false니까 -> 다시 움직인다
        //    agent.isStopped = false;
        //}

        //else  // 플레이어와의 거리가 40 이하라면
        //{
        //    if (!isAttack)
        //    {
        //        isAttack = true;

        //        //공격 실행 

        //        isAttack = false;

        //    }


        // 이동 상태로 전환
        //eState = EnemyStete.BackJump;



        //attackcool += Time.deltaTime;

        //  // 2.5초 주기로 발동되게 하는 딜레이 넣어주기
        //  if (attackcool >= 4.5f)
        //  {

        //    int randomAttack = Random.Range(1, 3);  // 1페이즈 공격 확률
        //    int randomAttack2 = Random.Range(1, 4); // 2페이즈 공격 확률

        //    // 1페이즈
        //    if (enemyhp >= 10000f && enemyhp < 15000f) 
        //    {

        //        if (randomAttack == 2) // 25프로 확률로 특수공격 
        //        {
        //            // 샤우팅 공격
        //            StartCoroutine(Attack_Shouting());
        //        }

        //        if (randomAttack == 1)
        //        {
        //            // 물기
        //            StartCoroutine(Attack_Basic());
        //        }


        //        agent.isStopped = false;

        //        attackcool = 0;
        //    }


        //    // 2페이즈 
        //    if (enemyhp >= 5000f && enemyhp < 10000f)
        //    {
        //        if (randomAttack2 == 1) // 25프로 확률로 특수공격 
        //        {
        //            // 점프공격
        //            StartCoroutine(Attack_Jump());
        //        }

        //        if (randomAttack2 == 2) // 25프로 확률로 특수공격 
        //        {
        //            // 물기
        //            StartCoroutine(Attack_Basic());
        //        }

        //        if (randomAttack2 == 3) // 25프로 확률로 특수공격 
        //        {
        //            // 샤우팅 공격
        //            StartCoroutine(Attack_Shouting());
        //        }


        //        agent.isStopped = false;

        //        attackcool = 0;

        //    }



        //  }

        //    // 3페이즈 
        //    if (enemyhp >= 2500f && enemyhp < 5000f)
        //    {

        //     int randomAttack3 = Random.Range(1, 2); // 3페이즈 공격 확률

        //      if (attackcool >= 3.5f)
        //      {

        //        if (randomAttack3 == 3) // 25프로 확률로 특수공격 
        //        {

        //            StartCoroutine(Attack_Breath());
        //        }

        //        if (randomAttack3 == 1) // 25프로 확률로 특수공격 
        //        {

        //            // 샤우팅
        //            StartCoroutine(Attack_Shouting());

        //        }

        //        if (randomAttack3 == 3)
        //        {
        //            StartCoroutine(Attack_Basic());

        //        }

        //        agent.isStopped = false;

        //        attackcool = 0;


        //      }
        //    }

        ////if (enemyhp >= 1500f && enemyhp < 3000f)
        ////{

        ////    int randomAttack4 = Random.Range(1, 2); // 3페이즈 공격 확률

        ////    if (attackcool >= 7f) 
        ////    {

        ////        if (randomAttack4 == 1) // 25프로 확률로 특수공격 
        ////        {

        ////            Attack_Breath();


        ////        }


        ////        agent.isStopped = false;

        ////        attackcool = 0;


        ////    }
        ////}

        //if (enemyhp < 2500f)
        //{

        //    int randomAttack5 = Random.Range(1, 2); // 3페이즈 공격 확률

        //    if (attackcool >= 12f)
        //    {


        //        if (randomAttack5 == 1) // 25프로 확률로 특수공격 
        //        {
        //            StartCoroutine(Attack_Meteo());

        //        }


        //        agent.isStopped = false;

        //        attackcool = 0;


        //    }
        //}













        //}

        #endregion
    }


    // 스킬 클래스 자료형의 skillList List를 전역변수로 새롭게 생성.
    public List<Skill> skillList = new List<Skill>();

    // 사용된 스킬을 저장하는 리스트
    public List<Skill> usedskillList = new List<Skill>();

    // 공격시작을 구분짓기 위한 변수 (true == 시작함 / false == 공격중이 아님)

    public void isAttackFalse()  // Animation의 Add event로 호출 
    {
        isAttack = false;  // 공격이 끝난 상태 : 새로운 패턴을 사용 할 수 있는 상태 -> 패턴이 겹치게 사용되는 것을 막기 위함
    }

    public void isAttackTrue()  // Animation의 Add event로 호출 
    {
        isAttack = true;  // 공격중인 상태 : 새로운 패턴을 사용 할 수 없는 상태 -> 패턴이 겹치게 사용되는 것을 막기 위함
    }

    public bool startCool = false;  // 쿨타임 초기화 변수 

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

        #region
        // List로 스킬 관리 하기 전 확률에 따른 공격패턴 코드 

        //else
        //{
        //    StartCoroutine(AttackStart());
        //}






        //isAttack = false;

        //Vector3 backJumpDir = -player.forward;
        //yield return new WaitForSeconds(0.1f); // 이 시간이 길어질 수록 난이도가 쉬워짐 -> 난이도 조절에 용이함





        //if (distance > 12f)
        //{
        //    print("ifOK");
        //    print(agent.enabled);
        //    agent.enabled = false;

        //    //// 네비게이션이 비활성화 됐을때 -y축으로 추락하지 않도록 하는 코드   -> 이거는 툴의 설정을 바꾸는거라 굉장히 위험함.
        //    //RigidbodyConstraints originConstraints = rigid.constraints;
        //    //rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;


        //    //distance 값 확인해보기.
        //    print(distance);

        //    if (distance < 9)
        //    {

        //        while (distance > 20f)
        //        {

        //            print("While");

        //            yield return null;


        //        }
        //    }

        //    //agent.enabled = true;
        //    print(agent.enabled);

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

        skillList.Add(refillSkill);  // 리필 하려는 스킬을 skillList로 넣어주기

        usedskillList.Remove(refillSkill); // 사용한 스킬을 담은 usedskillList안의 리필 한 스킬을 삭제해주기.
    }



    #region
    IEnumerator Attack_Basic()
    {


        // isAttack = true;

        yield return new WaitForSeconds(3.0f);
        // 공격중이아닐때의 변수를 false로 - isAttack = false



    }

    IEnumerator Attack_Shouting()
    {
        // 샤우팅
        // 공격 애니메이션

        yield return new WaitForSeconds(4.0f);
        //StartCoroutine(AttackStart());
    }

    IEnumerator Attack_Jump()
    {

        yield return new WaitForSeconds(5.0f);
        //StartCoroutine(AttackStart());
    }

    IEnumerator Attack_Breath()
    {



        yield return new WaitForSeconds(5.0f);

        agent.speed = 2.5f;
        //StartCoroutine(AttackStart());

        // 포지션값이 포지션의 뒤쪽으로 이동하는 코드 => 안되는 중
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + (-transform.forward * 100.0f), 0.1f);
    }

    IEnumerator Attack_Meteo()
    {

        yield return new WaitForSeconds(13.0f);
        //StartCoroutine(AttackStart());

    }

    public float jumpTime; // 점프하고있는 시간
    public float jumpForce;  // 뒤로 점프할때 밀어내는 힘 

    IEnumerator Move_BackJump()
    {

        anim.SetTrigger("Move_BackJump");
        yield return new WaitForSeconds(1.0f);

        agent.isStopped = true; // 몬스터 무빙 멈추기1
        agent.enabled = false; //  네비게이션 비활성화
        rigid.velocity = Vector3.zero; // 속도 0 으로 내리기 
        rigid.AddForce(-transform.forward * jumpForce * 2 + transform.up * jumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(jumpTime);

        rigid.useGravity = false;
        rigid.velocity = Vector3.zero;
        agent.enabled = true;
        agent.isStopped = false;


        //rigid.AddForce(transform.forward * -25, ForceMode.Impulse); // 뒤로 물어나게 되는 것

        yield return new WaitForSeconds(2.0f);
        //StartCoroutine(AttackStart());

    }



    public void IsAttackingTrue()
    {
        isAttacking = true;
    }

    public void IsAttackingFalse()
    {
        isAttacking = false;
    }

    // 플레이어의 피를 깎는 코드 : 애니메이션의 책갈피 기능에서 원하는 타이밍에 호출됨.
    void RealAttack() // 기본공격때 플레이어에게 주는 데미지.
    {

        player.SendMessage("Damaged", enemystr);  // 플레이어에 있는 데미지함수에 enemystr만큼 값을 보내준다.


    }

    void RealAttackDouble() // 특수공격때 플레이어에게 주는 데미지이며, 2배의 데미지를 줌
    {
        // 플레이어에 있는 데미지함수에 enemystr만큼 값을 보내준다.
        player.SendMessage("Damaged", enemystr * 2);
    }
    //void DamagedEnd() // 피격 애니메이션 끝날 때 호출됨.
    //{
    //    // 공격 상태로 전환
    //    eState = EnemyStete.Attack;
    //}

    void RealAttackTriple() // 특수공격때 플레이어에게 주는 데미지이며, 3배의 데미지를 줌
    {

        player.SendMessage("Damaged", enemystr * 3);
    }

    public void OnAttackCollision_Enemy() // 애니메이션 책갈피에서 호출
    {
        attackCollision_Enemy.SetActive(true);  // 공격지점 활성화
        //AudioManager.instance.PlaySE(PlayerAttack_Sound); // 공격 사운드 출력

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


    IEnumerator DamageColor()
    {
        mesh.material.color = new Vector4(0.9622642f, 0.3948914f, 0.9108856f, 1f);

        yield return new WaitForSeconds(0.1f);

        mesh.material.color = Color.white;
    }

    public void Attack_MeteoStart() //애니메이션에서 호출
    {

        StartCoroutine(Attack_MeteoCoroutine());

    }

    IEnumerator Attack_MeteoCoroutine()
    {
        GameObject meteo = Instantiate(meteoPref, meteoPort.position, meteoPort.rotation);

        yield return new WaitForSeconds(1f);

        meteo = Instantiate(meteoPref, meteoPort.position, meteoPort.rotation);

        yield return new WaitForSeconds(1f);

        meteo = Instantiate(meteoPref, meteoPort.position, meteoPort.rotation);

        yield return new WaitForSeconds(1f);

    }

    #endregion
}