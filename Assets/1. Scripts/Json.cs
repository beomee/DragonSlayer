using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;



// 직렬화 
[System.Serializable]
public class Data  
{
    public float hp;   // 플레이어의 체력
    public float maxhp; // 플레이어의 최대 체력
    public float str;   // 플레이어의 공격력
    public Vector3 position; //플레이어의 위치 
    public float criticalStr; //플레이어의 크리티컬 공격력 배율 
    public List<int> itemNumber = new List<int>();  //  저장할 아이템 고유번호를 담을 인트자료형 리스트 
}



public class Json : MonoBehaviour
{
    public Data data;
    public Button Savebtn;   // 저장해줄 버튼
    static public Json instance; 
    public Transform player;

    public GameObject saveCompleteImg;

    string GameDataFileName = "GameData.json";
    private void Awake()
    {
        

        if (instance == null)
        {
            instance = this; //인스턴스에 나를 할당 

            DontDestroyOnLoad(gameObject); // 다른씬으로 가도 사라지지 않도록.      
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

       
    }

    // Start is called before the first frame update
    private void Start()
    {
       

        Savebtn.onClick.AddListener(Save); // 저장버튼 기능 시작 
        //Load();
    }

    public void Save()
    {
        data.position = player.position;  // 플레이어 위치 저장
        saveCompleteImg.SetActive(true);
        Json.instance.data.hp = Player.instance.hpBar.value;
        Json.instance.data.maxhp = Player.instance.hpBar.maxValue;
        FindObjectOfType<Inventory>().SaveSlot();   // 인벤토리의 슬롯을 저장 

        // 저장 경로
        string path = Application.persistentDataPath + "/" + GameDataFileName;
        // 저장할 클래스를 json 형태로 전환 (가독성 좋게)
        string saveDate = JsonUtility.ToJson(data,true);
        // json 형태로 전환된 문자열을 저장
        File.WriteAllText(path, saveDate); //파일을 생성하면서 값을 동시에 저장
        print("저장 완료");

    }


    public void Load()
    {
        // 불러오기 경로
        string path = Application.persistentDataPath + "/" + GameDataFileName;

        // 파일이 존재한다면 
        if (File.Exists(path))
        {
            // 문자열로 저장된 json 파일 읽어오기 
            string loadData = File.ReadAllText(path);
            // json을 클래스 형태로 전환 + 할당
            data = JsonUtility.FromJson<Data>(loadData);
            FindObjectOfType<Inventory>().LoadSlot();   // 인벤토리의 슬롯을 저장 
            player.position = data.position; // 플레이어 위치 불러오기
            Player.instance.hpBar.maxValue = Json.instance.data.maxhp;
            Player.instance.hpBar.value = Json.instance.data.hp;
            print(" 저장된 파일 불러오기 완료");
         

        }

        if (!File.Exists(path))
        {
            print("초기파일 불러오기 완료");
            FirstStart();
        }
    }

    public void DataDelete()
    {
        // 불러오기 경로
        string path = Application.persistentDataPath + "/" + GameDataFileName;

        if (File.Exists(path))
        {
            // 데이터 삭제 완료
            File.Delete(path);
        }

        else
        {
            return;
        }

        // 데이터 삭제 완료
    }

    public void FirstStart() // 초기 값으로 설정해주는 코드 
    {
        player.position = new Vector3(-235.5f, 0.118f, -236.8f);
        // 플레이어의 최초 스텟들 
        Json.instance.data.hp = 1000f;
        Json.instance.data.maxhp = 1000f;
        Json.instance.data.str = 70f;
        Json.instance.data.criticalStr = 3.00f;
        Json.instance.data.itemNumber.Clear(); // 아이템 초기화

        //Json.instance.data.position = new Vector3(-235.5f, 0.118f, -236.8f);
        //print(Json.instance.data.position);
        //player.position = Json.instance.data.position; // 플레이어 위치 불러오기
        
    }


    // 게임이 꺼졌을때 자동으로 저장 되는 기능
    // 게임 종료 시 호출
    //private void OnApplicationQuit()
    //{
    //    Save(); 
    //}

    // 제이슨은 게임오브젝트를 저장 못함.
}
