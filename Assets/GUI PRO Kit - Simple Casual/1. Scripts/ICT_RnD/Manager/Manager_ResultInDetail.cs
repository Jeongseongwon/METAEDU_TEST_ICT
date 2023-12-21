using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class Manager_ResultInDetail : MonoBehaviour
{
    public static Manager_ResultInDetail instance = null;
    public static List<Result_IndetailData> OriginDataList;
    private List<Result_IndetailData> NewDataList;

    private string filePath;
    private TextAsset XmlFilepath;


    private List<string> String_Data_attribute = new List<string>();
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

            for (int i = 0; i < OriginDataList.Count; ++i)
            {
                Result_IndetailData item = OriginDataList[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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

            //검증 필요함
            for (int i = 1; i < data.Data.Count; i++)
            {
                ItemElement.SetAttribute(String_Data_attribute[i], data.Data[i]);
            }
            
        }
        Document.Save(AssetDatabase.GetAssetPath(XmlFilepath));

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
                    Debug.Log("Data Empty"+ i);
                }
                else
                {
                    Item.Data.Add(ItemElement.GetAttribute(String_Data_attribute[i]));
             //       Debug.Log("Data_attribute : " + String_Data_attribute[i] + "item : " + Item.Data[i]);
                }
            }
            //Debug.Log("Data count : " + Item.Data.Count);
            ItemList.Add(Item);
        }
        return ItemList;
    }

    public void Init_RID()
    {
        
        for(int i = 0; i < 50; i++)
        {
            //1~50까지 입력, 편의상 i=1로 설정
            String_Data_attribute.Add("Data_" + (i+1).ToString());
            Debug.Log("Data_attribute : " + String_Data_attribute[i]);
        }
    }
}
