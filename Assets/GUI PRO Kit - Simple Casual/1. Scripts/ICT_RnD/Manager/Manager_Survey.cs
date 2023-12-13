using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.WSA;

public class SurveyData
{
    [XmlAttribute]
    public string ID;
    [XmlAttribute]
    public string Name;
    [XmlAttribute]
    public string Date;
    [XmlAttribute]
    public string Session;
    [XmlAttribute]
    public string Data_S1;
    [XmlAttribute]
    public string Data_S2;
    [XmlAttribute]
    public string Data_S3;
    [XmlAttribute]
    public string Data_S4;
    [XmlAttribute]
    public string Data_S5;
    [XmlAttribute]
    public string Data_S6;
    [XmlAttribute]
    public string Data_S7;
    [XmlAttribute]
    public string Data_S8;
}

public class Manager_Survey : MonoBehaviour
{
    public static Manager_Survey instance = null;
    public static List<SurveyData> OriginDataList;
    private List<SurveyData> NewDataList;

    private string filePath;

    private int Question_Number;
    private List<int> Answer_Number = new List<int>();

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
    public void Write()
    {
        XmlDocument Document = new XmlDocument();
        XmlElement ItemListElement = Document.CreateElement("Survey_data");
        Document.AppendChild(ItemListElement);

        foreach (SurveyData data in NewDataList)
        {
            XmlElement ItemElement = Document.CreateElement("Survey_data");
            ItemElement.SetAttribute("ID", data.ID);
            ItemElement.SetAttribute("Name", data.Name);
            ItemElement.SetAttribute("Date", data.Date);
            ItemElement.SetAttribute("Session", data.Session);
            ItemElement.SetAttribute("Data_1", data.Data_S1);
            ItemElement.SetAttribute("Data_2", data.Data_S2);
            ItemElement.SetAttribute("Data_3", data.Data_S3);
            ItemElement.SetAttribute("Data_4", data.Data_S4);
            ItemElement.SetAttribute("Data_5", data.Data_S5);
            ItemElement.SetAttribute("Data_6", data.Data_S6);
            ItemElement.SetAttribute("Data_7", data.Data_S7);
            ItemElement.SetAttribute("Data_8", data.Data_S8);
            ItemListElement.AppendChild(ItemElement);
        }
        Document.Save(filePath);

    }

    public List<SurveyData> Read()
    {
        //저장된 filepath에서 xml파일 로드
        XmlDocument Document = new XmlDocument();
        Document.Load(filePath);
        XmlElement ItemListElement = Document["Survey_data"];
        List<SurveyData> ItemList = new List<SurveyData>();

        foreach (XmlElement ItemElement in ItemListElement.ChildNodes)
        {
            SurveyData Item = new SurveyData();
            Item.ID = ItemElement.GetAttribute("ID");
            Item.Name = ItemElement.GetAttribute("Name");
            Item.Date = ItemElement.GetAttribute("Date");
            Item.Session = ItemElement.GetAttribute("Session");
            Item.Data_S1 = ItemElement.GetAttribute("Data_1");
            Item.Data_S2 = ItemElement.GetAttribute("Data_2");
            Item.Data_S3 = ItemElement.GetAttribute("Data_3");
            Item.Data_S4 = ItemElement.GetAttribute("Data_4");
            Item.Data_S5 = ItemElement.GetAttribute("Data_5");
            Item.Data_S6 = ItemElement.GetAttribute("Data_6");
            Item.Data_S7 = ItemElement.GetAttribute("Data_7");
            Item.Data_S8 = ItemElement.GetAttribute("Data_8");

            ItemList.Add(Item);
        }
        return ItemList;
    }
    public void Add_Surveydata()
    {
        SurveyData Item = new SurveyData();
        NewDataList.Add(Item);

        if (NewDataList[NewDataList.Count - 1].ID == Item.ID)
        {
            Write();
            Debug.Log("Survey data saved!");
        }
    }
    //전체 문제 데이터 저장
    //최종 xml 파일 형태로 따로 데이터 저장
    //그럼 설문조사 데이터도 가시화 해야하나?


    // Start is called before the first frame update
    void Start()
    {

        filePath = UnityEngine.Application.dataPath + "/Resources/Data/SURVEYRESULT.xml";
    }


    public void Button_Next(int Num_content)
    {
        //스크립트 변경
        //위에 텍스트 변경
    }
    public void Button_Prev()
    {
        //스크립트 변경
        //위에 텍스트 변경
    }
    public void Button_Answer(int num_Answer)
    {
        //해당 점수 반영
    }
    public void Button_Submit()
    {
        //해당 점수 반영 후 저장
        Add_Surveydata();
    }
}
