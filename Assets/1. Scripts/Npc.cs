using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{


    public Transform player;
    public GameObject InteractionUi;
    public GameManager gm;
    public GameObject scanObject;  // npc ��ȭ ��������
    public Cinemachine.CinemachineBrain camera3;
    public Cinemachine.CinemachineFreeLook freeLook;
    public GameObject talkPanel;  // ��ǳ����� ui ��� ����
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

            gm.Action(scanObject);     // NPC��ȭ ��������.
            OpenNpcTalk();
        }

      

    }

    private void OpenNpcTalk()
    {
        isNpcUi = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        InteractionUi.SetActive(false); // ��ȣ�ۿ� �ȳ� �޼��� â �ݱ�

        talkPanel.SetActive(true); // ��ǳ�����UI �ѱ�
        freeLook.m_XAxis.m_MaxSpeed = 0;

        //Player.instance.isNpc = false;  // �ٸ� ui�� ������ �ʵ��� �ٲپ� �ִ� ����

        //Player.instance.enabled = false;
       
        //anim.enabled = false; // �÷��̾��� �ִϸ��̼� ��� �ߴ� 

        //playermove.enabled = false; // �÷��̾ �����̴� ��� �ߴ�  

        //camera3.enabled = false; // ī�޶� ��� 

    }


    public void CloseNpcTalk() // escŰ�� �����ų� ������ ��ư�� ������ �� ȣ�� 
    {
        isNpcUi = false;
        freeLook.m_XAxis.m_MaxSpeed = 150;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        talkPanel.SetActive(false); // ��ǳ�����UI �ѱ�
    }
}
