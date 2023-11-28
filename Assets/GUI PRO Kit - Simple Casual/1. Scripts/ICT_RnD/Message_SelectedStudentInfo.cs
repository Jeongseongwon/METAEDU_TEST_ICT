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
            Text_Name.text = Student_Name + " �л����� �α��� �ұ��?";

        if (Student_ID != null)
            Text_ID.text = "�л� ID : " + Student_ID;
    }

    public void Change_Info()
    {
        Student_Name = Manager_login.instance.Get_SelectedStudentName();
        Student_ID = Manager_login.instance.Get_SelectedStudentID();

        if(Text_Name != null)
            Text_Name.text = Student_Name + " �л����� �α��� �ұ��?";

        if (Text_ID != null)
            Text_ID.text = "�л� ID : "+ Student_ID;
    }
}
