using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.WSA;

public class Button_Launcher_Login : MonoBehaviour, IPointerClickHandler
{


    private GameObject Launcher;

    public bool Login_Select = false;
    public bool Login_Edit = false;
    public bool Login_Register = false;
    public bool Login = false;
    // Start is called before the first frame update
    void Start()
    {
        Launcher = GameObject.Find("Launcher");
    }

    // Update is called once per frame
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Login)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Message_Login();

        if (Login_Register)
            Manager_login.instance.Add_Studentdata();

        if (Login_Select)
            Manager_login.instance.Setting_StudentInfo();
        //���
        //���� �ۼ��Ǿ��ִ� ������ ���� ������ ���

        //����
        //** �л� Ȱ��ȭ �� ���, �л� ���� ��ư ���� �ʿ�


        //����
        //���� ���õ� �����ͷ� �α��� ���� ����


    }
}
