using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameSceneMng : MonoBehaviour
{

    //public Button StartBtn;  // ó������
    public Button ExitBtn;   // ��������
    //public Button LoadBtn;   // �̾��ϱ�
    public Button ExplainBtn;// ����
    public GameObject ExplainUi;     
    public Button QuitBtn;   // â �ݱ�
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
    private string openUi; // �÷��̾ �������� �ݴ� �Ҹ� 

    public bool isSettingUi = false;

    // ����â�� Ȱ��ȭ�Ǹ� true�� ���ٵ�, �׶��� ĳ���� �������̰� 
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
        // ���丮������ ��ȯ�ϴ� ���
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

        // ���� ���� 
#if UNITY_EDITOR // ������ �ϰ�� 

        // �����Ϳ����� ��� ������ ��
        UnityEditor.EditorApplication.isPlaying = false;

#else
        // �������Ͽ����� ��� ������ �� 
  Application.Quit();

#endif



    }

    public void ClickLoad()
    {
        // �÷��̾����� ��ȯ�ϴ� ���
        SceneManager.LoadScene("3.GameScene");
     
    }

    // ������ ��ư�� ��������(X���)
    public void ClickQuit()
    {
        //player.GetComponent<Player>().isNpc = false;
        //player.GetComponent<Player>().isStore = false;
        //InteractionUi.gameObject.SetActive(false);
        //storeImg.gameObject.SetActive(false);
        //inventoryUi.SetActive(false);
        //player.GetComponent<Player>().enabled = true;

        // �ִϸ��̼� ���������� ����ϵ��� �ϱ�
        //anim.enabled = true;


        //// ī�޶� �ӵ� ���󺹱�
        //freeLook.m_XAxis.m_MaxSpeed = 150;

        //// �÷��̾��� playerMove ��ũ��Ʈ�� �ٽ� �簡���ǵ��� 
        //player.GetComponent<PlayerMove>().enabled = true;
        //// ī�޶� ��ũ��Ʈ�� �ٽ� �簡���ǵ��� 
        //camera3.enabled = true;

        //Cursor.visible = false;  // ���콺 Ŀ�� �Ⱥ��̰� �ϱ� (�̵��� �� ���ϰ�, �������� �ʰ�) 
        //Cursor.lockState = CursorLockMode.Locked;

        // ����� UI ���� 
        //gm.CloseFullmap(); // ����� UI ���� 

        // ���� UI ����
        CloseSetting();

        // �κ��丮 UI���� 
        invectory.CloseInventory();

        // ���� UI����
        store1.CloseStoreImg();

        // NPC ��ȭui ���� 
        npc.CloseNpcTalk();


    }

    // ESCŰ�� ������ �� 
    public void ExitKey()
    {
       

        //if (gm.isWorldMapOpen == true)
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        // ����� UI ���� 
        //        gm.CloseFullmap(); // ����� UI ���� 
        //    }
        //}

        if (invectory.InvenUiOpen == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // �κ��丮 UI���� 
                invectory.CloseInventory();
            }
        }

        if (store1.isShopOpenUi == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // ���� UI����
                store1.CloseStoreImg();
                invectory.CloseInventory();
            }
        }

        if (npc.isNpcUi == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // NPC ��ȭui ���� 
                npc.CloseNpcTalk();
            }
        }

        if (myStatus.statusUi == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // �� ����â ����
                myStatus.CloseStatus();
            }
        }


        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
          
        //  //player.GetComponent<Player>().isNpc = false;
        //  //player.GetComponent<Player>().isStore = false;
        //  //// �÷��̾��� playerMove ��ũ��Ʈ�� �ٽ� �簡���ǵ��� 
        //  //player.GetComponent<PlayerMove>().enabled = true;

        //    // ��ȣ�ۿ� ui����
        //    //InteractionUi.gameObject.SetActive(false);

        //  // ���� ui����
        //  //storeImg.gameObject.SetActive(false);

        //  //player.GetComponent<Player>().enabled = true;
        //  //  // �ִϸ��̼� ���������� ����ϵ��� �ϱ�

        //  //  anim.enabled = true;
          

        //  //// NPC ��ȭâ ����
        //  //talkImg.gameObject.SetActive(false);  
          

          


        //  // ī�޶� ��ũ��Ʈ�� �ٽ� �簡���ǵ��� 
        //  camera3.enabled = true;

        //  Cursor.visible = false;  // ���콺 Ŀ�� �Ⱥ��̰� �ϱ� (�̵��� �� ���ϰ�, �������� �ʰ�) 
        //  Cursor.lockState = CursorLockMode.Locked;

        //    // ����� UI����
        //    ClickExplainQuit();
        //    // ����Ϸ� UI����
        //    SaveCompleteQuit();
        //    // ������ ���Ž��� UI����
        //    ClickitamBuyFailImgQuit();
        //    // ī�޶� �ӵ� 150���� ���󺹱�
        //    freeLook.m_XAxis.m_MaxSpeed = 150;

        //    // ����� UI ���� 
        //    gm.CloseFullmap(); // ����� UI ���� 

        //    // ���� UI ����
        //    CloseSetting();

        //    // �κ��丮 UI ����
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

        else //isSettingUi�� true�� ��쿡��
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
