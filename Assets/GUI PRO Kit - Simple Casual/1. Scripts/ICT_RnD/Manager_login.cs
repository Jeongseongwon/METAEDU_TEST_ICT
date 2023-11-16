
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Application = UnityEngine.Application;

// 각 column에 해당하는 데이터 작성
public class LoginData
{
    [XmlAttribute]
    public string ID;
    [XmlAttribute]
    public string Name;
    [XmlAttribute]
    public string Birth_date;
}

public class Manager_login : MonoBehaviour
{
    public static Manager_login instance = null;

    public static List<LoginData> OriginDataList;
    private List<LoginData> NewDataList;

    private string filePath;
    private LoginData Student_data;
    public bool Is_datasaved = false;


    [Header("LOGIN PAGE COMPONENT")]
    public GameObject Prefab_StudentInfo;
    public Transform Panel_Left_Content;
    public Slider ProgressBar_OX;
    public Slider ProgressBar_SW;

    public GameObject DataText_group;
    //추후 public 삭제 필요
    private UnityEngine.UI.Text test_Name;
    private UnityEngine.UI.Text text_ID;
    private UnityEngine.UI.Text test_Time;
    private UnityEngine.UI.Text text_Data_1;
    private UnityEngine.UI.Text text_Data_2;
    private GameObject text_None;
    private UnityEngine.UI.Text text_Date_0;
    private UnityEngine.UI.Text text_Date_1;
    private UnityEngine.UI.Text text_Date_2;

    //
    private Stack<DialogueData> Recent_data = new Stack<DialogueData>();
    private Stack<string> Recent_result_1 = new Stack<string>();
    private Stack<string> Recent_result_2 = new Stack<string>();


    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }

    void Start()
    {
        //Init_Text();
        filePath = Application.dataPath + "/Resources/Data/Info_ID.xml";

        if (filePath != null)
        {
            //Read_txt();
            Recent_data.Clear();
            Debug.Log(Application.dataPath);
            OriginDataList = Read();
            NewDataList = Read();

            for (int i = 0; i < OriginDataList.Count; ++i)
            {
                LoginData item = OriginDataList[i];
                //Debug.Log(string.Format("DATA [{0}] : ({1}, {2}, {3}, {4}, {5}, {6}, {7})",
                //    i, item.ID, item.Name, item.Birth_date, item.Date, item.Session, item.Data_1, item.Data_2));

                GameObject myInstance = Instantiate(Prefab_StudentInfo, Panel_Left_Content);
                myInstance.GetComponent<UI_button_StudentInfo>().Result_num = i;
                myInstance.GetComponent<UI_button_StudentInfo>().Student_Name = item.Name;
                myInstance.GetComponent<UI_button_StudentInfo>().Student_ID = item.ID;
                myInstance.GetComponent<UI_button_StudentInfo>().Student_BirthDate = item.Birth_date;
            }
            //Write();

            //itemList = Read(Application.dataPath + "/Resources/Data/Data_wirte_test.xml");
            //for (int i = 0; i < itemList.Count; ++i)
            //{
            //    DialogueData item = itemList[i];
            //    Debug.Log(string.Format("DATA [{0}] : ({1}, {2}, {3}, {4}, {5}, {6}, {7})",
            //        i, item.ID, item.Name, item.Birth_date, item.Date, item.Session, item.Data_1, item.Data_2));
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //기존 데이터 아랫줄에 최신 데이터 추가한 뒤
    //저장
    public void Write()
    {
        //저장된 DataList를 저장된 Filepath에 저장
        //최종적으로 write 함수가 호출이 되지 않으면 저장이 되지 않음
        if (Is_datasaved)
        {
            NewDataList.Add(Student_data);
            Debug.Log("SAVED DATA WRITE");
            Is_datasaved = false;
        }

        XmlDocument Document = new XmlDocument();
        XmlElement ItemListElement = Document.CreateElement("Test_data");
        Document.AppendChild(ItemListElement);

        foreach (LoginData data in NewDataList)
        {
            XmlElement ItemElement = Document.CreateElement("Test_data");
            ItemElement.SetAttribute("ID", data.ID);
            ItemElement.SetAttribute("Name", data.Name);
            ItemElement.SetAttribute("Birthdate", data.Birth_date);
            ItemListElement.AppendChild(ItemElement);
        }
        Document.Save(filePath);
    }

    public List<LoginData> Read()
    {
        //저장된 filepath에서 xml파일 로드
        XmlDocument Document = new XmlDocument();
        Document.Load(filePath);
        XmlElement ItemListElement = Document["Test_data"];
        List<LoginData> ItemList = new List<LoginData>();

        foreach (XmlElement ItemElement in ItemListElement.ChildNodes)
        {
            LoginData Item = new LoginData();
            Item.ID = ItemElement.GetAttribute("ID");
            Item.Name = ItemElement.GetAttribute("Name");
            Item.Birth_date = ItemElement.GetAttribute("Birthdate");

            ItemList.Add(Item);
        }
        return ItemList;
    }

    public void Add_data(LoginData data)
    {
        Is_datasaved = true;
        Student_data = data;
    }

    public void Refresh_data()
    {
        if (NewDataList.Count != OriginDataList.Count)
        {
            //초기 start에서 생성해낸 프리팹과 수가 맞지 않으면
            //생성되지 않은 번호만큼 추가 생성 필요
            for (int i = OriginDataList.Count; i < NewDataList.Count; ++i)
            {
                LoginData item = NewDataList[i];
                //Debug.Log(string.Format("DATA [{0}] : ({1}, {2}, {3}, {4}, {5}, {6}, {7})",
                //    i, item.ID, item.Name, item.Birth_date, item.Date, item.Session, item.Data_1, item.Data_2));

                GameObject myInstance = Instantiate(Prefab_StudentInfo, Panel_Left_Content);
                myInstance.GetComponent<UI_button_SD>().Result_num = i;
                myInstance.GetComponent<UI_button_SD>().Student = item.Name;
            }
        }
    }

    public void Setting_ButtonSD()
    {

    }

    public LoginData Get_Listdata(int num)
    {
        return OriginDataList[num];
    }

    public void Init_Text()
    {
        test_Name = DataText_group.transform.GetChild(0).gameObject.GetComponent<Text>();
        text_ID = DataText_group.transform.GetChild(1).gameObject.GetComponent<Text>();
        test_Time = DataText_group.transform.GetChild(2).gameObject.GetComponent<Text>();
        text_Data_1 = DataText_group.transform.GetChild(3).gameObject.GetComponent<Text>();
        text_Data_2 = DataText_group.transform.GetChild(4).gameObject.GetComponent<Text>();
        text_None = DataText_group.transform.GetChild(5).gameObject;
        text_Date_0 = DataText_group.transform.GetChild(6).gameObject.GetComponent<Text>();
        text_Date_1 = DataText_group.transform.GetChild(7).gameObject.GetComponent<Text>();
        text_Date_2 = DataText_group.transform.GetChild(8).gameObject.GetComponent<Text>();
    }
}
