using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // 인스펙터 창에서 뜨게 됨
public class ItemEffect
{
    public string itemName; // 아이템의 이름. (키값)
    public string[] part; // 부위(어느부위를 회복/소모 할 건지)
    public int[] num; // 수치
                      
}
public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;

    private const string HP = "HP", SP = "SP", MP = "MP";

    // 필요한 컴포넌트
    //private StatusController thePlayerStatus;

    public void UseItem(Item _item)
    {
        if (_item.itemType == Item.ItemType.Used)
        {
            for (int x = 0; x < itemEffects.Length; x++)
            {
                if (itemEffects[x].itemName == _item.itemName)
                {

                    for (int y = 0; y < itemEffects[x].part.Length; y++)
                    {

                        switch (itemEffects[x].part[y])
                        {
                            case HP:
                             
                                break;
                            case SP:
                                break;
                            case MP:
                                break;
                        }


                    }

                }


            }
        }
    }

}
