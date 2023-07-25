using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_ : MonoBehaviour
{
    public Transform player;
    public GameManager gm;
    public GameObject storeUi;
    PlayerMove playermove;
    //TestBackCamera3 camera3;  => ���׸ӽ����� �ٽ� ���� ���� �� ��� �� ���� 
    //public Cinemachine.CinemachineFreeLook camera3;
    public Cinemachine.CinemachineBrain camera3;
    public Cinemachine.CinemachineFreeLook freeLook;
    public GameObject InteractionUi;
    public GameObject inventoryUi;
    Animator anim;
    public ShopSlot[] shopSlots;

    public bool isConnect = false;
    public bool isShopOpenUi = false;

    [SerializeField]
    private string OpenStore; // ������ ���� �� ������ ���带 ���� ����


    // Start is called before the first frame update
    void Start()
    {
        //camera3 = GetComponent<Cinemachine.CinemachineFreeLook>();
        
        //camera3 = player.GetComponentInChildren<TestBackCamera3>();  => ���׸ӽ����� �ٽ� ���� ���� �� ����� �ڵ� 
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
        if (Player.instance.isStore == true) // ������ ���ȴµ�, �� ������ �ʰ� �ϱ� ����.
        {
            InteractionUi.SetActive(true);
            TryOpenStore();
           
        }

        if (Vector3.Distance(transform.position, player.position) > 3) // �÷��̾�� �Ÿ��� �־����� �ȳ����� ui�� �����.
        {
           InteractionUi.SetActive(false);
        }

    }

    private void TryOpenStore()  // ���� ui ���� �Լ� 
    {

       if (Input.GetKeyDown(KeyCode.G)) // gŰ�� ������ 
       {
          OpenStoreImg();
       }



    }
    private void OpenStoreImg() //���� �̹��� ���� 
    {
        isShopOpenUi = true;
        Cursor.visible = true;   // ���콺 Ȱ�밡�� 
        Cursor.lockState = CursorLockMode.Confined;

        AudioManager.instance.PlaySE(OpenStore,1,1);   // ���� ������ ���� ��� 



        InteractionUi.SetActive(false); // �ȳ����� ui �����

        for (int i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].itemCount = 1; // ������ ������ ���� 1���� �ʱ�ȭ
            shopSlots[i].itemCountTxt.text = shopSlots[i].itemCount.ToString(); // ���� ������ �ؽ�Ʈ�� ��ȯ 
            shopSlots[i].buyForNeedGold = shopSlots[i].itemCount * shopSlots[i].itemSell.itemPrice; // ������ ǥ�õ� �������� ����
            shopSlots[i].buyForNeedGoldTxt.text = shopSlots[i].buyForNeedGold.ToString(); // ���� ������ �ؽ�Ʈ�� ��ȯ 

        }

        storeUi.SetActive(true); // ���� ui ����
        inventoryUi.SetActive(true); // �κ��丮 ui ����
        freeLook.m_XAxis.m_MaxSpeed = 0;


        //anim.enabled = false; // �ִϸ����� �۵� �ߴ� 
        //playermove.enabled = false; // �÷��̾� ��� �ߴ�  

        //camera3.enabled = false; // ī�޶� ��� �ߴ� 

    }

    public void CloseStoreImg() // escŰ�� �����ų� ������ ��ư�� ������ �� ȣ�� 
    {
        isShopOpenUi = false;
        freeLook.m_XAxis.m_MaxSpeed = 150;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        storeUi.SetActive(false);
        InteractionUi.SetActive(false);


        //player.GetComponent<Player>().enabled = true;
        //anim.enabled = true;
        //playermove.enabled = true; // �÷��̾� ��� �ߴ�  
     
       


    }
}









