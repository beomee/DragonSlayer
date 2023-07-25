using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewMyStatus : MonoBehaviour
{
    public Text MyStrTxt; // �� ���� Str�� ���� �ؽ�Ʈ�� ��� �� ����
    public Text MyMaxHpTxt; //�� ���� MaxHP�� ���� �ؽ�Ʈ�� ��� �� ���� 
    public Text MyCriticalStrTxt; // �� ���� CritiacalStr�� ���� �ؽ�Ʈ�� ��� �� ����

    public GameObject StatusPanel; // ����â�г��� ��Ƶ� ����


    [SerializeField]
    private string openUi; // �÷��̾ �������� �ݴ� �Ҹ� 

    public bool statusUi = false;

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {

        TryOpenStatus();  // �κ��丮�� ���� �õ��� �ϴ� �Լ�

    }

    void TryOpenStatus()
    {

        if (statusUi == false)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                OpenStatus();  // ����â�� ���� 
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                CloseStatus(); // ����â�� �ݱ�
            }

        }

    }

    void OpenStatus() // ����â�� ����
    {
        StatusPanel.SetActive(true);
        ViewMyCriticalStr(); // ���� criticalStr�� ��ġ�� �ؽ�Ʈ�� ����ֱ�
        ViewMyMaxhp(); // ���� Maxhp�� ��ġ�� �ؽ�Ʈ�� ����ֱ�
        ViewMyStr(); // ���� Str�� ��ġ�� �ؽ�Ʈ�� ����ֱ� 
        AudioManager.instance.PlaySE(openUi,1,1);
        statusUi = true;
    }

    public void CloseStatus() // ����â�� �ݱ�
    {
        StatusPanel.SetActive(false);
        statusUi = false;
     
    }


    public void ViewMyStr() // ���� Str�� ��ġ�� �ؽ�Ʈ�� ����ֱ� + �������� ������ �ٷ� ������ �Ǿ�������� �ܺο��� ����ǵ��� public���� ����
    {
        MyStrTxt.text = Json.instance.data.str.ToString();
    }

    public void ViewMyMaxhp() // ���� Maxhp�� ��ġ�� �ؽ�Ʈ�� ����ֱ� + �������� ������ �ٷ� ������ �Ǿ�������� �ܺο��� ����ǵ��� public���� ����
    {
        MyMaxHpTxt.text = Json.instance.data.maxhp.ToString();
    }

    public void ViewMyCriticalStr() // ���� criticalStr�� ��ġ�� �ؽ�Ʈ�� ����ֱ� + �������� ������ �ٷ� ������ �Ǿ�������� �ܺο��� ����ǵ��� public���� ����
    {
        MyCriticalStrTxt.text = Json.instance.data.criticalStr.ToString();
    }
}
