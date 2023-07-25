using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField]
    GameObject go_Base;  // �ʿ��ҋ��� ȣ���� �Ŵ�. 

    [SerializeField]   // �ۿ��� ��� ���� �� �ִµ�, �ܺο��� ������ ����.
    Text txt_ItemName; // ������ �̸��� ��� �ؽ�Ʈ 
    [SerializeField]
    Text txt_ItemInfo; // ������ ������ ��� �ؽ�Ʈ 
    [SerializeField]
    Text txt_ItemHowtoUsed; // ������ ������ ��� �ؽ�Ʈ 

    public void PointerEnter()
    {
        if (GetComponentInParent<ShopSlot>().itemSell != null) // �Ǹ��� �������� ���� �ƴ϶�� 
        {

            ShowToolTip(GetComponentInParent<ShopSlot>().itemSell, transform.position); // �Ǹ��� �����ۿ� ��ġ���� ���� �����ֱ�
        }

    }

    public void PointerExit()
    {
        if (GetComponent<ShopSlot>().itemSell != null)
        {
            HideToolTip();
        }
    }

    public void ShowToolTip(Item item, Vector3 tooltip_position) // ������ �����ִ� �ؽ�Ʈ�� �����ִ� �Լ� 
    {
        go_Base.SetActive(true); // ���� ui�� ���ְ� 

        tooltip_position += new Vector3(150f, 0f);

        go_Base.transform.position = tooltip_position;



        txt_ItemName.text = item.itemName;   // ������ �̸��κ��� �ؽ�Ʈ�� ������ �̸� �ֱ� 
        txt_ItemInfo.text = item.itemInfo;   // ������ ����κ��� �ؽ�Ʈ�� ������ ���� �ֱ� 

        if (item.itemType == Item.ItemType.Used)  // ���࿡ �������� Ÿ���� Used��� 
        {
            txt_ItemHowtoUsed.text = "��Ŭ�� - ���";  // �������� ���� �κ��� �ؽ�Ʈ�� ������ ���� ���� �ֱ� 
        }

        else
        {
            txt_ItemHowtoUsed.text = "";
        }
    }

    public void HideToolTip()
    {
        go_Base.SetActive(false);
    }

}
