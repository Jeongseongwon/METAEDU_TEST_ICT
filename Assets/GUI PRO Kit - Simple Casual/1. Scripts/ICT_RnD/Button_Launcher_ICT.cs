using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Launcher_ICT : MonoBehaviour, IPointerClickHandler
{
    /*
     * 0923 해당 되는 버튼 기능의 변수에 TRUE 또는 입력 할 것 
     * 
     *
     **/

    private GameObject Launcher;
    public int Mode = -1;
    public int Contents = -1;
    public bool Back = false;
    public bool Setting = false;
    public bool Close = false;
    public bool Home_student = false;
    public bool Home_teacher = false;
    public int Teacher_UI = -1;
    public int Student_UI = -1;
    public bool BackToTeacher = false;
    public bool BackToStudent = false;

    // Start is called before the first frame update
    void Start()
    {
        Launcher = GameObject.Find("Launcher");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
       
        if (Mode != -1)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Modes(Mode);
        
        if (Contents != -1)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Contents(Contents);


        if (Back)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Back_ToMode();

        if (Setting)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Setting();

        if (Close)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Close();

        if (Home_student)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Home_Student();

        if (Home_teacher)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Home_Teacher();

        if (Teacher_UI != -1)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Teacher_UI(Teacher_UI);

        if (Student_UI != -1)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Student_UI(Student_UI);

        if (BackToTeacher)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Back_ToTeacher();

        if (BackToStudent)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Back_ToStudent();
        //우선은 추후에 개발될 UI들도 여기서 공통적으로 구현하는걸로
    }

    //뒤로 가기 버튼을 구현하려면 ON되어있는 씬을 추적해야할 필요가 있을듯
}
