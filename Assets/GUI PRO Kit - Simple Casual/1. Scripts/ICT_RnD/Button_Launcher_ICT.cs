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
       //���� ��ü ��� �ٽ� �� �� �ٵ��
        
        if (Contents != -1)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Contents(Contents);

        //Back ��� ���� �ʿ�
        //1�� �� Ȩ
        //2�� �� ���۵���
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

        //�켱�� ���Ŀ� ���ߵ� UI�鵵 ���⼭ ���������� �����ϴ°ɷ�
    }

    //�ڷ� ���� ��ư�� �����Ϸ��� ON�Ǿ��ִ� ���� �����ؾ��� �ʿ䰡 ������
}
