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
    public bool Tool = false;
    public bool Result = false;
    public bool Back = false;
    public bool Back_ToContent = false;
    public bool Save = false;
    public bool Setting = false;
    public bool Close = false;
    public bool Home = false;
    public bool START = false;
    public int Num_contents = -1;
    public int Num_contents_Func = -1;
    //Teacher_UI 번호순서대로 콘텐츠 실행 

    // Start is called before the first frame update
    void Start()
    {
        Launcher = GameObject.Find("Launcher");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Message_Contents)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Message_Contents();

        if (Back)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Back_ToHome();

        if (Back_ToContent)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Back_ToContent();

        if (Setting)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Setting();

        if (Close)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Close();

        if (Home)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Home();

        if (START)
            Launcher.GetComponent<GameLauncher_ICT>().Button_START();

        if (Num_contents != -1)
            Launcher.GetComponent<GameLauncher_ICT>().Run_Contents(Num_contents);

        if(Num_contents_Func != -1)
            Launcher.GetComponent<GameLauncher_ICT>().Run_Contents(Num_contents);

        if (Save)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Save();

        if (Tool)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Tool();

        if (Result)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Result();
        //우선은 추후에 개발될 UI들도 여기서 공통적으로 구현하는걸로
    }

    //뒤로 가기 버튼을 구현하려면 ON되어있는 씬을 추적해야할 필요가 있을듯
}
