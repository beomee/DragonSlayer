//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;


//public class SkillManager : MonoBehaviour
//{
//    Animator anim;
//    NavMeshAgent agent;
//    Rigidbody rigid;
//    float coolTime; // 전체 쿨타임
//    float currentCoolTime; // 전체 쿨타임

//    Enemy_RedDragon redDragon;
   

//    [SerializeField]
//    private string Attack1_Sound;

//    List<Skill> _skillList = new List<Skill>();

//    private void Awake()
//    {
//        redDragon = GetComponent<Enemy_RedDragon>();
//        anim = GetComponent<Animator>();
//        agent = GetComponent<NavMeshAgent>();
//        anim = GetComponent<Animator>();
//        rigid = GetComponent<Rigidbody>();
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        //전투시작

//        BiteSkill biteSkill = new BiteSkill(3.0f, 6, anim, "bite");
//        JumpAttackSkill jumpAttackSkill = new JumpAttackSkill(5.0f, 5, anim);
//        ShoutingAttackSkill shoutingAttackSkill = new ShoutingAttackSkill(10.0f, 4, anim);
//        BackJumpSkill backJumpSkill = new BackJumpSkill(10.0f, 3, anim, rigid, agent, this.transform);
//        BreatheAttackSkill breatheAttackSkill = new BreatheAttackSkill(10.0f, 2, anim);
//        MeteoAttackSkill meteoAttackSkill = new MeteoAttackSkill(15.0f, 1, anim);

//        _skillList.Add(biteSkill as Skill); // Skill에 상속되어있는 biteSkill를 skillList에 저장! => (Skill)biteSkill의 모양으로도 쓸 수 있음.  
//        _skillList.Add(jumpAttackSkill as Skill);
//        _skillList.Add(shoutingAttackSkill as Skill);
//        _skillList.Add(backJumpSkill as Skill);
//        _skillList.Add(breatheAttackSkill as Skill);
//        _skillList.Add(meteoAttackSkill as Skill);


//    }

//    // Update is called once per frame
//    void Update()
//    {
        


//    }


//    void StartBiteAttack()
//    {
//        // 공격 애니메이션
//        anim.SetTrigger("attack");
//        AudioManager.instance.PlaySE(Attack1_Sound);

//        // 
//        Skill skill = redDragon.ReturnSelectedSkill();

//        float mycoolTime = skill.coolTime;
//        Animator myanim = skill.anim;
//        string hash = skill.hashTag;

//        anim.SetTrigger(hash);
//    }





//    IEnumerator WaitOneSecond()
//    {
//        yield return new WaitForSeconds(1.0f);
//    }

//    IEnumerator WaitJumpTime()
//    {
//        yield return new WaitForSeconds(0.2f);
//    }






//}
