
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static UnityEditor.Progress;
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

    public GameObject Prefab_SD;
    public Transform Panel_Left_Content;

    public string File_name;    //해당 파일 불러오기
    public static List<DialogueData> itemList;

    public UnityEngine.UI.Text test_Name;
    public UnityEngine.UI.Text text_ID;
    public UnityEngine.UI.Text test_Time;
    public UnityEngine.UI.Text text_Data_1;
    public UnityEngine.UI.Text text_Data_2;

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
        if (File_name != null)
        {
            //Read_txt();

            Debug.Log(Application.dataPath);
            itemList = Read(Application.dataPath + "/Resources/Data/Data_exceltoxml.xml");
            for (int i = 0; i < itemList.Count; ++i)
            {
                DialogueData item = itemList[i];
                Debug.Log(string.Format("DATA [{0}] : ({1}, {2}, {3}, {4}, {5}, {6}, {7})",
                    i, item.ID, item.Name, item.Birth_date, item.Date, item.Session, item.Data_1, item.Data_2));

                GameObject myInstance = Instantiate(Prefab_SD, Panel_Left_Content);
                myInstance.GetComponent<UI_button_SD>().Result_num = i;
                myInstance.GetComponent<UI_button_SD>().Student = item.Name;
            }
            itemList = Read(Application.dataPath + "/Resources/Data/Data_exceltoxml.xml");
            Write(itemList,Application.dataPath + "/Resources/Data/Data_wirte_test.xml");

            itemList = Read(Application.dataPath + "/Resources/Data/Data_wirte_test.xml");
            for (int i = 0; i < itemList.Count; ++i)
            {
                DialogueData item = itemList[i];
                Debug.Log(string.Format("DATA [{0}] : ({1}, {2}, {3}, {4}, {5}, {6}, {7})",
                    i, item.ID, item.Name, item.Birth_date, item.Date, item.Session, item.Data_1, item.Data_2));
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {

    }


    public void Write(List<DialogueData> DataList, string filePath)
    {
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
    }

    public List<DialogueData> Read(string filePath)
    {
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


            //인스턴시 에이트 해서 버튼 추가
            //해당 버튼에 데이터 추가하기
        }
        return ItemList;
    }

    public void Change_result(int num)
    {
        DialogueData Item = itemList[num];

        test_Name.text = Item.Name;
        text_ID.text = Item.ID;
        test_Time.text = Item.Date;
        text_Data_1.text = Item.Data_1;
        text_Data_2.text = Item.Data_2;
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
