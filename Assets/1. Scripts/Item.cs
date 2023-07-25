using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject  // 게임오브젝트에 붙히지 않아도 작동함.
{

    public string itemName; // 아이템의 이름
    public ItemType itemType; // 아이템의 유형
    public int itemPrice;    // 아이템의 가격
    public Sprite itemImage; // 아이템의 이미지
    public GameObject itemPrefab; // 아이템의 프리팹
    public int ItemNumber; // 이 아이템의 고유번호

    [TextArea] // 다음줄로 이동이 가능하게 해주는 코드
    public string itemInfo;  // 아이템의 설명 
    public enum ItemType
    {
        Equipment, // 장비
        Used, // 소모품 
        Ingredint, // 재료
        ETC, // 등등
      
    }


    void Update()
    {

    }






}


