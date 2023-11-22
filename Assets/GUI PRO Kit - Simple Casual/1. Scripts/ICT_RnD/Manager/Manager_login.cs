
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Application = UnityEngine.Application;

// �� column�� �ش��ϴ� ������ �ۼ�
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

    //���� ������ �Ʒ��ٿ� �ֽ� ������ �߰��� ��
    //����
    public void Write()
    {
        //����� DataList�� ����� Filepath�� ����
        //���������� write �Լ��� ȣ���� ���� ������ ������ ���� ����
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
        //����� filepath���� xml���� �ε�
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
        //���� ������ �Ұ�� true�� �ǵ��� ���� �ʿ�
        Is_Logindatasaved = true;
        Student_data = data;
        
    }

    public void Refresh_data()
    {
        if (NewDataList.Count != OriginDataList.Count)
        {
            //�ʱ� start���� �����س� �����հ� ���� ���� ������
            //�������� ���� ��ȣ��ŭ �߰� ���� �ʿ�
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

    //Centents ������ �� ��
    //ó���� ������ �ִ��� üũ�ϰ�(���� �׷��� �������� Ȯ�� �ʿ���) ������ �ʱ�ȭ
    //���� ���õ� �л� ������ �޾ƿͼ� ����
    public void Setting_StudentInfo(int num_student)
    {
        Selected_Student_data = NewDataList[num_student];

        if (Is_Logindatasaved)
        {
            //Ŭ�� �� �л� ���� ������ �� �װ� ��������
            //���õ� �л� �����ͷ� ����
        }
        else
        {
            //�ʱ�ȭ
            Picture_Off.SetActive(true);
            Picture_On.SetActive(false);
            Text_ID.text = "�̼���";
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
