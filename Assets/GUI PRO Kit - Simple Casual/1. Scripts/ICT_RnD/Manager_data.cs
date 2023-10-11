using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Manager_data : MonoBehaviour
{
    public static Manager_data instance = null;
    //어떤 데이터 저장하면 좋을지?
    //데이터 저장 방법?

    //데이터 갯수에 맞춰서 버튼 생성
    //버튼 클릭하면 해당하는 데이터로 화면 변경
    //순서, 정오반응 데이터, 강약반응 데이터


    public Text text_1;           //스크립트 나타는 박스
    public Text text_2;           //스크립트 나타는 박스
    public Text text_3;           //스크립트 나타는 박스

    public string File_name;    //해당 파일 불러오기

    public List<string> textList = new List<string>();


    private int Max_num_script = 0;

    private string Student_ID;    //학생 아이디
    private string Data_1;    //정오반응
    private string Data_2;    //강약반응

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
            Read_txt();
        }
    }
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

    //버튼을 클릭하면
    //해당하는 리스트에 저장되어있는 스트링 꺼내서
    //꺼내서 넣기

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
