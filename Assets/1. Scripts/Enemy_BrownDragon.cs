using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

class BiteSkill_Brown : Skill
{


    public BiteSkill_Brown(float coolTime, int priority, Animator anim) : base(coolTime, priority, anim) // ������ �޼ҵ�
    {
        // �� ���� �ܿ� �߰� �� ������ ������ ���⿡ �߰� 
    }

    public override void Attack() // ����
    {
        // ���� �ִϸ��̼�
        anim.SetTrigger("attack");

    }
}

class HandAttackSkill_Brown : Skill
{

    public HandAttackSkill_Brown(float coolTime, int priority, Animator anim) : base(coolTime, priority, anim) // ������ �޼ҵ�
    {
        // �� ���� �ܿ� �߰� �� ������ ������ ���⿡ �߰� 
    }

    public override void Attack() // ����
    {
        anim.SetTrigger("attack2");
    }

}

class HeadAttackSkill_Brown : Skill
{


    public HeadAttackSkill_Brown(float coolTime, int priority, Animator anim) : base(coolTime, priority, anim) // ������ �޼ҵ�
    {
        // �� ���� �ܿ� �߰� �� ������ ������ ���⿡ �߰� 
    }

    public override void Attack() // ����
    {
        anim.SetTrigger("attack3");
    }
}


public class Enemy_BrownDragon : MonoBehaviour
{
    public float distance; // �÷��̾���� �Ÿ�
    public float enemyhp; // ���� ü��
    public float enemystr;

    public GameObject attackCollision_BrownEnemy; // ������ ���� �ݶ��̴��� ���� ���� 
    public GameObject attackCollision_BrownEnemy_Hand; // �ķ�ġ�� �����ݶ��̴��� ���� ����
    public GameObject attackCollision_BrownEnemy_Head; // ���̹ޱ� �����ݶ��̴��� ���� ���� 

    public Collider bossStart;

    float speed = 3f; // ���Ͱ� ȸ���ϴ� �ӵ��� ��Ʈ���ϴ� ���� 
    public Rigidbody rigid;
    Animator anim;
    public NavMeshAgent agent;
    public Slider EnemySlider;

    bool isAttacking = false; // ���� ���϶� ȸ���� ��Ʈ�� �� �� �ְԲ� �ϱ� ���� ���� / false = ������ �� �ִ� ����
    bool isJump = false;
    bool isAttack = false;
    bool isAttackStart = false;
    bool BackOriginBgm = false;

    AudioManager audioManager;
    SkinnedMeshRenderer mesh;

    public CapsuleCollider[] capColliders; // ���� ���������� ������������ ������ �ݶ��̴��� ��� ���� 
    public SphereCollider bodyCollider;


    // �ܺο� �ִ� �÷��̾� Ʈ������ �������� 
    public Transform player; // �÷��̾� ��������(�÷��̾��� ��ġ���� �����;� �ϴϱ�)

    bool startFightBgm = false; // walk ���¿��� ����bgm�� Ʋ������ true�� �ٲ��ֱ� ���� ����
    bool pazeTwoStart = false;

    [SerializeField]
    public GameObject goldPref; // ��� ������ ���� ����
    [SerializeField]
    public GameObject hpHealingPotionPref; // ü��ȸ������ ������ ���� ����

    [SerializeField]
    public GameObject criticalPref; // ũ��Ƽ�� ���� ��������  ���� ����

    [SerializeField]
    public GameObject maxHpPotionPref; // ü���������� ���� ������ ���� ����

    [SerializeField]
    public GameObject staminaPotion; // ���¹̳� ȸ�� ���� ������ ���� ����


    // �ʿ��� ���� �̸�
    [SerializeField]
    private string damaged_Sound;

    [SerializeField]
    private string Dead_Sound;

    [SerializeField]
    private string BrownEnemyAttack1_Sound_Bite; // ������

    [SerializeField]
    private string BrownEnemyAttack2_Sound_HandAttack; // �ķ�ġ��

    [SerializeField]
    private string BrownEnemyAttack3_Sound_HeadAttack; // ���̹ޱ� 

    [SerializeField]
    private string PhazeChangeSound_Brown;  // ������ ��ȯ

    [SerializeField]
    private string DropItemSound;  // ������ ���

    [SerializeField]
    private string BrownEnemyWalk_Sound;

    // �� ���¸� ��Ÿ���� �ڷ��� (������) 
    public enum EnemyStete
    {
        Idle,        // �⺻
        Walk,        // �̵�
        Attack,      // ����
        Dead         // ����
    }

    // ���� ���� ���� + ù ���´� �⺻ 
    public EnemyStete eState = EnemyStete.Idle;

    public static Enemy_BrownDragon instance;

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

        eState = EnemyStete.Idle;
        bodyCollider.enabled = true;

        BiteSkill_Brown biteSkill_Brown = new BiteSkill_Brown(1f, 3, anim);
        HandAttackSkill_Brown handAttackSkill_Brown = new HandAttackSkill_Brown(10f, 2, anim);

        skillList_Brown.Add(biteSkill_Brown as Skill);
        skillList_Brown.Add(handAttackSkill_Brown as Skill);

        #region
        //playerTransform = FindObjectOfType<Player>().transform;

        //// Ȱ��ȭ �Ǿ��ִ� Canvas�� ��������. 
        //hpBarSliderCanvas = FindObjectOfType<Canvas>();

        //EnemySlider = hpBarSliderCanvas.transform.GetChild(1).GetComponent<Slider>();

        //// ĸ�� �ݶ��̴��� �ٽ� ����
        //GetComponent<CapsuleCollider>().enabled = true;

        #endregion


    }



    // Update is called once per frame
    void Update()
    {

        // �극����ų�� ����°Ŵ� Start�� ������ ����ǰ�, �װ� �Ű������� �޾ƿͼ� ��ų����Ʈ�� �߰��� �ؾ���. 
        if (pazeTwoStart == false)
        {

            if (enemyhp < 1000.0f)
            {
                HeadAttackSkill_Brown headAttackSkill_Brown = new HeadAttackSkill_Brown(10.0f, 1, anim);
                skillList_Brown.Add(headAttackSkill_Brown);
                pazeTwoStart = true;
                anim.SetTrigger("PhazeChange");
            }

        }
        // �÷��̾���� �Ÿ� ���
        distance = Vector3.Distance(this.transform.position, player.position);

        // �̵��� ���� �ִϸ��̼� ��ȯ
        anim.SetFloat("speed", agent.velocity.magnitude); // ������ ũ�⸸ ������� magnitude


        // ���º��� �� �� ����
        switch (eState)
        {
            case EnemyStete.Idle: Idle(); break;
            case EnemyStete.Walk: Walk(); break;
            case EnemyStete.Attack:; break;
        }


        // ������ �Ÿ��� 20�̸��� �ɶ�
        if (distance < 25.0f)
        {
            // ���ʹ� hp�� �ѱ�
            EnemySlider.gameObject.SetActive(true);
        }

        // ������ �Ÿ��� 20�ʰ��� �ǰų�, hp�� 0 ���ϰ� �� ��
        if (distance > 25.0f || enemyhp <= 0)
        {
            // ���ʹ� hp�� ����
            EnemySlider.gameObject.SetActive(false);
        }

        // �������� �ƴҶ��� �÷��̾ ���� �ٶ󺸴ٰ� ������ �ϸ� ȸ���ϴ� ���� �����ϵ��� ��
        if (isAttacking == true)
        {
            Vector3 l_vector = player.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(l_vector), Time.deltaTime * speed);

        }

    }


    void ColliderOn()  // ���׿�&������ ��ȯ ���� �� ������ ǥ���ϱ� ���� �޼��� on : ��������  
    {
        capColliders[0].enabled = true;
        capColliders[1].enabled = true;
        capColliders[2].enabled = true;
    }

    void ColliderOff()  // ���׿�&������ ���� �� ������ ǥ���ϱ� ���� �޼��� off : ����  
    {
        capColliders[0].enabled = false;
        capColliders[1].enabled = false;
        capColliders[2].enabled = false;
    }


    // ���� �۾� 

    void BrownEnemySoundStart_Bite()
    {
        AudioManager.instance.PlaySE(BrownEnemyAttack1_Sound_Bite, 1,1);
    }

    void BrownEnemySoundStart_HandAttack()
    {
        AudioManager.instance.PlaySE(BrownEnemyAttack2_Sound_HandAttack, 1,1);
    }

    void BrownEnemySoundStart_HeadAttack() // ����� ���Ҷ� ���� ���¢�� �Ҹ��� ��� (��ũ�� ���߱� ���ؼ� Ư�� �ִϸ��̼ǿ��� ȣ��)
    {
        AudioManager.instance.PlaySE(BrownEnemyAttack3_Sound_HeadAttack, 1,1);
    }


    void PhazeChangeSoundStart() // ����� ���Ҷ� ���� ���¢�� �Ҹ��� ��� (��ũ�� ���߱� ���ؼ� Ư�� �ִϸ��̼ǿ��� ȣ��)
    {
        AudioManager.instance.PlaySE(PhazeChangeSound_Brown, 1,1);
    }

    void DropItemSoundStart()
    {
        AudioManager.instance.PlaySE(DropItemSound, 1, 1);
    }

    void BrownEnemySoundStart_Walk()
    {
        AudioManager.instance.PlaySE(BrownEnemyWalk_Sound, 1, 0.8f);
    }


    
    void EnemyFix() // �����ϴ� ���߿��� �׺���̼��� �ӵ��� 0���� ���� �������� �ʰ� �ϱ� ���� �޼���
    {
        agent.speed = 0.0f;
    }

    void EnemyMoveStart() // ������ �������� �ٽ� ������� �׺���̼��� �ӵ��� �༭ �÷��̾ �ָ� �ִ°�� ���󰡵��� �ϰ� �ϴ� �޼���.
    {
        agent.speed = 3.5f;
    }
    public void Damaged(float damage) // ���� ������ ȣ��

    {

        StartCoroutine(DamageColor()); // �ǰݽ� ���� ��ȭ

        //AudioManager.instance.PlaySE(damaged_Sound,1);

        enemyhp -= damage; //ü�� ����

        EnemySlider.value = enemyhp;

        //agent.isStopped = true;

        //agent.ResetPath();


        if (enemyhp <= 0)    // ���� hp�� 0���Ϸ� ������ ���.
        {

            anim.SetTrigger("dead"); // ���� �ִϸ��̼� ����

            Dead(); // �� ���� �Լ� ȣ��
        }

    }

    IEnumerator DamageColor()  // �ǰ� �� ������ �ٲ��ִ� �Լ� 
    {
        mesh.material.color = new Vector4(1f, 0.3349057f, 0.3349057f, 1f);

        yield return new WaitForSeconds(0.1f);

        mesh.material.color = Color.white;
    }


    void Dead() // �� ���� �Լ� 
    {
        isAttacking = false;
      
        bossStart.enabled = true;

        bodyCollider.enabled = false;

        AudioManager.instance.PlayStartMountainBgm();
        AudioManager.instance.PlaySE(Dead_Sound,1,1);

        // ĸ�� �ݶ��̴��� �����
        capColliders[0].enabled = false;
        capColliders[1].enabled = false;
        capColliders[2].enabled = false;


        StartCoroutine(EnemyDestroy());

        float GoldRandomCountDrop = Random.Range(1, 2);

        float HpPotionRandomDrop = Random.Range(1, 2);

        float criticalRandomDrop = Random.Range(1, 2);

        switch (GoldRandomCountDrop)
        {
            case 1:
                for (int i = 1; i < 11; i++)
                {
                    Instantiate(goldPref, transform.position + new Vector3(0,0.9f,0), Quaternion.identity);
                }

                break;
        }

        switch (HpPotionRandomDrop)
        {
            case 1:
                // �������� 1�� ���
                Instantiate(hpHealingPotionPref, transform.position, Quaternion.identity);
                // �ִ�ü������ ���� 1�� ���
                Instantiate(maxHpPotionPref, transform.position, Quaternion.identity);
                // ���¹̳� ���� 1�� ���
                Instantiate(staminaPotion, transform.position, Quaternion.identity);
                break;
        }

        switch (criticalRandomDrop)
        {
            case 1:         // ������ �� ���
                Instantiate(criticalPref, transform.position, Quaternion.identity);
                break;

        }

    }

    void Idle() // �⺻���� �� �� ���� 
    {
        BackOriginBgm = true;
        startFightBgm = false;

        // �� ã�� ���� + ����ʱ�ȭ�� ��Ʈ�� ���ִ°� ����
        // �� ã�� �߱�
        agent.isStopped = true;
        // ��� �ʱ�ȭ
        agent.ResetPath();

        // �÷��̾�� �Ÿ��� 20�ʰ� 30 ���϶��  => 20�����϶��� ���� => 20�ʰ� �� �� ������ �����.

        if (distance <= 25)
        {
            eState = EnemyStete.Walk; // �̵� ���·� ��ȯ 

        }

    }

    void Walk() // �̵����� �� �� ���� -> �����Ÿ� ������ �Ǹ� �����ϱ� + �־����� idle�� �ִ� ��
    {
        if (startFightBgm == false)
        {
            AudioManager.instance.PlayBossSceneBgm();
            startFightBgm = true;
        }


        agent.isStopped = false; // �� ã�� ����

        /*isAttacking = true;*/ // �÷��̾ ���ؼ� �ٶ󺸵��� �ϴ� �Լ� 
                            //�÷��̾ �������� �ΰ� ���󰡱� 
        agent.destination = player.position;


        // �÷��̾�� �Ÿ��� 60���� ũ�ٸ�
        if (distance > 25/* && isAttack == false*/)
        {
            // �⺻ ���·� ��ȯ
            eState = EnemyStete.Idle;

            if (BackOriginBgm == true)
            {
                AudioManager.instance.PlayStartMountainBgm();
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
        //// ���� �� ���� ������ ���� �ٶ󺸸鼭 ȸ���ϴ� �ڵ�
        //Vector3 l_vector = playerTransform.position - transform.position;
        //transform.rotation = Quaternion.LookRotation(l_vector).normalized;

        ////transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(l_vector), Time.deltaTime * speed);




        //// �÷��̾���� �Ÿ��� 7���� ũ�ٸ� 
        //if (distance > 6.5f)
        //{
        //    // �̵� ���·� ��ȯ
        //    eState = EnemyStete.Walk;

        //    // �׺���̼� �ڵ��̵� ���ߴ� �� false -> �ٽ� �����δ�
        //    agent.isStopped = false;
        //}

        //else
        //{

        //    attackcool += Time.deltaTime;

        //    // 2.5�� �ֱ�� �ߵ��ǰ� �ϴ� ������ �־��ֱ�
        //    if (attackcool >= 2.5f)
        //    {
        //        int randomAttack = Random.Range(1, 3); // 50���� Ȯ���� �ٸ� ������ �ϱ� ���� ���� 

        //        if (randomAttack == 1) // 25���� Ȯ���� Ư������ 
        //        {
        //            // ���� �ִϸ��̼�
        //            anim.SetTrigger("attack"); // ���� �ִϸ��̼� ����
        //            AudioManager.instance.PlaySE(BrownEnemyAttack1_Sound);
        //        }

        //        else if (randomAttack == 2) // 25���� Ȯ���� Ư������ 
        //        {
        //            // ���� �ִϸ��̼�
        //            anim.SetTrigger("attack2");

        //        }

        //        agent.isStopped = false;

        //        attackcool = 0;
        //    }

        //}

        #endregion
    }

    // �÷��̾��� �Ǹ� ��� �ڵ� : �ִϸ��̼��� å���� ��ɿ��� ���ϴ� Ÿ�ֿ̹� ȣ���.

    public void isAttackFalse()  // Animation�� Add event�� ȣ�� 
    {
        isAttack = false;  // ������ ���� ���� : ���ο� ������ ��� �� �� �ִ� ���� -> ������ ��ġ�� ���Ǵ� ���� ���� ����
    }

    public void isAttackTrue()  // Animation�� Add event�� ȣ�� 
    {
        isAttack = true;  // �������� ���� : ���ο� ������ ��� �� �� ���� ���� -> ������ ��ġ�� ���Ǵ� ���� ���� ����
    }

    public bool startCool = false;  // ��Ÿ�� �ʱ�ȭ ���� 

    public void SkillStart_Brown()  // SkillStart �ڷ�ƾ �Լ��� ȣ���ϴ� �Լ�
    {
        if (distance <= 25 && Json.instance.data.hp > 0.0f)
        {
            StartCoroutine(CoroutineSkillStart_Brown());

        }

        else
        {
            eState = EnemyStete.Walk;
            return;
        }


    }


    // ��ų Ŭ���� �ڷ����� skillList List�� ���������� ���Ӱ� ����.
    public List<Skill> skillList_Brown = new List<Skill>();

    // ���� ��ų�� �����ϴ� ����Ʈ
    public List<Skill> usedskillList_Brown = new List<Skill>();

    public IEnumerator CoroutineSkillStart_Brown() // ���������� üũ + �켱���� ���� ��ų ���� + ��� + �������� + List2�� �߰� + List2�� �߰��� ��ų ���� 
    {
        /**
         ��ų���� ��� �߿� �� �ٽ� ������ ����ϴ°��� �����ϱ� ���ؼ� ���!
         ���� Animation�� Add event���� isAttack�� false�� �������ִ� �Լ� ȣ��
         */
        //isAttack = true;



        if (skillList_Brown.Count > 0) // isAttack�� false �϶��� ��� ����  *isAttack = true : �������� ���� / false : ���� ���� �ƴѻ��� == ������ ���� ����
        {

            int pos_Brown = 0;
            int maxNumber = int.MaxValue; // ���� ���� ������ ��ȣ�� ���

            for (int j = 0; j < skillList_Brown.Count; j++) // ��ų����Ʈ�� ����� ���ڸ�ŭ �ݺ�
            {

                if (skillList_Brown[j].priority < maxNumber)  // priority�� ���� �۴ٸ� ��� 
                {

                    maxNumber = skillList_Brown[j].priority;
                    pos_Brown = j;

                }

            }

            yield return new WaitForSeconds(2f);  // �������ϰ��� �ð� ��

            Skill usedSkill_Brown = skillList_Brown[pos_Brown];// �켱������ ���� ���� ��ų�� ���������� ����

         


            usedSkill_Brown.Attack();  // �켱������ ���� ���� ��ų�� ����ؼ� ���� 
            

            usedskillList_Brown.Add(usedSkill_Brown); // ����� ��ų�� '���� ��ų ����Ʈ'�� �߰� �ϴ� �۾� 
      

            skillList_Brown.Remove(usedSkill_Brown); // �켱������ ���� ���ٰ� ���� ��ų�� '��ų ����Ʈ'���� ���� 
     


            startCool = true; // ��Ÿ���� ���ư��ٴ� ���� üũ�ϱ� ���� (true : ��ų�� �����ؼ� ��Ÿ���� ���ư����ִ� ���� / false : ��Ÿ���� 0�ʰ� �Ǿ �� ���ư��� �ִ� ����)

            StartCoroutine(coolManager(usedSkill_Brown));  // ��Ÿ���� ������ ,üũ�ϴ� �ڷ�ƾ �޼��� ȣ�� 
         
            //yield return new WaitUntil(() => !isAttack); // isAttack = false�� �ɶ����� ��ٷȴٰ� �Ʒ� �ڵ�� ����                                                                                 
        }

    }

    IEnumerator coolManager(Skill skill) // ���� ��ų�� ��Ÿ���� �����ͼ� 0���� �ٿ��ְ�, 0�� �� ��ų�� ������ ���� ���ִ� �ڷ�ƾ�Լ�
    {

        float skillAddCoolTime = 0.1f;

        // ��ų�� ����ߴٸ�, �� ��ų�� ���� ��Ÿ���� 0�� �ɶ����� ��� - ���ֱ� 
        while (skill.coolTime >= skillAddCoolTime)
        {
            skill.coolTime -= Time.deltaTime;  // ������Ƽ���� set���� ����� ���� ���� ���� �� �� ����.
                                               //print(usedSkill.coolTime);
                                               //Mathf.Min(usedSkill.coolTime = 0.0f); // 0.0f �� �����δ� �������� �ʰԲ� ����
            yield return null;
        }

        RefillSkill(skill, skill.originCoolTime);  // ��Ÿ���� 0�� ���� ���, �������ִ� �Լ� ȣ��

    }
    void RefillSkill(Skill refillSkill, float originCoolTimeValue) // ��Ÿ���� 0�� �� ������ List1�� �߰� + �߰��� ���� list2���� ���� ���ִ� �Լ�
    {
        refillSkill.coolTime = originCoolTimeValue;  // ���� �Ϸ��� ��ų�� ��Ÿ�ӿ� ���� ��Ÿ�� ���� �־��ֱ� 

        skillList_Brown.Add(refillSkill);  // ���� �Ϸ��� ��ų�� skillList�� �־��ֱ�

        usedskillList_Brown.Remove(refillSkill); // ����� ��ų�� ���� usedskillList���� ���� �� ��ų�� �������ֱ�.
    }

    // �����߿��� �� �ٸ� ������ �ϴ� ���� �����ϱ� ���� ���� 
    public void IsAttackingTrue() // ���� ���� ���� �� �� ����
    {
        isAttacking = true;
    }

    public void IsAttackingFalse() // �� �ٸ� ���� ���� 
    {
        isAttacking = false;
    }


    public void OnAttackCollision_BrownEnemy() // �ִϸ��̼� å���ǿ��� ȣ��
    {
        attackCollision_BrownEnemy.SetActive(true);  // �������� Ȱ��ȭ
        //AudioManager.instance.PlaySE(PlayerAttack_Sound); // ���� ���� ���

    }

    public void OnAttackCollision_BrownEnemy_HandAttack()
    {
        attackCollision_BrownEnemy_Hand.SetActive(true);
    }

    public void OnAttackCollision_BrownEnemy_HeadAttack()
    {
        attackCollision_BrownEnemy_Head.SetActive(true);
    }


    IEnumerator EnemyDestroy() // ���� �� ���͸� �����ϴ� �ڷ�ƾ �Լ� 
    {
        yield return new WaitForSeconds(3.5f);

        Destroy(this.gameObject);

    }

}

