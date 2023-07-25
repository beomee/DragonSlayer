using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; //인터페이스를 담당하는 여러가지가 있음

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{

    public Item item; // 획득한 아이템
    public int itemCount; //획득한 아이템의 개수
    public float hp;
    public Image itemImage; //아이템의 이미지
    public Player player;
    public Slider hpBar;    // 플레이어의 체력바   


    ViewMyStatus viewMyStatus;
    SlotToolTip theSlotToolTip; // 슬롯툴팁을 호출하는 변수 선언
    
    // 필요한 컴포넌트
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    [SerializeField]
    private string DrinkHealingPotion; // 플레이어가 체력회복물약을 마시는 소리 

    [SerializeField]
    private string DrinkMaxHpPotion; // 플레이어가 체력증가물약을 마시는 소리

    [SerializeField]
    private string DrinkWarriorPotion; // 플레이어가 전사의물약을 마시는 소리

    [SerializeField]
    private string UseCriticalStone; // 플레이어가 전사의돌을 사용하는 소리









    void Start()
    {
        player = FindObjectOfType<Player>();
        viewMyStatus = FindObjectOfType<ViewMyStatus>();
        theSlotToolTip = FindObjectOfType<SlotToolTip>();
    }

  

    // 이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // 아이템 슬롯을 추가 한다.
    public void AddItem(Item _item, int _count = 1)
    {


        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();

        }

        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);

        }

        SetColor(1);

    }

    // 슬롯 안의 아이템 개수를 나타내는 코드
    public void SetSlotCount(int _count)
    {
     
        itemCount += _count; // 개수(itemCount)를 매개변수 _count만큼 올려주고 
        text_Count.text = itemCount.ToString(); // 그 개수(itemCount)의 값을 텍스트로 변환시켜줌

        // 아이템 개수가 0개 이하라면 
        if (itemCount <= 0)
        {
            // 슬롯 클리어 
            ClearSlot(); // 슬롯에 아무것도 없는 것으로 표기
        }
    }

    // 슬롯 초기화
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);


    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 마우스를 가져다대고 좌클릭을 하면, {}안의 이벤트 내용이 실행되는 것
        if (eventData.button == PointerEventData.InputButton.Left)
        {
                // 아이템이 있다면 
             if (item != null)
             {

                if (item.itemName == "체력회복물약")
                {
                    player.Healing();

                    AudioManager.instance.PlaySE(DrinkHealingPotion,1,1);
                    SetSlotCount(-1); // 한개를 소모

                }

                else if (item.itemName == "전사의 물약")
                {

                    Json.instance.data.str += 20;

                    SetSlotCount(-1); // 한개를 소모
                    AudioManager.instance.PlaySE(DrinkWarriorPotion,1,1);
                    viewMyStatus.ViewMyStr();
                }

                else if (item.itemName == "최대체력증가물약")
                {

                    Json.instance.data.maxhp += 100;

                    hpBar.maxValue = Json.instance.data.maxhp; //현재 maxhp 수치를 hp바의 maxvalue 값에 넣어주는것

                    AudioManager.instance.PlaySE(DrinkMaxHpPotion,1,1);

                    SetSlotCount(-1); // 한개를 소모
                    viewMyStatus.ViewMyMaxhp();
                }

                else if (item.itemName == "전사의 돌")
                {

                    Json.instance.data.criticalStr += 0.1000000f;

                    AudioManager.instance.PlaySE(UseCriticalStone,1,1);

                    SetSlotCount(-1); // 한개를 소모

                    viewMyStatus.ViewMyCriticalStr();
                }

                else if (item.itemName == "스태미나회복물약")
                {
                    player.HealingStamina(); // 플레이어의 스태미나회복 함수를 호출.

                    AudioManager.instance.PlaySE(DrinkHealingPotion,1,1);
                    SetSlotCount(-1); // 한개를 소모

                }

            }



            
            

        }
    }

    // 드래그 관련 
    public void OnBeginDrag(PointerEventData eventData) //드래그를 시작할 때 
    {
        if (item != null)

        {
            DragSlot.instance.dragSlot = this; //드래그슬롯 클래스안의 슬롯을 복사해서
            DragSlot.instance.DragSetImage(itemImage);  // 아이템 이미지를 넣어준다

            DragSlot.instance.transform.position = eventData.position; 
        }
        
    }

    public void OnDrag(PointerEventData eventData) //드래그 중일때 
    {
        if (item != null) //아이템이 null 값이 아니라면 

        {
            DragSlot.instance.transform.position = eventData.position;  //드래그 슬롯안의 자리에 eventData변수안에 자리에 넣어준다. 
        }
    }

    public void OnEndDrag(PointerEventData eventData) //드래그가 끝났을떄의 이벤트
    {
        DragSlot.instance.SetColor(0);  // 투명한 상태로 되어 짐
        DragSlot.instance.dragSlot = null; //슬롯이 null 값이 됨.
    }

    public void OnDrop(PointerEventData eventData)
    {

        // 빈슬롯
        if (DragSlot.instance.dragSlot != null) //드래그를 시작 할 슬롯이 null 값이 아니라면
            ChangeSlot(); //슬롯을 바꾸는 함수(아이템 변경의 의미)

    }

    private void ChangeSlot()
    {
        // 복사할 아이템 
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        //초기화
        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        }

        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }

    }

    // 마우스가 슬롯에 들어갈 때 발동 
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            theSlotToolTip.ShowToolTip(item, transform.position);
        }

    }

    // 마우스가 슬롯에서 나올 때 발동
    public void OnPointerExit(PointerEventData eventData)
    {
        theSlotToolTip.HideToolTip();

    }
}
