using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 아이템 감지 및 습득 
public class ActionController : MonoBehaviour
{

    [SerializeField]
    private float range; // 습득 가능한 최대 거리

    Player player;

    // 아이템 레이어에만 반응하도록 레이어마스크를 설정
    [SerializeField]
    private LayerMask layerMask;

    // 필요한 컴포넌트
    [SerializeField]
    private Text actionText;

    [SerializeField]
    private Inventory theInventory;

    [SerializeField]
    private string openUi; // 플레이어가 아이템을 줍는 소리 

    public Vector3 boxsize = new Vector3(1f, 1f, 1f);
    public Item item;


    private void Start()
    {
        player = GetComponent<Player>();

    }
    private void Update()
    {

        Collider[] colls = Physics.OverlapBox(transform.position, boxsize * 1.0f, transform.rotation,layerMask);  // 아이템을 감지할 상자

        if (Input.GetKeyDown(KeyCode.E))
        {
           
            for (int i = 0; i < colls.Length; i++)
            {
                PickupItem(colls[i]); // 아이템을 줍기 
            }

        }

        if (colls.Length == 0) // 박스안에 아이템이 아무것도 없다면
        {
            InfoDisAppear();
        }

    }
    private void OnDrawGizmos()  // 상자 그리기
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.up, boxsize);
    }


    private void PickupItem(Collider coll) //아이템 줍기
    {
        theInventory.AcquireItem(coll.GetComponent<ItemPickUp>().item);

        AudioManager.instance.PlaySE(openUi, 1, 0.25f);

        DestroyItem(coll);
    }


    private void DestroyItem(Collider coll)  // 콜라이더에 닿은 물체 파괴하기 
    {
        Destroy(coll.transform.gameObject);

    }


    public void ItemInfoAppear()
    {
        actionText.gameObject.SetActive(true);
        actionText.text = "아이템 획득 ";
    }

    private void InfoDisAppear()
    {

        actionText.gameObject.SetActive(false);
        player.isGetItem = false;


    }

}