using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class ShopSlot : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/
{

    public Item itemSell; // �ǸŸ� �� ������
    public Image slotImg;
    public int itemCount = 1; //�������� ����
    public int buyForNeedGold = 1; // �����۱��ſ� �ʿ��� �� ����� ���� (������ ���� * �������� ����)
    public Inventory theInventory; //�κ��丮 ��������
    public Text itemCountTxt;   // ������ ������ �ؽ�Ʈ�� �������� (UI�� �������� �ϴϱ�) 
    public Text buyForNeedGoldTxt;  // ������ ���ſ� �ʿ��� �� ����� ������ �ؽ�Ʈ�� �������� (UI�� �������� �ϴϱ�)
    public GameObject itemBuyFailImg; // ������ ���ſ� ���������� �ߴ� ������ ���� ����
    SlotToolTip theSlotToolTip; // ���������� ȣ���ϴ� ���� ����

    GameObject storeImg; // storeImg ������Ʈ�� setActive true/false �ϱ����ؼ� ���� ����

    [SerializeField]
    private string purchaseItem; // �÷��̾ ������ �����ϴ� �Ҹ� 

    // Start is called before the first frame update
    void Start()
    {

        slotImg.sprite = itemSell.itemImage; // ���� �������ִ� ���� �̹�����, �Ǹ��� �̹����� �ֱ�. 

        storeImg = GetComponent<GameObject>();

        theSlotToolTip = FindObjectOfType<SlotToolTip>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    // �������� �����ϴ� �Լ�
    public void ItemBuy()
    {

        int goldCount = 0; // ����� ������ ���� ����
        int goldSlotNum = -1; // ��尡 ����ִ� ������ ��ȣ(�𸣴ϱ� -1)�� ����)


        for (int i = 0; i < theInventory.slots.Length; i++) // �κ��丮�� ������ ���̸�ŭ �ݺ��� ��.
        {

            if (theInventory.slots[i].item != null) //�κ��丮 i��° ������ �������� null ���� �ƴ϶��,
            {

                // �� ���Կ� �ִ� �������� �̸��� ������� �ƴ��� �Ǻ��ϱ�
                if (theInventory.slots[i].item.itemName == "���")
                {
                    goldCount = theInventory.slots[i].itemCount; //����� ���� 

                    goldSlotNum = i;  // ��尡 ����ִ� ������ ��ȣ�� ��尡 ����ִ� i��°�� ����. -> ��尡 ����� ��¥ ����
                }
                 
            }

        }

        if (goldCount >= buyForNeedGold) // ���� �������ִ� ����� ������ �����Ҷ� �ʿ��� ����� �������� ���ٸ� 
        {
            AudioManager.instance.PlaySE(purchaseItem,1,1);
            theInventory.AcquireItem(itemSell, itemCount);  // �������Կ� �ִ� �������� �����ϴ� �Լ� (��������, itemCount) ��ŭ

            // �������� ���ݸ�ŭ ���� ���� ��带 �A ����, ���� ���� ����� ������ �־������.
            goldCount = goldCount - buyForNeedGold ; // goldCount�� ���� ���� ���� ����

            // ��¥ ����� ������ goldCount �������� �־���.
            theInventory.slots[goldSlotNum].SetSlotCount(-buyForNeedGold);
            theInventory.slots[goldSlotNum].itemCount = goldCount; //�κ��丮�� ���Կ��ִ�[��尡 ����ִ� �����ǹ�ȣ]�� ������ ���� �� goldCount�� ����.

        }

        else  // ���� ������ �� ��Ÿ���� ��� 
        {
            itemBuyFailImg.SetActive(true);
        }
    }

    // ������ ������ ���������ִ� �Լ� 
    public void ItemCountPlus()
    {
        if (itemCount == 10)
        {
            return;
        }

        else
        { 
        itemCount++; // ������ ������ 1�� ����

        itemCountTxt.text = itemCount.ToString(); // �����ϴ� ���� �ؽ�Ʈ�� ������ ǥ���ϱ�

        buyForNeedGold = itemCount * itemSell.itemPrice; // ������ ǥ�õ� �������� ����

        buyForNeedGoldTxt.text = buyForNeedGold.ToString();  // ǥ�õ� �������� ������ �ؽ�Ʈ�� ǥ���ϱ�
        }
    }

    public void ItemCountMinus()
    {
        if (itemCount == 1)
        {
            return;
        }

        else
        {
            itemCount--; // ������ ������ 1�� ����


            itemCountTxt.text = itemCount.ToString(); // �����ϴ� ���� �ؽ�Ʈ�� ȭ�鿡 ǥ��

            buyForNeedGold = itemCount * itemSell.itemPrice; // ������ ǥ�õ� �������� ����

            buyForNeedGoldTxt.text = buyForNeedGold.ToString(); // ǥ�õ� �������� ������ �ؽ�Ʈ�� ǥ��
        }

    }


}
