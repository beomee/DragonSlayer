using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; //�������̽��� ����ϴ� ���������� ����

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{

    public Item item; // ȹ���� ������
    public int itemCount; //ȹ���� �������� ����
    public float hp;
    public Image itemImage; //�������� �̹���
    public Player player;
    public Slider hpBar;    // �÷��̾��� ü�¹�   


    ViewMyStatus viewMyStatus;
    SlotToolTip theSlotToolTip; // ���������� ȣ���ϴ� ���� ����
    
    // �ʿ��� ������Ʈ
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    [SerializeField]
    private string DrinkHealingPotion; // �÷��̾ ü��ȸ�������� ���ô� �Ҹ� 

    [SerializeField]
    private string DrinkMaxHpPotion; // �÷��̾ ü������������ ���ô� �Ҹ�

    [SerializeField]
    private string DrinkWarriorPotion; // �÷��̾ �����ǹ����� ���ô� �Ҹ�

    [SerializeField]
    private string UseCriticalStone; // �÷��̾ �����ǵ��� ����ϴ� �Ҹ�









    void Start()
    {
        player = FindObjectOfType<Player>();
        viewMyStatus = FindObjectOfType<ViewMyStatus>();
        theSlotToolTip = FindObjectOfType<SlotToolTip>();
    }

  

    // �̹����� ���� ����
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // ������ ������ �߰� �Ѵ�.
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

    // ���� ���� ������ ������ ��Ÿ���� �ڵ�
    public void SetSlotCount(int _count)
    {
     
        itemCount += _count; // ����(itemCount)�� �Ű����� _count��ŭ �÷��ְ� 
        text_Count.text = itemCount.ToString(); // �� ����(itemCount)�� ���� �ؽ�Ʈ�� ��ȯ������

        // ������ ������ 0�� ���϶�� 
        if (itemCount <= 0)
        {
            // ���� Ŭ���� 
            ClearSlot(); // ���Կ� �ƹ��͵� ���� ������ ǥ��
        }
    }

    // ���� �ʱ�ȭ
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
        // ���콺�� �����ٴ�� ��Ŭ���� �ϸ�, {}���� �̺�Ʈ ������ ����Ǵ� ��
        if (eventData.button == PointerEventData.InputButton.Left)
        {
                // �������� �ִٸ� 
             if (item != null)
             {

                if (item.itemName == "ü��ȸ������")
                {
                    player.Healing();

                    AudioManager.instance.PlaySE(DrinkHealingPotion,1,1);
                    SetSlotCount(-1); // �Ѱ��� �Ҹ�

                }

                else if (item.itemName == "������ ����")
                {

                    Json.instance.data.str += 20;

                    SetSlotCount(-1); // �Ѱ��� �Ҹ�
                    AudioManager.instance.PlaySE(DrinkWarriorPotion,1,1);
                    viewMyStatus.ViewMyStr();
                }

                else if (item.itemName == "�ִ�ü����������")
                {

                    Json.instance.data.maxhp += 100;

                    hpBar.maxValue = Json.instance.data.maxhp; //���� maxhp ��ġ�� hp���� maxvalue ���� �־��ִ°�

                    AudioManager.instance.PlaySE(DrinkMaxHpPotion,1,1);

                    SetSlotCount(-1); // �Ѱ��� �Ҹ�
                    viewMyStatus.ViewMyMaxhp();
                }

                else if (item.itemName == "������ ��")
                {

                    Json.instance.data.criticalStr += 0.1000000f;

                    AudioManager.instance.PlaySE(UseCriticalStone,1,1);

                    SetSlotCount(-1); // �Ѱ��� �Ҹ�

                    viewMyStatus.ViewMyCriticalStr();
                }

                else if (item.itemName == "���¹̳�ȸ������")
                {
                    player.HealingStamina(); // �÷��̾��� ���¹̳�ȸ�� �Լ��� ȣ��.

                    AudioManager.instance.PlaySE(DrinkHealingPotion,1,1);
                    SetSlotCount(-1); // �Ѱ��� �Ҹ�

                }

            }



            
            

        }
    }

    // �巡�� ���� 
    public void OnBeginDrag(PointerEventData eventData) //�巡�׸� ������ �� 
    {
        if (item != null)

        {
            DragSlot.instance.dragSlot = this; //�巡�׽��� Ŭ�������� ������ �����ؼ�
            DragSlot.instance.DragSetImage(itemImage);  // ������ �̹����� �־��ش�

            DragSlot.instance.transform.position = eventData.position; 
        }
        
    }

    public void OnDrag(PointerEventData eventData) //�巡�� ���϶� 
    {
        if (item != null) //�������� null ���� �ƴ϶�� 

        {
            DragSlot.instance.transform.position = eventData.position;  //�巡�� ���Ծ��� �ڸ��� eventData�����ȿ� �ڸ��� �־��ش�. 
        }
    }

    public void OnEndDrag(PointerEventData eventData) //�巡�װ� ���������� �̺�Ʈ
    {
        DragSlot.instance.SetColor(0);  // ������ ���·� �Ǿ� ��
        DragSlot.instance.dragSlot = null; //������ null ���� ��.
    }

    public void OnDrop(PointerEventData eventData)
    {

        // �󽽷�
        if (DragSlot.instance.dragSlot != null) //�巡�׸� ���� �� ������ null ���� �ƴ϶��
            ChangeSlot(); //������ �ٲٴ� �Լ�(������ ������ �ǹ�)

    }

    private void ChangeSlot()
    {
        // ������ ������ 
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        //�ʱ�ȭ
        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        }

        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }

    }

    // ���콺�� ���Կ� �� �� �ߵ� 
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            theSlotToolTip.ShowToolTip(item, transform.position);
        }

    }

    // ���콺�� ���Կ��� ���� �� �ߵ�
    public void OnPointerExit(PointerEventData eventData)
    {
        theSlotToolTip.HideToolTip();

    }
}
