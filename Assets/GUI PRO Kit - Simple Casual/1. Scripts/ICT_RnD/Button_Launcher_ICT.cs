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
    public int Back = -1;
    public bool Setting = false;
    public bool Close = false;
    public bool Home = false;
    public int Teacher_UI = -1;
    public bool START = false;

    // Start is called before the first frame update
    void Start()
    {
        Launcher = GameObject.Find("Launcher");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
       //여기 전체 기능 다시 한 번 다듬기
        
        if (Contents != -1)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Contents(Contents);

        //Back 기능 수정 필요
        //1일 때 홈
        //2일 때 저작도구
        if (Back == 1)
        {
            Launcher.GetComponent<GameLauncher_ICT>().Button_Back_ToHome();
        }
        else if (Back == 2)
        {
            Launcher.GetComponent<GameLauncher_ICT>().Button_Back_ToTool();
        }

        if (Setting)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Setting();

        if (Close)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Close();

        if (Home)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Home();

        if (START)
            Launcher.GetComponent<GameLauncher_ICT>().Button_START();

        if (Teacher_UI != -1)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Teacher_UI(Teacher_UI);

        //우선은 추후에 개발될 UI들도 여기서 공통적으로 구현하는걸로
    }

    //뒤로 가기 버튼을 구현하려면 ON되어있는 씬을 추적해야할 필요가 있을듯
}
