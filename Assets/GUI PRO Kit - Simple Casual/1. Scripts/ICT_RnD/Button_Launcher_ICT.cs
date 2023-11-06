using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Launcher_ICT : MonoBehaviour, IPointerClickHandler
{
    /*
     * 0923 �ش� �Ǵ� ��ư ����� ������ TRUE �Ǵ� �Է� �� �� 
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
    //Teacher_UI ��ȣ������� ������ ���� 

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
        //�켱�� ���Ŀ� ���ߵ� UI�鵵 ���⼭ ���������� �����ϴ°ɷ�
    }

    //�ڷ� ���� ��ư�� �����Ϸ��� ON�Ǿ��ִ� ���� �����ؾ��� �ʿ䰡 ������
}
