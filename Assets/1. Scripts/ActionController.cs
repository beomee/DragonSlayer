using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ������ ���� �� ���� 
public class ActionController : MonoBehaviour
{

    [SerializeField]
    private float range; // ���� ������ �ִ� �Ÿ�

    Player player;

    // ������ ���̾�� �����ϵ��� ���̾��ũ�� ����
    [SerializeField]
    private LayerMask layerMask;

    // �ʿ��� ������Ʈ
    [SerializeField]
    private Text actionText;

    [SerializeField]
    private Inventory theInventory;

    [SerializeField]
    private string openUi; // �÷��̾ �������� �ݴ� �Ҹ� 

    public Vector3 boxsize = new Vector3(1f, 1f, 1f);
    public Item item;


    private void Start()
    {
        player = GetComponent<Player>();

    }
    private void Update()
    {

        Collider[] colls = Physics.OverlapBox(transform.position, boxsize * 1.0f, transform.rotation,layerMask);  // �������� ������ ����

        if (Input.GetKeyDown(KeyCode.E))
        {
           
            for (int i = 0; i < colls.Length; i++)
            {
                PickupItem(colls[i]); // �������� �ݱ� 
            }

        }

        if (colls.Length == 0) // �ڽ��ȿ� �������� �ƹ��͵� ���ٸ�
        {
            InfoDisAppear();
        }

    }
    private void OnDrawGizmos()  // ���� �׸���
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.up, boxsize);
    }


    private void PickupItem(Collider coll) //������ �ݱ�
    {
        theInventory.AcquireItem(coll.GetComponent<ItemPickUp>().item);

        AudioManager.instance.PlaySE(openUi, 1, 0.25f);

        DestroyItem(coll);
    }


    private void DestroyItem(Collider coll)  // �ݶ��̴��� ���� ��ü �ı��ϱ� 
    {
        Destroy(coll.transform.gameObject);

    }


    public void ItemInfoAppear()
    {
        actionText.gameObject.SetActive(true);
        actionText.text = "������ ȹ�� ";
    }

    private void InfoDisAppear()
    {

        actionText.gameObject.SetActive(false);
        player.isGetItem = false;


    }

}