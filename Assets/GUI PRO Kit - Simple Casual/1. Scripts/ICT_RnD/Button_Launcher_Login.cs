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
        //등록
        //현재 작성되어있는 데이터 토대로 데이터 등록

        //수정
        //** 학생 활성화 될 경우, 학생 삭제 버튼 구현 필요


        //선택
        //현재 선택된 데이터로 로그인 정보 저장


    }
}
