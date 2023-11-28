using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message_SelectedStudentInfo : MonoBehaviour
{
    public GameObject Text_group;

    public Text Text_Name;
    public Text Text_ID;

    private string Student_ID;
    private string Student_Name;
    // Start is called before the first frame update
    void Start()
    {
        Text_Name = Text_group.transform.GetChild(0).gameObject.GetComponent<Text>();
        Text_ID = Text_group.transform.GetChild(1).gameObject.GetComponent<Text>();

        if (Student_Name != null)
            Text_Name.text = Student_Name + " 학생으로 로그인 할까요?";

        if (Student_ID != null)
            Text_ID.text = "학생 ID : " + Student_ID;
    }

    public void Change_Info()
    {
        Student_Name = Manager_login.instance.Get_SelectedStudentName();
        Student_ID = Manager_login.instance.Get_SelectedStudentID();

        if(Text_Name != null)
            Text_Name.text = Student_Name + " 학생으로 로그인 할까요?";

        if (Text_ID != null)
            Text_ID.text = "학생 ID : "+ Student_ID;
    }
}
