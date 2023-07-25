using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{

    // ��𼭵� ���Խ�ų �� �ְ� �ν��Ͻ���
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


    // ��� ����� �巡�� �� ���� �����ֱ�
    public void SetColor(float _alpha)
    {
        Color color = imageItem.color;
        color.a = _alpha;
        imageItem.color = color;
    }


}
