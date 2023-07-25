using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;



// ����ȭ 
[System.Serializable]
public class Data  
{
    public float hp;   // �÷��̾��� ü��
    public float maxhp; // �÷��̾��� �ִ� ü��
    public float str;   // �÷��̾��� ���ݷ�
    public Vector3 position; //�÷��̾��� ��ġ 
    public float criticalStr; //�÷��̾��� ũ��Ƽ�� ���ݷ� ���� 
    public List<int> itemNumber = new List<int>();  //  ������ ������ ������ȣ�� ���� ��Ʈ�ڷ��� ����Ʈ 
}



public class Json : MonoBehaviour
{
    public Data data;
    public Button Savebtn;   // �������� ��ư
    static public Json instance; 
    public Transform player;

    public GameObject saveCompleteImg;

    string GameDataFileName = "GameData.json";
    private void Awake()
    {
        

        if (instance == null)
        {
            instance = this; //�ν��Ͻ��� ���� �Ҵ� 

            DontDestroyOnLoad(gameObject); // �ٸ������� ���� ������� �ʵ���.      
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

       
    }

    // Start is called before the first frame update
    private void Start()
    {
       

        Savebtn.onClick.AddListener(Save); // �����ư ��� ���� 
        //Load();
    }

    public void Save()
    {
        data.position = player.position;  // �÷��̾� ��ġ ����
        saveCompleteImg.SetActive(true);
        Json.instance.data.hp = Player.instance.hpBar.value;
        Json.instance.data.maxhp = Player.instance.hpBar.maxValue;
        FindObjectOfType<Inventory>().SaveSlot();   // �κ��丮�� ������ ���� 

        // ���� ���
        string path = Application.persistentDataPath + "/" + GameDataFileName;
        // ������ Ŭ������ json ���·� ��ȯ (������ ����)
        string saveDate = JsonUtility.ToJson(data,true);
        // json ���·� ��ȯ�� ���ڿ��� ����
        File.WriteAllText(path, saveDate); //������ �����ϸ鼭 ���� ���ÿ� ����
        print("���� �Ϸ�");

    }


    public void Load()
    {
        // �ҷ����� ���
        string path = Application.persistentDataPath + "/" + GameDataFileName;

        // ������ �����Ѵٸ� 
        if (File.Exists(path))
        {
            // ���ڿ��� ����� json ���� �о���� 
            string loadData = File.ReadAllText(path);
            // json�� Ŭ���� ���·� ��ȯ + �Ҵ�
            data = JsonUtility.FromJson<Data>(loadData);
            FindObjectOfType<Inventory>().LoadSlot();   // �κ��丮�� ������ ���� 
            player.position = data.position; // �÷��̾� ��ġ �ҷ�����
            Player.instance.hpBar.maxValue = Json.instance.data.maxhp;
            Player.instance.hpBar.value = Json.instance.data.hp;
            print(" ����� ���� �ҷ����� �Ϸ�");
         

        }

        if (!File.Exists(path))
        {
            print("�ʱ����� �ҷ����� �Ϸ�");
            FirstStart();
        }
    }

    public void DataDelete()
    {
        // �ҷ����� ���
        string path = Application.persistentDataPath + "/" + GameDataFileName;

        if (File.Exists(path))
        {
            // ������ ���� �Ϸ�
            File.Delete(path);
        }

        else
        {
            return;
        }

        // ������ ���� �Ϸ�
    }

    public void FirstStart() // �ʱ� ������ �������ִ� �ڵ� 
    {
        player.position = new Vector3(-235.5f, 0.118f, -236.8f);
        // �÷��̾��� ���� ���ݵ� 
        Json.instance.data.hp = 1000f;
        Json.instance.data.maxhp = 1000f;
        Json.instance.data.str = 70f;
        Json.instance.data.criticalStr = 3.00f;
        Json.instance.data.itemNumber.Clear(); // ������ �ʱ�ȭ

        //Json.instance.data.position = new Vector3(-235.5f, 0.118f, -236.8f);
        //print(Json.instance.data.position);
        //player.position = Json.instance.data.position; // �÷��̾� ��ġ �ҷ�����
        
    }


    // ������ �������� �ڵ����� ���� �Ǵ� ���
    // ���� ���� �� ȣ��
    //private void OnApplicationQuit()
    //{
    //    Save(); 
    //}

    // ���̽��� ���ӿ�����Ʈ�� ���� ����.
}
