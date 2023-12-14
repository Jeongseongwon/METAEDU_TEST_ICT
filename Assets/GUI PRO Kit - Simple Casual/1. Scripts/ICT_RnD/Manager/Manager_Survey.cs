using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.WSA;
using UnityEngine.UI;

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
    private List<string> Text_QuestionList = new List<string>();


    public GameObject Message_AnswerNotSelected;
    public GameObject Message_SubmitCompleted;
    public GameObject Text_group;
    private Text Text_Number;
    private Text Text_Question;

    public GameObject Survey_UI;
    private GameObject Intro;
    private GameObject Question;
    private GameObject Submit;

    private string text_QuestionNumber;
    private string text_Question;

    private bool Is_AnswerSelected;

    [Header("SURVEY INFORMATION]")]
    [SerializeField]
    public string ID;
    public string Name;
    public string Date;
    public string Session;
    public string Data_1;
    public string Data_2;
    public string Data_3;
    public string Data_4;
    public string Data_5;
    public string Data_6;
    public string Data_7;
    public string Data_8;
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
            //NewDataList.Add(Student_data);
            //Debug.Log("SAVED DATA WRITE");

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


            ID = Item.ID;
            Name = Item.Name;
            Date = Item.Date;
            Session = Item.Session;
            Data_1 = Item.Data_S1;
            Data_2 = Item.Data_S2;
            Data_3 = Item.Data_S3;
            Data_4 = Item.Data_S4;
            Data_5 = Item.Data_S5;
            Data_6 = Item.Data_S6;
            Data_7 = Item.Data_S7;
            Data_8 = Item.Data_S8;

            ItemList.Add(Item);
        }
        return ItemList;
    }
    public void Add_Surveydata()
    {
        SurveyData Item = new SurveyData();

        //Item.ID = Manager_login.instance.ID;
        //Item.Name = Manager_login.instance.Name;
        //Item.Date = Manager_login.instance.Date;

        //Item.Data_S1 = Answer_Number[0].ToString();
        //Item.Data_S2 = Answer_Number[1].ToString();
        //Item.Data_S3 = Answer_Number[2].ToString();
        //Item.Data_S4 = Answer_Number[3].ToString();
        //Item.Data_S5 = Answer_Number[4].ToString();
        //Item.Data_S6 = Answer_Number[5].ToString();
        //Item.Data_S7 = Answer_Number[6].ToString();
        //Item.Data_S8 = Answer_Number[7].ToString();

        Item.ID = ID;
        Item.Name = Name;
        Item.Date = Date;
        Item.Data_S1 = Data_1;
        Item.Data_S2 = Data_2;
        Item.Data_S3 = Data_3;
        Item.Data_S4 = Data_4;
        Item.Data_S5 = Data_5;
        Item.Data_S6 = Data_6;
        Item.Data_S7 = Data_7;
        Item.Data_S8 = Data_8;

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
        Init_Survey();

        filePath = UnityEngine.Application.dataPath + "/Resources/Data/SURVEYRESULT.xml";

        if (filePath != null)
        {
            OriginDataList = Read();

            for (int i = 0; i < OriginDataList.Count; ++i)
            {
                SurveyData item = OriginDataList[i];
            }
        }

    }
    public void ChangeText()
    {
        Text_Number.text = (Question_Number+1) + "/8";
        Text_Question.text = Text_QuestionList[Question_Number];
    }

    public void Button_Next()
    {
        if (Is_AnswerSelected)
        {
            if (Question_Number == 7)
            {
                Question.SetActive(false);
                Submit.SetActive(true);
            }
            else
            {
                Question_Number += 1;
                ChangeText();
                Is_AnswerSelected = false;
            }
        }
        else
        {
            Message_AnswerNotSelected.SetActive(true);
        }
    }
    public void Button_Prev()
    {
        Question_Number -= 1;
        ChangeText();
        Is_AnswerSelected = false;
        //가장 앞에 부분 예외처리
    }
    public void Button_Answer(int num_Answer)
    {
        //해당 문제 번호
        //해당 정답 번호
        Is_AnswerSelected = true;
        Answer_Number[Question_Number] = num_Answer;

        //다음 버튼 애니메이션 재생 되게끔
    }
    public void Button_Start()
    {
        Intro.SetActive(false);
        Question.SetActive(true );
    }
    public void Button_Submit()
    {
        Add_Surveydata();
    }

    public void Init_Survey()
    {
        Debug.Log("서베이 초기화 확인");

        Intro = Survey_UI.transform.GetChild(0).gameObject;
        Question = Survey_UI.transform.GetChild(1).gameObject;
        Submit = Survey_UI.transform.GetChild(2).gameObject;

        Text_Number = Text_group.transform.GetChild(0).gameObject.GetComponent<Text>();
        Text_Question = Text_group.transform.GetChild(1).gameObject.GetComponent<Text>();

        Intro.SetActive(true);
        Question.SetActive(false);
        Submit.SetActive(false);
        
        Text_QuestionList.Add("프로그램이 마음에 들었나요?");
        Text_QuestionList.Add("프로그램이 재미있었나요?");
        Text_QuestionList.Add("프로그램이 사용하기 쉬웠나요?");
        Text_QuestionList.Add("프로그램에서 꽃벵이/옥수수/당근/알로에를 쉽게 알아볼 수 있었나요?");
        Text_QuestionList.Add("프로그램을 통해 꽃벵이/옥수수/당근/알로에의 이름을 잘 알게 되었나요?");
        Text_QuestionList.Add("프로그램을 사용해서 배우니 꽃벵이/옥수수/당근/알로에를 이해하는데 더 도움이 되었나요?");
        Text_QuestionList.Add("프로그램의 사용이 만족스러운가요?");
        Text_QuestionList.Add("다른 친구에게 프로그램의 사용법을 설명하기 쉬운가요?");
    }
}
