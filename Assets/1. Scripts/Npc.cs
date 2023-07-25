using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{


    public Transform player;
    public GameObject InteractionUi;
    public GameManager gm;
    public GameObject scanObject;  // npc 대화 가져오기
    public Cinemachine.CinemachineBrain camera3;
    public Cinemachine.CinemachineFreeLook freeLook;
    public GameObject talkPanel;  // 말풍선배경 ui 담는 변수
    public PlayerMove playermove;
    public Animator anim;

    public bool isNpcUi = false;

    // Start is called before the first frame update
    void Start()
    {
      
        playermove = player.GetComponent<PlayerMove>();
        anim = player.GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.instance.isConnect = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.isNpc == true)
        {
            TryNpcTalk();
            InteractionUi.SetActive(true);
        }

        if (Vector3.Distance(transform.position, player.position) > 4)
        {
            InteractionUi.SetActive(false);
        }

    }




    private void TryNpcTalk()
    {
      
        if (Input.GetKeyDown(KeyCode.G))
        {

            gm.Action(scanObject);     // NPC대화 가져오기.
            OpenNpcTalk();
        }

      

    }

    private void OpenNpcTalk()
    {
        isNpcUi = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        InteractionUi.SetActive(false); // 상호작용 안내 메세지 창 닫기

        talkPanel.SetActive(true); // 말풍선배경UI 켜기
        freeLook.m_XAxis.m_MaxSpeed = 0;

        //Player.instance.isNpc = false;  // 다른 ui가 켜지기 않도록 바꾸어 주는 조건

        //Player.instance.enabled = false;
       
        //anim.enabled = false; // 플레이어의 애니메이션 기능 중단 

        //playermove.enabled = false; // 플레이어가 움직이는 기능 중단  

        //camera3.enabled = false; // 카메라 기능 

    }


    public void CloseNpcTalk() // esc키를 누르거나 나가기 버튼을 눌렀을 때 호출 
    {
        isNpcUi = false;
        freeLook.m_XAxis.m_MaxSpeed = 150;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        talkPanel.SetActive(false); // 말풍선배경UI 켜기
    }
}
