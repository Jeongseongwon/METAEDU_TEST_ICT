
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
    private LoginData Selected_Student_data;
    private int Num_student;
    public bool Is_Logindatasaved = false;


    [Header("[LOGIN PAGE COMPONENT]")]
    public GameObject Prefab_StudentInfo;
    public Transform Panel_Left_Content;

    public GameObject Student_Info;

    public GameObject Text_Icon_group;
    private GameObject Picture_Off;
    private GameObject Picture_On;
    private Text Text_ID;
    private Text Text_Name;

    private Stack<DialogueData> Recent_data = new Stack<DialogueData>();
    private Stack<string> Recent_result_1 = new Stack<string>();
    private Stack<string> Recent_result_2 = new Stack<string>();


    [Header("[LOGIN INFORMATION]")]
    [SerializeField]
    public string ID;
    public string Name;
    public string Birthdate;
    public string Date;
    public int Session;
    public string Data_1;
    public string Data_2;

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
        if (Is_Logindatasaved)
        {
            NewDataList.Add(Student_data);
            Debug.Log("SAVED DATA WRITE");
            Is_Logindatasaved = false;
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
        //최종 선택을 할경우 true가 되도록 수정 필요
        Is_Logindatasaved = true;
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

    public bool Get_Islogindatasaved()
    {
        return Is_Logindatasaved;
    }

    //Centents 페이지 갈 때
    //처음에 데이터 있는지 체크하고(정말 그렇게 구현될지 확인 필요함) 없으면 초기화
    //최종 선택된 학생 데이터 받아와서 설정
    public void Setting_StudentInfo(int num_student)
    {
        Selected_Student_data = NewDataList[num_student];

        if (Is_Logindatasaved)
        {
            //클릭 된 학생 숫자 저장한 뒤 그거 가져오고
            //선택된 학생 데이터로 변경
        }
        else
        {
            //초기화
            Picture_Off.SetActive(true);
            Picture_On.SetActive(false);
            Text_ID.text = "미선택";
            Text_Name.text = "ICT_RND_OOOO";
        }
    }

    public void Init_Text()
    {
        Picture_Off = Text_Icon_group.transform.GetChild(0).gameObject;
        Picture_On = Text_Icon_group.transform.GetChild(1).gameObject;
        Text_ID = Text_Icon_group.transform.GetChild(2).gameObject.GetComponent<Text>();
        Text_Name = Text_Icon_group.transform.GetChild(3).gameObject.GetComponent<Text>();
    }
}
