using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Inventory : MonoBehaviour
{
    // 인벤토리가 활성화되면 true가 될텐데, 그때는 캐릭터 못움직이게 
    //public static bool inventoryActivated = false;

    public Cinemachine.CinemachineBrain camera3;
    public Cinemachine.CinemachineFreeLook freeLook;

    // 필요한 컴포넌트
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;

    SlotToolTip theSlotToolTip; // 슬롯툴팁을 호출하는 변수 선언

    public Slot[] slots;     // 슬롯들

    public Item[] itemKinds; // 아이템의 종류를 담아두는 변수 

    public bool InvenUiOpen = false;

    [SerializeField]
    private string openUi; // 플레이어가 아이템을 줍는 소리 

    // Start is called before the first frame update
    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        theSlotToolTip = FindObjectOfType<SlotToolTip>();
       
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (InvenUiOpen == false)
        {
            if (Input.GetKeyDown(KeyCode.I))

            {
                OpenInventory();
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.I))

            {
                CloseInventory();
            }
        }
    }

    private void OpenInventory()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        freeLook.m_XAxis.m_MaxSpeed = 0;
        go_InventoryBase.SetActive(true);
        AudioManager.instance.PlaySE(openUi,1,1);
        //camera3.enabled = false;
        InvenUiOpen = true;

    }
    public void CloseInventory()
    {
        freeLook.m_XAxis.m_MaxSpeed = 150;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        go_InventoryBase.SetActive(false);
        theSlotToolTip.HideToolTip();

        InvenUiOpen = false;


    }

    public void AcquireItem(Item _item, int _count = 1)  // 아이템 줍기
    {

        // 슬롯에 이미 아이템이 있다
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null) // 슬롯안의 아이템이 null이 아니라면 
            {
                // 개수를 채워주고
                if (slots[i].item.itemName == _item.itemName)
                {
                    slots[i].SetSlotCount(_count); // 아이템의 수량을 나타내는 코드
                    return;
                }
            }

        }

        // 아이템이 없다
        for (int i = 0; i < slots.Length; i++)
        {
            // 빈자리를 채워준다
            if (slots[i].item == null)
            {
                    slots[i].AddItem(_item, _count);

                return;
            }
        }

    }

    public void SaveSlot() // 인벤토리의 슬롯안에 있는 아이템의 번호를 리스트에 담기 위한 함수
    {
        // 리스트 초기화
        Json.instance.data.itemNumber.Clear();

        //  슬롯의0번째에 있는 아이템의 번호를 담는 함수

        for (int i = 0; i < slots.Length; i++) // 슬롯의 개수만큼 반복해야 함.
        {

            if (slots[i].item != null) // 슬롯안의 아이템이 비어있지 않을때면
            {
                for (int j = 0; j < slots[i].itemCount; j++)
                {
                    // 제이슨안에 있는 인트 자료형의 리스트에 / 슬롯안에있는 아이템의 아이템 번호를 추가
                    Json.instance.data.itemNumber.Add(slots[i].item.ItemNumber); 
                }

            }


        }


    }

    public void LoadSlot() // 인트로 고유번호를 저장해놓은 리스트에서 확인하고, 그 번호에 맞는 아이템을 인벤토리 슬롯안으로 넣어야 함.
    {

        for (int i = 0; i < Json.instance.data.itemNumber.Count; i++) // 카운트만큼 반복해서 리스트안의 번호를 가져온거.
        {
            
            int itemRealNumber = Json.instance.data.itemNumber[i]; // 아이템 고유번호를 담는 변수.

            AcquireItem(itemKinds[itemRealNumber]);  // 아이템 획득하는 함수(아이템이 뭔지 -> 아이템종류의 고유번호대로 가져오게끔 ex) 0이면 골드, 1이면 포션 ...)
            
        }
    }
}
