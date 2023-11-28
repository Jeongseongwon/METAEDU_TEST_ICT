
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using TMPro;
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
    private GameLauncher_ICT Launcher;

    public static List<LoginData> OriginDataList;
    private List<LoginData> NewDataList;

    private string filePath;
    private LoginData Student_data;
    private LoginData Selected_Student_data;
    private int Num_student;
    public bool Is_Logindatasaved = false;
    public bool Is_StudentDataSelected = false;
    private int num_list = 0;

    [Header("[CONTENTS PAGE COMPONENT]")]
    public GameObject Text_Icon_group;
    private GameObject Picture_Off;
    private GameObject Picture_On;
    private Text Text_ID;
    private Text Text_Name;

    [Header("[LOGIN PAGE COMPONENT]")]
    public GameObject Prefab_StudentInfo;
    public Transform Panel_Left_Content;

    public GameObject InputField_group;
    private GameObject InputField_Name;
    private GameObject InputField_ID;
    private GameObject InputField_BirthDate;

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
        Num_student = -1;

        Init_Text();
        Launcher = this.gameObject.GetComponent<GameLauncher_ICT>();

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
            num_list = OriginDataList.Count;
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

    public void Add_Studentdata()
    {
        //Debug.Log(InputField_ID.GetComponent<TMP_InputField>().text);
        //Debug.Log(InputField_Name.GetComponent<TMP_InputField>().text);
        //Debug.Log(InputField_BirthDate.GetComponent<TMP_InputField>().text);


        //필드가 비었을 경우 예외처리, 다른 문자가 들어갔을 경우 예외처리

        LoginData Item = new LoginData();
        Item.ID = InputField_ID.GetComponent<TMP_InputField>().text;
        Item.Name = InputField_Name.GetComponent<TMP_InputField>().text;
        Item.Birth_date = InputField_BirthDate.GetComponent<TMP_InputField>().text;

        NewDataList.Add(Item);

        if (NewDataList[NewDataList.Count - 1].ID == Item.ID)
        {
            Launcher.Button_Message_Login_StudentDataSaved();
        }

        Refresh_data();
        //그리고 나중에 정상종료가 되지 않을 경우에 미리미리 저장하지 않도록 예외처리 필요
    }

    public void Refresh_data()
    {
        if (NewDataList.Count != OriginDataList.Count)
        {
            for (int i = num_list; i < NewDataList.Count; ++i)
            {
                LoginData item = NewDataList[i];

                GameObject myInstance = Instantiate(Prefab_StudentInfo, Panel_Left_Content);
                myInstance.GetComponent<UI_button_StudentInfo>().Result_num = i;
                myInstance.GetComponent<UI_button_StudentInfo>().Student_Name = item.Name;
                myInstance.GetComponent<UI_button_StudentInfo>().Student_BirthDate = item.Birth_date;
                myInstance.GetComponent<UI_button_StudentInfo>().Student_ID = item.ID;
            }

            num_list = NewDataList.Count;
        }
    }

    public LoginData Get_Listdata(int num)
    {
        return OriginDataList[num];
    }

    public bool Get_Islogindatasaved()
    {
        return Is_Logindatasaved;
    }
    public bool Get_Is_StudentDataSelected()
    {
        return Is_StudentDataSelected;
    }

    public void Setting_StudentInfo()
    {
        if (Is_StudentDataSelected)
        {
            ID = Selected_Student_data.ID;
            Name = Selected_Student_data.Name;
            Birthdate = Selected_Student_data.Birth_date;
            Date = DateTime.Now.ToString(("yyyy.mm.dd"));

            Picture_On.SetActive(true);
            Picture_Off.SetActive(false);
            Text_ID.text = ID;
            Text_Name.text = Name;

            Is_StudentDataSelected = false;
            Is_Logindatasaved = true;
        }
        else
        {
            //데이터 초기화 하는 부분, 추후에 적절한 곳으로 수정 필요 1129
            Picture_Off.SetActive(true);
            Picture_On.SetActive(false);
            Text_Name.text = "미선택";
            Text_ID.text = "ICT_RND_OOOO";
        }
    }


    public void Set_Selectednumber(int num)
    {
        Is_StudentDataSelected = true;
        Num_student = num;
        Selected_Student_data = NewDataList[Num_student];
    }
    public string Get_SelectedStudentName()
    {
        return Selected_Student_data.Name;
    }
    public string Get_SelectedStudentID()
    {
        return Selected_Student_data.ID;
    }

    public void Init_Text()
    {
        Picture_Off = Text_Icon_group.transform.GetChild(0).gameObject;
        Picture_On = Text_Icon_group.transform.GetChild(1).gameObject;
        Text_ID = Text_Icon_group.transform.GetChild(2).gameObject.GetComponent<Text>();
        Text_Name = Text_Icon_group.transform.GetChild(3).gameObject.GetComponent<Text>();

        InputField_Name = InputField_group.transform.GetChild(0).gameObject;
        InputField_ID = InputField_group.transform.GetChild(1).gameObject;
        InputField_BirthDate = InputField_group.transform.GetChild(2).gameObject;

    }
}
