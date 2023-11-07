﻿
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Application = UnityEngine.Application;

// 각 column에 해당하는 데이터 작성
public class DialogueData
{
    [XmlAttribute]
    public string ID;
    [XmlAttribute]
    public string Name;
    [XmlAttribute]
    public string Birth_date;
    [XmlAttribute]
    public string Date;
    [XmlAttribute]
    public string Session;
    [XmlAttribute]
    public string Data_1;
    [XmlAttribute]
    public string Data_2;
}

public class Manager_data : MonoBehaviour
{
    public static Manager_data instance = null;

    public string File_name;    //해당 파일 불러오기
    public static List<DialogueData> itemList;

    [Header ("DATA RESULT PAGE COMPONENT")]
    public GameObject Graph_chart;
    public GameObject Prefab_SD;
    public Transform Panel_Left_Content;
    public Slider ProgressBar_OX;
    public Slider ProgressBar_SW;

    public UnityEngine.UI.Text test_Name;
    public UnityEngine.UI.Text text_ID;
    public UnityEngine.UI.Text test_Time;
    public UnityEngine.UI.Text text_Data_1;
    public UnityEngine.UI.Text text_Data_2;
    public UnityEngine.UI.Text text_Date_0;
    public UnityEngine.UI.Text text_Date_1;
    public UnityEngine.UI.Text text_Date_2;
    public GameObject text_None;

    //
    private Stack<DialogueData> Recent_data = new Stack<DialogueData>();
    private Stack<string> Recent_result_1 = new Stack<string>();
    private Stack<string> Recent_result_2 = new Stack<string>();
    private int Num_Recent_data;

    private string filePath;
    private List<DialogueData> DataList;
    private DialogueData Student_data;
    public bool Is_datasaved=false;
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
        filePath = Application.dataPath + "/Resources/Data/Data_exceltoxml.xml";
        DataList = Read();

        if (File_name != null)
        {
            //Read_txt();
            Recent_data.Clear();
            Debug.Log(Application.dataPath);
            itemList = Read();
            for (int i = 0; i < itemList.Count; ++i)
            {
                DialogueData item = itemList[i];
                //Debug.Log(string.Format("DATA [{0}] : ({1}, {2}, {3}, {4}, {5}, {6}, {7})",
                //    i, item.ID, item.Name, item.Birth_date, item.Date, item.Session, item.Data_1, item.Data_2));

                GameObject myInstance = Instantiate(Prefab_SD, Panel_Left_Content);
                myInstance.GetComponent<UI_button_SD>().Result_num = i;
                myInstance.GetComponent<UI_button_SD>().Student = item.Name;
            }
            //itemList = Read(Application.dataPath + "/Resources/Data/Data_exceltoxml.xml");
            Write();

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
        if (Is_datasaved)
        {
            DataList.Add(Student_data);
            Debug.Log("SAVED DATA WRITE");
        }

        XmlDocument Document = new XmlDocument();
        XmlElement ItemListElement = Document.CreateElement("Test_data");
        Document.AppendChild(ItemListElement);

        foreach (DialogueData data in DataList)
        {
            XmlElement ItemElement = Document.CreateElement("Test_data");
            ItemElement.SetAttribute("ID", data.ID);
            ItemElement.SetAttribute("Name", data.Name);
            ItemElement.SetAttribute("Birthdate", data.Birth_date);
            ItemElement.SetAttribute("Date", data.Date);
            ItemElement.SetAttribute("Session", data.Session);
            ItemElement.SetAttribute("Data_1", data.Data_1);
            ItemElement.SetAttribute("Data_2", data.Data_2);
            ItemListElement.AppendChild(ItemElement);
        }
        Document.Save(filePath);

        Is_datasaved = false;
    }

    public List<DialogueData> Read()
    {
        //저장된 filepath에서 xml파일 로드
        XmlDocument Document = new XmlDocument();
        Document.Load(filePath);
        XmlElement ItemListElement = Document["Test_data"];
        List<DialogueData> ItemList = new List<DialogueData>();

        foreach (XmlElement ItemElement in ItemListElement.ChildNodes)
        {
            DialogueData Item = new DialogueData();
            Item.ID = ItemElement.GetAttribute("ID");
            Item.Name = ItemElement.GetAttribute("Name");
            Item.Birth_date = ItemElement.GetAttribute("Birthdate");
            Item.Date = ItemElement.GetAttribute("Date");
            Item.Session = ItemElement.GetAttribute("Session");
            Item.Data_1 = ItemElement.GetAttribute("Data_1");
            Item.Data_2 = ItemElement.GetAttribute("Data_2");

            ItemList.Add(Item);
        }
        return ItemList;
    }

    public void Add_data(DialogueData data)
    {
        Is_datasaved = true;
        Student_data = data;
    }
    public void Change_result(int num)
    {
        Recent_data.Clear();
        Recent_result_1.Clear();
        Recent_result_2.Clear();

        DialogueData Item = itemList[num];

        test_Name.text = Item.Name;
        text_ID.text = Item.ID;
        test_Time.text = Item.Date;
        text_Data_1.text = Item.Data_1;
        text_Data_2.text = Item.Data_2;

        //최근 플레이 데이터
        ProgressBar_OX.value = Int32.Parse(Item.Data_1) * 0.1f;
        ProgressBar_SW.value = Int32.Parse(Item.Data_2) * 0.1f;

        //최근 3회차 데이터 추출
        foreach (DialogueData data in itemList)
        {
            if (data.ID == Item.ID)
            {
                //결과 데이터 1,2, 시간 데이터 추출
                Recent_data.Push(data);
                //Debug.Log(data);
                //Debug.Log(Recent_data);
                //Debug.Log("Same data" + data.Date + data.Data_1 + data.Data_2);
            }
        }

        //1,2 -> 데이터 없음
        //3 -> 보여주기
        if (Recent_data.Count > 2)
        {
            for (int i = 0; i < 3; i++)
            {
                //가장 최근 데이터 순
                Item = Recent_data.Pop();
                Recent_result_1.Push(Item.Data_1);
                Recent_result_2.Push(Item.Data_2);
                //각각 데이터1,2 새로운 스택에 저장 및 x축 변경
                if (i == 0)
                {
                    text_Date_2.text = Item.Date;
                }
                else if (i == 1)
                {
                    text_Date_1.text = Item.Date;
                }
                else if (i == 2)
                {
                    //가장 오래된 데이터
                    text_Date_0.text = Item.Date;
                }
            }

            //Debug.Log("Number of Each data" + Recent_result_1.Count + Recent_result_2.Count);
            //값 변경 함수 호출
            Graph_chart.GetComponent<MultipleGraphDemo>().Add_Data(Recent_result_1, Recent_result_2);
            text_None.SetActive(false);
            Graph_chart.SetActive(true);
        }
        else
        {
            Graph_chart.SetActive(false);
            text_None.SetActive(true);
        }
    }
    public void Setting_ButtonSD()
    {

    }

    public DialogueData Get_Listdata(int num)
    {
        return itemList[num];
    }

    //텍스트 기반 데이터 동기화
    /*
    void Read_txt()
    {
        //TextAsset Script_file = Resources.Load(Scene_number) as TextAsset;
        TextAsset Script_file = Resources.Load<TextAsset>(File_name);
        StringReader sr = new StringReader(Script_file.text);
        textList.Clear();

        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_String = sr.ReadLine();
            if (data_String == null)
            {
                endOfFile = true;
                break;
            }
            var data_values = data_String.Split('\n');
            for (int i = 0; i < data_values.Length; i++)
            {
                textList.Add(data_values[i]);
                Max_num_script++;
                //Debug.Log(textList[i]);
            }
        }
        //Debug.Log(Max_num_script);
    }
    public void Change_result(int num)
    {
        Split_txt(textList[num]);
    }

    void Split_txt(string str)
    {
        string[] words = str.Split(',');
        Student_ID = words[0];
        Data_1 = words[1];
        Data_2 = words[2];


        text_1.text = Student_ID;
        text_2.text = Data_1;
        text_3.text = Data_2;

        //Debug.Log(Student_ID);
        //Debug.Log(Data_1);
        //Debug.Log(Data_2);
    }
    */
}
