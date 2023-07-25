using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // �ν����� â���� �߰� ��
public class ItemEffect
{
    public string itemName; // �������� �̸�. (Ű��)
    public string[] part; // ����(��������� ȸ��/�Ҹ� �� ����)
    public int[] num; // ��ġ
                      
}
public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;

    private const string HP = "HP", SP = "SP", MP = "MP";

    // �ʿ��� ������Ʈ
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
