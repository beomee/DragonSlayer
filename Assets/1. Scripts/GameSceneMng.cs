using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameSceneMng : MonoBehaviour
{

    //public Button StartBtn;  // 처음시작
    public Button ExitBtn;   // 게임종료
    //public Button LoadBtn;   // 이어하기
    public Button ExplainBtn;// 설명
    public GameObject ExplainUi;     
    public Button QuitBtn;   // 창 닫기
    public GameObject storeImg;
    public Cinemachine.CinemachineBrain camera3;
    public Inventory invectory;
    public ViewMyStatus myStatus;

    public GameObject player;
    public GameObject talkImg;
    //public GameObject inventoryUi;
    public Animator anim;
    public GameObject settingImg;
    public GameObject InteractionUi;
    public GameObject itamBuyFailImg;
    public GameObject saveCompleteImg;
    public Slider changeBgm;
    public Slider changeEffect;
    public Store_ store1;
    public Npc npc;

    public Cinemachine.CinemachineFreeLook freeLook;

    public GameManager gm;

    [SerializeField]
    private string openUi; // 플레이어가 아이템을 줍는 소리 

    public bool isSettingUi = false;

    // 설정창이 활성화되면 true가 될텐데, 그때는 캐릭터 못움직이게 
    //public static bool settingActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        freeLook.m_XAxis.m_MaxSpeed = 150;

        ExitBtn.onClick.AddListener(ClickExit);

        ExplainBtn.onClick.AddListener(ClickExplain);

        QuitBtn.onClick.AddListener(ClickQuit);

        anim = player.GetComponent<Animator>();

        //camera3.GetComponent<TestBackCamera3>();
        player.GetComponent<GameObject>();
        talkImg.GetComponent<GameObject>();
       
    }

    public void ChangeBgmSlider()
    {
        AudioManager.instance.audioSourceBgm.volume = changeBgm.value;
    }

    public void ChangeEffectSlider()
    {

        AudioManager.instance.audioSourceEffects[0].volume = changeEffect.value;
        AudioManager.instance.audioSourceEffects[1].volume = changeEffect.value;
        AudioManager.instance.audioSourceEffects[2].volume = changeEffect.value;
        AudioManager.instance.audioSourceEffects[3].volume = changeEffect.value;
        AudioManager.instance.audioSourceEffects[4].volume = changeEffect.value;

    }

    public void ClickStart()
    {
        // 스토리씬으로 전환하는 기능
        SceneManager.LoadScene("2.StoryScene");

    }

    public void StartEndingScene()
    {
        SceneManager.LoadScene("4.EndingScene");
    }

    public void ClickExplain()
    {
        ExplainUi.SetActive(true);
    }

    
    public void ClickExit()
    {

        // 게임 종료 
#if UNITY_EDITOR // 에디터 일경우 

        // 에디터에서만 사용 가능한 것
        UnityEditor.EditorApplication.isPlaying = false;

#else
        // 빌드파일에서만 사용 가능한 것 
  Application.Quit();

#endif



    }

    public void ClickLoad()
    {
        // 플레이씬으로 전환하는 기능
        SceneManager.LoadScene("3.GameScene");
     
    }

    // 나가기 버튼을 눌렀을때(X모양)
    public void ClickQuit()
    {
        //player.GetComponent<Player>().isNpc = false;
        //player.GetComponent<Player>().isStore = false;
        //InteractionUi.gameObject.SetActive(false);
        //storeImg.gameObject.SetActive(false);
        //inventoryUi.SetActive(false);
        //player.GetComponent<Player>().enabled = true;

        // 애니메이션 정상적으로 기능하도록 하기
        //anim.enabled = true;


        //// 카메라 속도 원상복구
        //freeLook.m_XAxis.m_MaxSpeed = 150;

        //// 플레이어의 playerMove 스크립트를 다시 재가동되도록 
        //player.GetComponent<PlayerMove>().enabled = true;
        //// 카메라 스크립트를 다시 재가동되도록 
        //camera3.enabled = true;

        //Cursor.visible = false;  // 마우스 커서 안보이게 하기 (이동할 건 다하고, 보이지만 않게) 
        //Cursor.lockState = CursorLockMode.Locked;

        // 월드맵 UI 끄기 
        //gm.CloseFullmap(); // 월드맵 UI 끄기 

        // 설정 UI 끄기
        CloseSetting();

        // 인벤토리 UI끄기 
        invectory.CloseInventory();

        // 상점 UI끄기
        store1.CloseStoreImg();

        // NPC 대화ui 끄기 
        npc.CloseNpcTalk();


    }

    // ESC키를 눌렀을 때 
    public void ExitKey()
    {
       

        //if (gm.isWorldMapOpen == true)
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        // 월드맵 UI 끄기 
        //        gm.CloseFullmap(); // 월드맵 UI 끄기 
        //    }
        //}

        if (invectory.InvenUiOpen == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // 인벤토리 UI끄기 
                invectory.CloseInventory();
            }
        }

        if (store1.isShopOpenUi == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // 상점 UI끄기
                store1.CloseStoreImg();
                invectory.CloseInventory();
            }
        }

        if (npc.isNpcUi == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // NPC 대화ui 끄기 
                npc.CloseNpcTalk();
            }
        }

        if (myStatus.statusUi == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // 내 스텟창 끄기
                myStatus.CloseStatus();
            }
        }


        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
          
        //  //player.GetComponent<Player>().isNpc = false;
        //  //player.GetComponent<Player>().isStore = false;
        //  //// 플레이어의 playerMove 스크립트를 다시 재가동되도록 
        //  //player.GetComponent<PlayerMove>().enabled = true;

        //    // 상호작용 ui끄기
        //    //InteractionUi.gameObject.SetActive(false);

        //  // 상점 ui끄기
        //  //storeImg.gameObject.SetActive(false);

        //  //player.GetComponent<Player>().enabled = true;
        //  //  // 애니메이션 정상적으로 기능하도록 하기

        //  //  anim.enabled = true;
          

        //  //// NPC 대화창 끄기
        //  //talkImg.gameObject.SetActive(false);  
          

          


        //  // 카메라 스크립트를 다시 재가동되도록 
        //  camera3.enabled = true;

        //  Cursor.visible = false;  // 마우스 커서 안보이게 하기 (이동할 건 다하고, 보이지만 않게) 
        //  Cursor.lockState = CursorLockMode.Locked;

        //    // 설명글 UI끄기
        //    ClickExplainQuit();
        //    // 저장완료 UI끄기
        //    SaveCompleteQuit();
        //    // 아이템 구매실패 UI끄기
        //    ClickitamBuyFailImgQuit();
        //    // 카메라 속도 150으로 원상복구
        //    freeLook.m_XAxis.m_MaxSpeed = 150;

        //    // 월드맵 UI 끄기 
        //    gm.CloseFullmap(); // 월드맵 UI 끄기 

        //    // 설정 UI 끄기
        //    CloseSetting();

        //    // 인벤토리 UI 끄기
        //    invectory.CloseInventory();

        //}
    }
    

    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    /// <summary>
    /// 
    /// 
    /// </summary>

    public void ClickExplainQuit()
    {
        ExplainUi.SetActive(false);
    }

    public void SaveCompleteQuit()
    {
       saveCompleteImg.SetActive(false);
    }

    public void ClickitamBuyFailImgQuit()
    {
        itamBuyFailImg.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

        TryOpenSettingImg();
        ExitKey();
    }


    private void TryOpenSettingImg()
    {
        if (isSettingUi == false && store1.isShopOpenUi == false && myStatus.statusUi == false && npc.isNpcUi == false && invectory.InvenUiOpen == false /*&& gm.isWorldMapOpen == false*/) 
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OpenSetting();
            }
        }

        else //isSettingUi가 true인 경우에는
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseSetting();
            }
        }


    }

    private void OpenSetting()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        freeLook.m_XAxis.m_MaxSpeed = 0;
        settingImg.SetActive(true);
        AudioManager.instance.PlaySE(openUi, 1, 1);
        isSettingUi = true;
    }

    public void CloseSetting()
    {
        freeLook.m_XAxis.m_MaxSpeed = 150;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ExplainUi.SetActive(false);
        saveCompleteImg.SetActive(false);
        settingImg.SetActive(false);
        isSettingUi = false;

    }
}
