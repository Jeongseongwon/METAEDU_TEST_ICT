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
    public bool Message_Contents = false;
    public bool Message_Contents_Login = false;
    public bool Tool = false;
    public bool Result = false;
    public bool Back = false;
    public bool Back_ToContent = false;
    public bool Back_ToMode = false;
    public bool Save_Tool = false;
    public bool Setting = false;
    public bool Close = false;
    public bool Home = false;
    public bool START = false;
    public int Num_contents = -1;
    public int Num_contents_Func = -1;
    public int Mode = -1;
    public bool Music_Content_End = false;
    //Teacher_UI 번호순서대로 콘텐츠 실행 

    // Start is called before the first frame update
    void Start()
    {
        Launcher = GameObject.Find("Launcher");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //To_Message
        if (Message_Contents)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Message_Contents();

        if (Message_Contents_Login && Num_contents != -1)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Message_Contents_Select(Num_contents);

        //To_Page
        if (Back)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Back_ToHome();

        if (Back_ToContent)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Back_ToContent();

        if (Back_ToMode)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Back_ToMode();

        if (Setting)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Setting();

        if (Close)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Setting_Close();

        if (Home)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Home();

        if (Mode != -1)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Mode(Mode);

        if (Num_contents_Func != -1)
            Launcher.GetComponent<GameLauncher_ICT>().Run_Contents_Func(Num_contents_Func);

        if (Save_Tool)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Save_Tool();

        if (Tool)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Tool();

        if (Result)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Result();

        if (Music_Content_End)
            Launcher.GetComponent<GameLauncher_ICT>().Run_Contents();
    }
}
