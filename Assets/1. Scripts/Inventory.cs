using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Inventory : MonoBehaviour
{
    // �κ��丮�� Ȱ��ȭ�Ǹ� true�� ���ٵ�, �׶��� ĳ���� �������̰� 
    //public static bool inventoryActivated = false;

    public Cinemachine.CinemachineBrain camera3;
    public Cinemachine.CinemachineFreeLook freeLook;

    // �ʿ��� ������Ʈ
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;

    SlotToolTip theSlotToolTip; // ���������� ȣ���ϴ� ���� ����

    public Slot[] slots;     // ���Ե�

    public Item[] itemKinds; // �������� ������ ��Ƶδ� ���� 

    public bool InvenUiOpen = false;

    [SerializeField]
    private string openUi; // �÷��̾ �������� �ݴ� �Ҹ� 

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

    public void AcquireItem(Item _item, int _count = 1)  // ������ �ݱ�
    {

        // ���Կ� �̹� �������� �ִ�
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null) // ���Ծ��� �������� null�� �ƴ϶�� 
            {
                // ������ ä���ְ�
                if (slots[i].item.itemName == _item.itemName)
                {
                    slots[i].SetSlotCount(_count); // �������� ������ ��Ÿ���� �ڵ�
                    return;
                }
            }

        }

        // �������� ����
        for (int i = 0; i < slots.Length; i++)
        {
            // ���ڸ��� ä���ش�
            if (slots[i].item == null)
            {
                    slots[i].AddItem(_item, _count);

                return;
            }
        }

    }

    public void SaveSlot() // �κ��丮�� ���Ծȿ� �ִ� �������� ��ȣ�� ����Ʈ�� ��� ���� �Լ�
    {
        // ����Ʈ �ʱ�ȭ
        Json.instance.data.itemNumber.Clear();

        //  ������0��°�� �ִ� �������� ��ȣ�� ��� �Լ�

        for (int i = 0; i < slots.Length; i++) // ������ ������ŭ �ݺ��ؾ� ��.
        {

            if (slots[i].item != null) // ���Ծ��� �������� ������� ��������
            {
                for (int j = 0; j < slots[i].itemCount; j++)
                {
                    // ���̽��ȿ� �ִ� ��Ʈ �ڷ����� ����Ʈ�� / ���Ծȿ��ִ� �������� ������ ��ȣ�� �߰�
                    Json.instance.data.itemNumber.Add(slots[i].item.ItemNumber); 
                }

            }


        }


    }

    public void LoadSlot() // ��Ʈ�� ������ȣ�� �����س��� ����Ʈ���� Ȯ���ϰ�, �� ��ȣ�� �´� �������� �κ��丮 ���Ծ����� �־�� ��.
    {

        for (int i = 0; i < Json.instance.data.itemNumber.Count; i++) // ī��Ʈ��ŭ �ݺ��ؼ� ����Ʈ���� ��ȣ�� �����°�.
        {
            
            int itemRealNumber = Json.instance.data.itemNumber[i]; // ������ ������ȣ�� ��� ����.

            AcquireItem(itemKinds[itemRealNumber]);  // ������ ȹ���ϴ� �Լ�(�������� ���� -> ������������ ������ȣ��� �������Բ� ex) 0�̸� ���, 1�̸� ���� ...)
            
        }
    }
}
