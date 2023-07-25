using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewMyStatus : MonoBehaviour
{
    public Text MyStrTxt; // 내 현재 Str의 값을 텍스트로 담아 둘 변수
    public Text MyMaxHpTxt; //내 현재 MaxHP의 값을 텍스트로 담아 둘 변수 
    public Text MyCriticalStrTxt; // 내 현재 CritiacalStr의 값을 텍스트로 담아 둘 변수

    public GameObject StatusPanel; // 스텟창패널을 담아둘 변수


    [SerializeField]
    private string openUi; // 플레이어가 아이템을 줍는 소리 

    public bool statusUi = false;

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {

        TryOpenStatus();  // 인벤토리를 여는 시도를 하는 함수

    }

    void TryOpenStatus()
    {

        if (statusUi == false)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                OpenStatus();  // 스텟창을 열기 
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                CloseStatus(); // 스텟창을 닫기
            }

        }

    }

    void OpenStatus() // 스텟창을 열기
    {
        StatusPanel.SetActive(true);
        ViewMyCriticalStr(); // 나의 criticalStr의 수치를 텍스트에 집어넣기
        ViewMyMaxhp(); // 나의 Maxhp의 수치를 텍스트에 집어넣기
        ViewMyStr(); // 나의 Str의 수치를 텍스트에 집어넣기 
        AudioManager.instance.PlaySE(openUi,1,1);
        statusUi = true;
    }

    public void CloseStatus() // 스텟창을 닫기
    {
        StatusPanel.SetActive(false);
        statusUi = false;
     
    }


    public void ViewMyStr() // 나의 Str의 수치를 텍스트에 집어넣기 + 아이템을 먹으면 바로 적용이 되어야함으로 외부에서 적용되도록 public으로 선언
    {
        MyStrTxt.text = Json.instance.data.str.ToString();
    }

    public void ViewMyMaxhp() // 나의 Maxhp의 수치를 텍스트에 집어넣기 + 아이템을 먹으면 바로 적용이 되어야함으로 외부에서 적용되도록 public으로 선언
    {
        MyMaxHpTxt.text = Json.instance.data.maxhp.ToString();
    }

    public void ViewMyCriticalStr() // 나의 criticalStr의 수치를 텍스트에 집어넣기 + 아이템을 먹으면 바로 적용이 되어야함으로 외부에서 적용되도록 public으로 선언
    {
        MyCriticalStrTxt.text = Json.instance.data.criticalStr.ToString();
    }
}
