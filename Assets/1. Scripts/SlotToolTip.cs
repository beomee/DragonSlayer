using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField]
    GameObject go_Base;  // 필요할떄만 호출할 거다. 

    [SerializeField]   // 밖에서 끌어서 담을 수 있는데, 외부에서 쓰지는 못함.
    Text txt_ItemName; // 아이템 이름을 담는 텍스트 
    [SerializeField]
    Text txt_ItemInfo; // 아이템 설명을 담는 텍스트 
    [SerializeField]
    Text txt_ItemHowtoUsed; // 아이템 사용법을 담는 텍스트 

    public void PointerEnter()
    {
        if (GetComponentInParent<ShopSlot>().itemSell != null) // 판매할 아이템이 널이 아니라면 
        {

            ShowToolTip(GetComponentInParent<ShopSlot>().itemSell, transform.position); // 판매할 아이템에 위치값에 툴팁 보여주기
        }

    }

    public void PointerExit()
    {
        if (GetComponent<ShopSlot>().itemSell != null)
        {
            HideToolTip();
        }
    }

    public void ShowToolTip(Item item, Vector3 tooltip_position) // 툴팁을 보여주는 텍스트로 보여주는 함수 
    {
        go_Base.SetActive(true); // 툴팁 ui를 켜주고 

        tooltip_position += new Vector3(150f, 0f);

        go_Base.transform.position = tooltip_position;



        txt_ItemName.text = item.itemName;   // 아이템 이름부분의 텍스트에 아이템 이름 넣기 
        txt_ItemInfo.text = item.itemInfo;   // 아이템 설명부분의 텍스트에 아이템 설명 넣기 

        if (item.itemType == Item.ItemType.Used)  // 만약에 아이템의 타입이 Used라면 
        {
            txt_ItemHowtoUsed.text = "좌클릭 - 사용";  // 아이템의 사용법 부분의 텍스트에 아이템 사용법 설명 넣기 
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
