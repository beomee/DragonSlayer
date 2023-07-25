using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_ : MonoBehaviour
{
    public Transform player;
    public GameManager gm;
    public GameObject storeUi;
    PlayerMove playermove;
    //TestBackCamera3 camera3;  => 씨네머신으로 다시 쓰지 않을 때 사용 할 변수 
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
    private string OpenStore; // 상점이 열릴 때 나오는 사운드를 담을 변수


    // Start is called before the first frame update
    void Start()
    {
        //camera3 = GetComponent<Cinemachine.CinemachineFreeLook>();
        
        //camera3 = player.GetComponentInChildren<TestBackCamera3>();  => 씨네머신으로 다시 쓰지 않을 때 사용할 코드 
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
        if (Player.instance.isStore == true) // 상점이 열렸는데, 또 열리지 않게 하기 위함.
        {
            InteractionUi.SetActive(true);
            TryOpenStore();
           
        }

        if (Vector3.Distance(transform.position, player.position) > 3) // 플레이어와 거리가 멀어지면 안내문구 ui가 사라짐.
        {
           InteractionUi.SetActive(false);
        }

    }

    private void TryOpenStore()  // 상점 ui 여는 함수 
    {

       if (Input.GetKeyDown(KeyCode.G)) // g키를 누르면 
       {
          OpenStoreImg();
       }



    }
    private void OpenStoreImg() //상점 이미지 열기 
    {
        isShopOpenUi = true;
        Cursor.visible = true;   // 마우스 활용가능 
        Cursor.lockState = CursorLockMode.Confined;

        AudioManager.instance.PlaySE(OpenStore,1,1);   // 상점 열리는 사운드 출력 



        InteractionUi.SetActive(false); // 안내문구 ui 사라짐

        for (int i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].itemCount = 1; // 상점의 아이템 개수 1개로 초기화
            shopSlots[i].itemCountTxt.text = shopSlots[i].itemCount.ToString(); // 위에 내용을 텍스트로 변환 
            shopSlots[i].buyForNeedGold = shopSlots[i].itemCount * shopSlots[i].itemSell.itemPrice; // 상점에 표시될 아이템의 가격
            shopSlots[i].buyForNeedGoldTxt.text = shopSlots[i].buyForNeedGold.ToString(); // 위의 내용을 텍스트로 변환 

        }

        storeUi.SetActive(true); // 상점 ui 열기
        inventoryUi.SetActive(true); // 인벤토리 ui 열기
        freeLook.m_XAxis.m_MaxSpeed = 0;


        //anim.enabled = false; // 애니메이터 작동 중단 
        //playermove.enabled = false; // 플레이어 기능 중단  

        //camera3.enabled = false; // 카메라 기능 중단 

    }

    public void CloseStoreImg() // esc키를 누르거나 나가기 버튼을 눌렀을 때 호출 
    {
        isShopOpenUi = false;
        freeLook.m_XAxis.m_MaxSpeed = 150;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        storeUi.SetActive(false);
        InteractionUi.SetActive(false);


        //player.GetComponent<Player>().enabled = true;
        //anim.enabled = true;
        //playermove.enabled = true; // 플레이어 기능 중단  
     
       


    }
}









