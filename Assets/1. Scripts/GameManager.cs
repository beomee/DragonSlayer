using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{

    public Transform[] EnemyRespawnPoints_Purple; // ���� ������ ��ġ�� ���� �迭
    public Transform[] EnemyRespawnPoints_Grey; // ���� ������ ��ġ�� ���� �迭
    public Transform[] EnemyRespawnPoints_Brown; // ���� ������ ��ġ�� ���� �迭
    public TalkManager talkManager; // ��ũ�Ŵ����� �������� ���� ����.
    public FadeEffect fadeEffect;
    //public CameraShake cameraShake;
    public GameObject deathMessagePanel; // �׾��� �� �޼����� ��Ÿ���� ���ӿ�����Ʈ 
    //public Camera3 camera;
    public Transform PlayerRespawnPoint; // �÷��̾ ������ �� ��ġ�� ���� �迭 
    public GameObject Player; // �÷��̾� ���ӿ�����Ʈ�� ���� ����
    public Slider hpbar;
    public Cinemachine.CinemachineBrain camera3;

    public Cinemachine.CinemachineFreeLook freeLook;


    //// �� ĳ���� ���ӿ�����Ʈ�� ������ ���� 
    //public GameObject Enemy_PurpleDragon;
    //public GameObject Enemy_GreyDragon;
    //public GameObject Enemy_BrownDragon;
    //public GameObject Enemy_RedDragon;

    // �� ĳ������ ������ �ֱ� 
    public float enemyCreateTime = 3.0f;

    // �� ĳ������ �ִ� ���� ����
    public int maxEnemy;
    public int talkIndex;
    public bool isAction;  // ��ȭâ�� ���� ����üũ�� �ϴ� ���� 
    public bool isWorldMapOpen = false;


    // ���� ���� ���θ� �Ǵ��� ����
    public bool isGameOver = false;

    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public GameObject fullMapCamera;


    // �ʿ��� ���� �̸�
    [SerializeField]
    private string openMapSound;


    // �κ��丮�� Ȱ��ȭ�Ǹ� true�� ���ٵ�, �׶��� ĳ���� �������̰� 
    //public static bool fullCameraActivated = false;

    //�ܺο� �ִ� �÷��̾� ���ӿ�����Ʈ�� ���������� ã�´�.


    // Start is called before the first frame update

    private void Start()
    {
        //EnemyRespawnPoints_Purple = GameObject.Find("SpawnPoint_PurpleDragon").GetComponentsInChildren<Transform>(); // ��������Ʈ�׷��� �ڽ��� ��ġ���� ����
        //EnemyRespawnPoints_Grey = GameObject.Find("SpawnPoint_GreyDragon").GetComponentsInChildren<Transform>(); // ��������Ʈ�׷��� �ڽ��� ��ġ���� ����
        //EnemyRespawnPoints_Brown = GameObject.Find("SpawnPoint_BrownDragon").GetComponentsInChildren<Transform>(); // ��������Ʈ�׷��� �ڽ��� ��ġ���� ����


        //StartCoroutine(this.CreateEnemy_Purple());
        //StartCoroutine(this.CreateEnemy_Grey());
        //StartCoroutine(this.CreateEnemy_Brown());


    }

    IEnumerator CreateEnemy_Purple()
    {
        //maxEnemy ������ŭ�� �ݺ��ϵ��� 
        yield return new WaitForSeconds(2f); // 2�ʰ� ���� �ڿ�

        for (int i = 0; i < EnemyRespawnPoints_Purple.Length; i++)
        {
            Instantiate(Enemy_PurpleDragon.instance, EnemyRespawnPoints_Purple[i]); // ���� �����ϴ� �ڵ� 
            yield return new WaitForSeconds(enemyCreateTime); // enemyCreateTime �� �ڿ� �ٽ� ���Ͱ� ����
        }

    }

    IEnumerator CreateEnemy_Grey()
    {
        //maxEnemy ������ŭ�� �ݺ��ϵ��� 
        yield return new WaitForSeconds(2f); // 2�ʰ� ���� �ڿ�

        for (int i = 0; i < EnemyRespawnPoints_Grey.Length; i++)
        {
            Instantiate(Enemy_GrayDragon.instance, EnemyRespawnPoints_Grey[i]); // ���� �����ϴ� �ڵ� 
            yield return new WaitForSeconds(enemyCreateTime); // enemyCreateTime �� �ڿ� �ٽ� ���Ͱ� ����
        }

    }

    IEnumerator CreateEnemy_Brown()
    {
        //maxEnemy ������ŭ�� �ݺ��ϵ��� 
        yield return new WaitForSeconds(2f); // 2�ʰ� ���� �ڿ�

        for (int i = 0; i < EnemyRespawnPoints_Brown.Length; i++)
        {
            Instantiate(Enemy_BrownDragon.instance, EnemyRespawnPoints_Brown[i]); // ���� �����ϴ� �ڵ� 
            yield return new WaitForSeconds(enemyCreateTime); // enemyCreateTime �� �ڿ� �ٽ� ���Ͱ� ����
        }

    }


    public void EnemyDead(GameObject enemy)  // �׾����� ȣ�� 
    {
        StartCoroutine(RespawnEnemy(enemy));
    }


    public IEnumerator RespawnEnemy(GameObject enemy) // ���� �������Ǵ� �ڵ�
    {

        yield return new WaitForSeconds(4f);

        GameObject instanceEnemy = Instantiate(enemy, enemy.transform.parent); // ���� �����ϴ� �ڵ� 


        instanceEnemy.gameObject.SetActive(false);

        Destroy(enemy);

        yield return new WaitForSeconds(7f);

        instanceEnemy.gameObject.SetActive(true);

    }


    public IEnumerator RespawnPlayer()
    {

        yield return new WaitForSeconds(4.5f); // 4.5�ʵڿ� 

        Player.transform.position = PlayerRespawnPoint.position; // ��������ҿ� �÷��̾� ��Ƴ� 

        Player.GetComponent<Player>().anim.SetTrigger("isRespawn"); // �÷��̾� �ִϸ��̼� Idle ���·� ���󺹱�



        yield return new WaitForSeconds(3f);   // 3�� �ڿ� 

        StartCoroutine(fadeEffect.Fade(1, 0));   // ������ ȭ�� ����������� 


        if (Enemy_RedDragon.instance.enemyhp > 0)
        {
            Enemy_RedDragon.instance.enabled = true;
        }


        if (Enemy_GrayDragon.instance.enemyhp > 0)
        {
            Enemy_GrayDragon.instance.enabled = true;
        }



        if (Enemy_BrownDragon.instance.enemyhp > 0)
        {
            Enemy_BrownDragon.instance.enabled = true;
        }



        if (Enemy_PurpleDragon.instance.enemyhp > 0)
        {
            Enemy_PurpleDragon.instance.enabled = true;
        }




        deathMessagePanel.gameObject.SetActive(false); // �׾����� ������ �޼��� �ٽ� ������ �ʵ��� ��.

        Player.GetComponent<CharacterController>().enabled = true; // �÷��̾� ĳ������Ʈ�ѷ� Ȱ��ȭ

        Player.GetComponent<PlayerMove>().enabled = true; // �÷��̾� �̵� ��� �ٽ� ���

        Player.GetComponent<Player>().enabled = true; // �÷��̾� ��� �ٽ� ��� 


        camera3.enabled = true;

        Json.instance.data.hp = Json.instance.data.maxhp;  // �ִ� ü���� ���� ���� Hp��ŭ ȸ�� ������ 

        hpbar.value = Json.instance.data.maxhp;  // �ִ� ü���� ���� HP�ٿ� ����

    }



    // Update is called once per frame
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);

    }

    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;  //talkdata�� null �� ��쿡�� return���� �Լ� ���������� 
        }

        if (isNpc)
        {
            talkText.text = talkData;
        }

        else
        {
            talkText.text = talkData;

        }

        isAction = true;
        talkIndex++; // �� ���������� �������� ���ؼ� �ۼ�.
    }

    public void ActionEnd(GameObject scanObj)
    {
        talkPanel.SetActive(false);

    }

    private void Update()
    {

        //TryOpenFullmap();

        if (Json.instance.data.hp <= 0)
        {
            deathMessagePanel.gameObject.SetActive(true);
        }

    }

    void TryOpenFullmap()
    {
        if (isWorldMapOpen == false)
        { 
            if (Input.GetKeyDown(KeyCode.Tab))
            {
              OpenFullmap();
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                CloseFullmap();
            }
          
        }
    }
    void OpenFullmap()
    {
        Cursor.visible = true;  // ���콺 Ŀ�� ���̰� �ϱ�
        Cursor.lockState = CursorLockMode.Confined;
        freeLook.m_XAxis.m_MaxSpeed = 0;
        fullMapCamera.SetActive(true);
        AudioManager.instance.PlaySE(openMapSound,1,1);
        isWorldMapOpen = true;
    }

    public void CloseFullmap()
    {
        freeLook.m_XAxis.m_MaxSpeed = 150;
        Cursor.visible = false;  // ���콺 Ŀ�� �Ⱥ��̰� �ϱ� (�̵��� �� ���ϰ�, �������� �ʰ�) 
        Cursor.lockState = CursorLockMode.Locked;
        fullMapCamera.SetActive(false);
        isWorldMapOpen = false;
    }


    
}
