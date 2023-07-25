using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{

    // 어디서든 대입시킬 수 있게 인스턴스로
    static public DragSlot instance;

    public Slot dragSlot;

    [SerializeField]
    private Image imageItem;


    private void Start()
    {
        instance = this;
    }

    public void DragSetImage(Image _itemImage)
    {
        imageItem.sprite = _itemImage.sprite;
        SetColor(1);
    }


    // 흰색 배경을 드래그 할 때만 보여주기
    public void SetColor(float _alpha)
    {
        Color color = imageItem.color;
        color.a = _alpha;
        imageItem.color = color;
    }


}
