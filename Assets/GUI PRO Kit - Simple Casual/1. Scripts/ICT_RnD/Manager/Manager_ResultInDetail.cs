using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Manager_ResultInDetail : MonoBehaviour
{
    public static Manager_ResultInDetail instance = null;
    public static List<Result_IndetailData> OriginDataList;
    private List<Result_IndetailData> NewDataList;

    private string filePath;
    private TextAsset XmlFilepath;

    private List<string> String_Data_attribute = new List<string>();


    [Header("[RESULT IN DETAIL INFORMATION]")]
    [SerializeField]
    public string ID;
    public string Name;
    public string Session;
    public string Date;
    public List<string> Data_Indetail = new List<string>();
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
        Init_RID();
        filePath = "Data/RESULT_INDETAIL";
        XmlFilepath = Resources.Load<TextAsset>(filePath);

        if (filePath != null)
        {
            OriginDataList = Read();
            NewDataList = Read();
        }
        Write();
    }


    public void Write()
    {
        XmlDocument Document = new XmlDocument();
        XmlElement ItemListElement = Document.CreateElement("Result_Indetail_data");
        Document.AppendChild(ItemListElement);

        foreach (Result_IndetailData data in NewDataList)
        {
            XmlElement ItemElement = Document.CreateElement("Result_Indetail_data");
            ItemElement.SetAttribute("ID", data.ID);
            ItemElement.SetAttribute("Name", data.Name);
            ItemElement.SetAttribute("Date", data.Date);
            ItemElement.SetAttribute("Session", data.Session);

            for (int i = 0; i < data.Data.Count; i++)
            {
                ItemElement.SetAttribute(String_Data_attribute[i], data.Data[i]);
            }
            ItemListElement.AppendChild(ItemElement);
        }
        Document.Save(AssetDatabase.GetAssetPath(XmlFilepath));

        Debug.Log("Result In detail data saved!");
    }

    public List<Result_IndetailData> Read()
    {
        XmlDocument Document = new XmlDocument();
        Document.LoadXml(XmlFilepath.ToString());
        XmlElement ItemListElement = Document["Result_Indetail_data"];
        List<Result_IndetailData> ItemList = new List<Result_IndetailData>();

        foreach (XmlElement ItemElement in ItemListElement.ChildNodes)
        {
            Result_IndetailData Item = new Result_IndetailData();
            Item.ID = ItemElement.GetAttribute("ID");
            Item.Name = ItemElement.GetAttribute("Name");
            Item.Date = ItemElement.GetAttribute("Date");
            Item.Session = ItemElement.GetAttribute("Session");

            for (int i = 0; i < 50; i++)
            {
                if (string.IsNullOrEmpty(ItemElement.GetAttribute(String_Data_attribute[i])))
                {
                    //Debug.Log("Data Empty" + i);
                }
                else
                {
                    Item.Data.Add(ItemElement.GetAttribute(String_Data_attribute[i]));
                    //Debug.Log("Data_attribute : " + String_Data_attribute[i] + "item : " + Item.Data[i]);
                }
            }
            //Debug.Log("Data count : " + Item.Data.Count);
            ItemList.Add(Item);
        }
        return ItemList;
    }
    public void Add_RIDdata(float data)
    {
        //데이터 리스트에 추가
        Data_Indetail.Add(data.ToString());
    }
    public void Clear_RIDdata()
    {
        Data_Indetail.Clear();
    }
    public void Save_RIDdata(int content_session)
    {
        Result_IndetailData Item = new Result_IndetailData();

        ID = Manager_login.instance.ID;
        Name = Manager_login.instance.Name;
        Date = DateTime.Now.ToString(("yyyy.MM.dd.HH.mm"));
        Session = content_session.ToString();

        Item.ID = ID;
        Item.Name = Name;
        Item.Date = Date;
        Item.Session = Session;

        for (int i = 0; i < Data_Indetail.Count; i++)
        {
            if (string.IsNullOrEmpty(Data_Indetail[i]))
            {
                //Debug.Log("Data Empty" + i);
            }
            else
            {
                Item.Data.Add(Data_Indetail[i]);
            }
        }

        OriginDataList.Add(Item);
        NewDataList = OriginDataList;

        if (NewDataList[NewDataList.Count - 1].ID == Item.ID)
        {
            Write();
            Debug.Log("Result In detail data saved!");
        }
    }

    public void Init_RID()
    {

        for (int i = 0; i < 50; i++)
        {
            String_Data_attribute.Add("Data_" + (i + 1).ToString());
            //Debug.Log("Data_attribute : " + String_Data_attribute[i]);
        }
    }
}
