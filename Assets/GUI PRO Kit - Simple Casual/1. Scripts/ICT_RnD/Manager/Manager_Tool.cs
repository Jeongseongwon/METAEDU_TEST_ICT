using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Tool : MonoBehaviour
{
    public static Manager_Tool instance = null;

    public GameObject Tool_BGM_UI;
    public GameObject Tool_Inst_UI;

    private bool Is_BGM_Selected = false;
    private bool Is_Inst_Selected = false;


    private GameObject Inst_1;
    private GameObject Inst_2;
    private GameObject Inst_3;
    //우선은 클릭하면 해당 번호가 순차적으로 출력되게 구현

    //해당 하는 북으로 바꾸고
    //해당 하는 북 저장해놓고 있어야함


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }
    void Start()
    {
        
    }


    public void Active_Inst()
    {
        Tool_Inst_UI.SetActive(true);
        //BGM 설정하고 있던거 중지 또는 초기화
        Is_Inst_Selected = true;
        Is_BGM_Selected = false;
    }
    //악기 설정하고 있던거 중지 또는 초기화 필요

    public void Change_Inst(int selected)
    {
        //우선 Inst가 활성화 되어있는지 확인

        if (Is_Inst_Selected)
        {
            if(selected == 0)
            {
                //해당 하는 위치 애니메이션 및 저장
            }
            else if(selected == 1)
            {

            }
            else if (selected == 2)
            {

            }
            else if (selected == 3)
            {

            }
            else if (selected == 4)
            {

            }
            else if (selected == 5)
            {

            }
        }
    }
    public void Active_BGM()
    {
        Tool_BGM_UI.SetActive(true);
        //악기 설정하고 있던거 중지 또는 초기화 필요

        Is_Inst_Selected = false;
        Is_BGM_Selected = true;
    }
    public void Change_BGM()
    {
        Tool_BGM_UI.SetActive(true);
        //악기 설정하고 있던거 중지 또는 초기화 필요
    }
}
