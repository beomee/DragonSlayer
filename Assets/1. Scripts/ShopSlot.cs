using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class ShopSlot : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/
{

    public Item itemSell; // 판매를 할 아이템
    public Image slotImg;
    public int itemCount = 1; //아이템의 개수
    public int buyForNeedGold = 1; // 아이템구매에 필요한 총 골드의 개수 (아이템 개수 * 아이템의 가격)
    public Inventory theInventory; //인벤토리 가져오기
    public Text itemCountTxt;   // 아이템 개수를 텍스트로 가져오기 (UI에 보여져야 하니까) 
    public Text buyForNeedGoldTxt;  // 아이템 구매에 필요한 총 골드의 개수를 텍스트로 가져오기 (UI에 보여져야 하니까)
    public GameObject itemBuyFailImg; // 아이템 구매에 실패했을때 뜨는 문구를 담을 변수
    SlotToolTip theSlotToolTip; // 슬롯툴팁을 호출하는 변수 선언

    GameObject storeImg; // storeImg 오브젝트를 setActive true/false 하기위해서 만든 변수

    [SerializeField]
    private string purchaseItem; // 플레이어가 아이템 구매하는 소리 

    // Start is called before the first frame update
    void Start()
    {

        slotImg.sprite = itemSell.itemImage; // 내가 가지고있는 슬롯 이미지에, 판매할 이미지를 넣기. 

        storeImg = GetComponent<GameObject>();

        theSlotToolTip = FindObjectOfType<SlotToolTip>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    // 아이템을 구매하는 함수
    public void ItemBuy()
    {

        int goldCount = 0; // 골드의 개수를 담을 변수
        int goldSlotNum = -1; // 골드가 담겨있는 슬롯의 번호(모르니까 -1)로 선언)


        for (int i = 0; i < theInventory.slots.Length; i++) // 인벤토리의 슬롯의 길이만큼 반복을 함.
        {

            if (theInventory.slots[i].item != null) //인벤토리 i번째 슬롯의 아이템이 null 값이 아니라면,
            {

                // 이 슬롯에 있는 아이템의 이름이 골드인지 아닌지 판별하기
                if (theInventory.slots[i].item.itemName == "골드")
                {
                    goldCount = theInventory.slots[i].itemCount; //골드의 개수 

                    goldSlotNum = i;  // 골드가 담겨있는 슬롯의 번호에 골드가 담겨있는 i번째를 넣음. -> 골드가 담겨진 진짜 슬롯
                }
                 
            }

        }

        if (goldCount >= buyForNeedGold) // 내가 가지고있는 골드의 개수가 구매할때 필요한 골드의 개수보다 많다면 
        {
            AudioManager.instance.PlaySE(purchaseItem,1,1);
            theInventory.AcquireItem(itemSell, itemCount);  // 상점슬롯에 있는 아이템을 구매하는 함수 (아이템을, itemCount) 만큼

            // 아이템의 가격만큼 내가 가진 골드를 뺸 값을, 내가 가진 골드의 개수에 넣어줘야함.
            goldCount = goldCount - buyForNeedGold ; // goldCount의 변수 값만 변경 됐음

            // 진짜 골드의 개수에 goldCount 변수값을 넣어줌.
            theInventory.slots[goldSlotNum].SetSlotCount(-buyForNeedGold);
            theInventory.slots[goldSlotNum].itemCount = goldCount; //인벤토리의 슬롯에있는[골드가 담겨있는 슬롯의번호]의 아이템 개수 에 goldCount를 넣음.

        }

        else  // 돈이 부족할 때 나타내는 기능 
        {
            itemBuyFailImg.SetActive(true);
        }
    }

    // 아이템 개수를 증가시켜주는 함수 
    public void ItemCountPlus()
    {
        if (itemCount == 10)
        {
            return;
        }

        else
        { 
        itemCount++; // 아이템 개수를 1씩 증가

        itemCountTxt.text = itemCount.ToString(); // 증가하는 숫자 텍스트를 밖으로 표시하기

        buyForNeedGold = itemCount * itemSell.itemPrice; // 상점에 표시될 아이템의 가격

        buyForNeedGoldTxt.text = buyForNeedGold.ToString();  // 표시될 아이템의 가격을 텍스트로 표시하기
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
            itemCount--; // 아이템 개수를 1씩 감소


            itemCountTxt.text = itemCount.ToString(); // 감소하는 숫자 텍스트를 화면에 표기

            buyForNeedGold = itemCount * itemSell.itemPrice; // 상점에 표시될 아이템의 가격

            buyForNeedGoldTxt.text = buyForNeedGold.ToString(); // 표시될 아이템의 가격을 텍스트로 표시
        }

    }


}
